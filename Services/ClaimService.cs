using PROG6212___CMCS___ST10082700.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using PROG6212___CMCS___ST10082700.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Supabase.Gotrue;


namespace PROG6212___CMCS___ST10082700.Services
{
    public class ClaimService : IClaimService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string[] allowedFileExtensions = { ".pdf", ".doc", ".docx", ".xls", ".xlsx" };
        private const long MaxFileSize = 5 * 1024 * 1024; // 5MB

        public ClaimService(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IEnumerable<ClaimModel>> GetAllClaimsAsync()
        {
            return await _context.Claims
                .OrderByDescending(c => c.SubmissionDate)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<ClaimModel>> GetClaimsByLecturerAsync(string lecturerUsername)
        {
            if (string.IsNullOrEmpty(lecturerUsername))
                throw new ArgumentNullException(nameof(lecturerUsername));

            return await _context.Claims
                .Where(c => c.LecturerUsername == lecturerUsername)
                .OrderByDescending(c => c.SubmissionDate)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ClaimModel> GetClaimByIdAsync(int id)
        {
            return await _context.Claims
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddClaimAsync(ClaimModel claim, IFormFile supportingDocument)
        {
            ValidateClaimModel(claim);

            if (supportingDocument != null)
            {
                ValidateFile(supportingDocument);
                claim.SupportingDocumentName = await SaveFileAsync(supportingDocument);
            }

            claim.Status = "Pending";
            claim.SubmissionDate = DateTime.UtcNow;
            claim.IsVerified = false;
            claim.TotalAmount = claim.HoursWorked * claim.HourlyRate;

            try
            {
                _context.Claims.Add(claim);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // If file was saved but database operation failed, cleanup the file
                if (!string.IsNullOrEmpty(claim.SupportingDocumentName))
                {
                    await DeleteFileAsync(claim.SupportingDocumentName);
                }
                throw new InvalidOperationException("Failed to save claim to database", ex);
            }
        }

        public async Task<bool> ApproveClaimAsync(int id, string approverUsername)
        {
            if (string.IsNullOrEmpty(approverUsername))
                throw new ArgumentNullException(nameof(approverUsername));

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var claim = await _context.Claims.FindAsync(id);
                if (claim == null || claim.Status != "Pending")
                    return false;

                claim.Status = "Approved";
                claim.VerificationDate = DateTime.UtcNow;
                claim.VerifiedBy = approverUsername;
                claim.IsVerified = true;
                claim.InvoiceNumber = GenerateInvoiceNumber(claim);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> RejectClaimAsync(int id, string approverUsername)
        {
            if (string.IsNullOrEmpty(approverUsername))
                throw new ArgumentNullException(nameof(approverUsername));

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var claim = await _context.Claims.FindAsync(id);
                if (claim == null || claim.Status != "Pending")
                    return false;

                claim.Status = "Rejected";
                claim.VerificationDate = DateTime.UtcNow;
                claim.VerifiedBy = approverUsername;
                claim.IsVerified = true;

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task UpdateClaimAsync(ClaimModel claim)
        {
            if (claim == null)
                throw new ArgumentNullException(nameof(claim));

            ValidateClaimModel(claim);

            var existingClaim = await _context.Claims.FindAsync(claim.Id);
            if (existingClaim == null)
                throw new InvalidOperationException($"Claim with ID {claim.Id} not found");

            // Only allow updates if the claim is still pending
            if (existingClaim.Status != "Pending")
                throw new InvalidOperationException("Cannot update a claim that has been approved or rejected");

            // Update the properties
            existingClaim.ClaimName = claim.ClaimName;
            existingClaim.ClaimDate = claim.ClaimDate;
            existingClaim.Description = claim.Description;
            existingClaim.HoursWorked = claim.HoursWorked;
            existingClaim.HourlyRate = claim.HourlyRate;
            existingClaim.TotalAmount = claim.HoursWorked * claim.HourlyRate;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Claims.AnyAsync(c => c.Id == claim.Id))
                    throw new InvalidOperationException($"Claim with ID {claim.Id} no longer exists");
                throw;
            }
        }


        public async Task<string> GetSupportingDocumentPathAsync(int claimId)
        {
            var claim = await _context.Claims.FindAsync(claimId);
            if (string.IsNullOrEmpty(claim?.SupportingDocumentName))
                return null;

            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", claim.SupportingDocumentName);
            return File.Exists(filePath) ? filePath : null;
        }

        private async Task<string> SaveFileAsync(IFormFile file)
        {
            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
            Directory.CreateDirectory(uploadsFolder); // Ensure directory exists

            string uniqueFileName = $"{DateTime.UtcNow:yyyyMMdd}_{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return uniqueFileName;
        }

        private async Task DeleteFileAsync(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return;

            string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", fileName);
            if (File.Exists(filePath))
            {
                await Task.Run(() => File.Delete(filePath));
            }
        }

        private void ValidateFile(IFormFile file)
        {
            if (file.Length > MaxFileSize)
                throw new InvalidOperationException($"File size exceeds maximum limit of {MaxFileSize / 1024 / 1024}MB");

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!allowedFileExtensions.Contains(extension))
                throw new InvalidOperationException($"File type {extension} is not allowed. Allowed types: {string.Join(", ", allowedFileExtensions)}");
        }

        private void ValidateClaimModel(ClaimModel claim)
        {
            if (claim == null)
                throw new ArgumentNullException(nameof(claim));

            if (string.IsNullOrEmpty(claim.ClaimName))
                throw new ArgumentException("Claim name is required");

            if (claim.HoursWorked <= 0)
                throw new ArgumentException("Hours worked must be greater than 0");

            if (claim.HourlyRate <= 0)
                throw new ArgumentException("Hourly rate must be greater than 0");
        }

        private string GenerateInvoiceNumber(ClaimModel claim)
        {
            return $"INV-{claim.Id}-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString().Substring(0, 8)}";
        }
    }
}