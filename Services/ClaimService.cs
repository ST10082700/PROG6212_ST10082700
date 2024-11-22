using PROG6212___CMCS___ST10082700.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using PROG6212___CMCS___ST10082700.Data;
using Microsoft.EntityFrameworkCore;


namespace PROG6212___CMCS___ST10082700.Services
{
    public class ClaimService : IClaimService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ClaimService(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IEnumerable<ClaimModel>> GetAllClaimsAsync()
        {
            return await _context.Claims
                .OrderByDescending(c => c.SubmissionDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<ClaimModel>> GetClaimsByLecturerAsync(string lecturerUsername)
        {
            return await _context.Claims
                .Where(c => c.LecturerUsername == lecturerUsername)
                .OrderByDescending(c => c.SubmissionDate)
                .ToListAsync();
        }

        public async Task<ClaimModel> GetClaimByIdAsync(int id)
        {
            return await _context.Claims.FindAsync(id);
        }

        public async Task AddClaimAsync(ClaimModel claim, IFormFile supportingDocument)
        {
            if (supportingDocument != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Generate unique filename
                string uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(supportingDocument.FileName)}";
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await supportingDocument.CopyToAsync(fileStream);
                }

                claim.SupportingDocumentName = uniqueFileName;
            }

            claim.Status = "Pending";
            claim.SubmissionDate = DateTime.UtcNow;
            claim.IsVerified = false;

            _context.Claims.Add(claim);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ApproveClaimAsync(int id, string approverUsername)
        {
            var claim = await _context.Claims.FindAsync(id);
            if (claim != null && claim.Status == "Pending")
            {
                claim.Status = "Accepted";
                claim.VerificationDate = DateTime.UtcNow;
                claim.VerifiedBy = approverUsername;
                claim.IsVerified = true;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> RejectClaimAsync(int id, string approverUsername)
        {
            var claim = await _context.Claims.FindAsync(id);
            if (claim != null && claim.Status == "Pending")
            {
                claim.Status = "Rejected";
                claim.VerificationDate = DateTime.UtcNow;
                claim.VerifiedBy = approverUsername;
                claim.IsVerified = true;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task UpdateClaimAsync(ClaimModel claim)
        {
            _context.Claims.Update(claim);
            await _context.SaveChangesAsync();
        }

        public async Task<string> GetSupportingDocumentPathAsync(int claimId)
        {
            var claim = await _context.Claims.FindAsync(claimId);
            if (claim?.SupportingDocumentName == null)
            {
                return null;
            }

            return Path.Combine(_webHostEnvironment.WebRootPath, "uploads", claim.SupportingDocumentName);
        }
    }
}