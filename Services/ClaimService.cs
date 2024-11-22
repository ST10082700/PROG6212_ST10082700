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

        public ClaimService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ClaimModel>> GetAllClaimsAsync()
        {
            return await _context.Claims.ToListAsync();
        }

        public async Task<IEnumerable<ClaimModel>> GetClaimsByLecturerAsync(string lecturerUsername)
        {
            return await _context.Claims
                .Where(c => c.LecturerUsername == lecturerUsername)
                .ToListAsync();
        }

        public async Task<ClaimModel> GetClaimByIdAsync(int id)
        {
            return await _context.Claims.FindAsync(id);
        }

        public async Task AddClaimAsync(ClaimModel claim)
        {
            _context.Claims.Add(claim);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ApproveClaimAsync(int id)
        {
            var claim = await _context.Claims.FindAsync(id);
            if (claim != null && claim.Status == "Pending")
            {
                claim.Status = "Accepted";
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> RejectClaimAsync(int id)
        {
            var claim = await _context.Claims.FindAsync(id);
            if (claim != null && claim.Status == "Pending")
            {
                claim.Status = "Rejected";
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
    }
}