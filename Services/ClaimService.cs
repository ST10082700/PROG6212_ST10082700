using PROG6212___CMCS___ST10082700.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace PROG6212___CMCS___ST10082700.Services
{
    public class ClaimService : IClaimService
    {
        private readonly List<ClaimModel> _claims;

        public ClaimService()
        {
            _claims = new List<ClaimModel>();
        }

        // Existing synchronous methods...

        public async Task<bool> ApproveClaimAsync(int id)
        {
            var claim = GetClaimById(id);
            if (claim != null && claim.Status == "Pending")
            {
                claim.Status = "Accepted";
                UpdateClaim(claim);
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }

        public async Task<bool> RejectClaimAsync(int id)
        {
            var claim = GetClaimById(id);
            if (claim != null && claim.Status == "Pending")
            {
                claim.Status = "Rejected";
                UpdateClaim(claim);
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }

        // Your existing methods remain the same...
        public IEnumerable<ClaimModel> GetAllClaims()
        {
            return _claims;
        }

        public IEnumerable<ClaimModel> GetClaimsByLecturer(string lecturerUsername)
        {
            return _claims.Where(c => c.LecturerUsername == lecturerUsername);
        }

        public ClaimModel GetClaimById(int id)
        {
            return _claims.FirstOrDefault(c => c.Id == id);
        }

        public void AddClaim(ClaimModel claim)
        {
            _claims.Add(claim);
        }

        public void UpdateClaim(ClaimModel claim)
        {
            var existingClaim = _claims.FirstOrDefault(c => c.Id == claim.Id);
            if (existingClaim != null)
            {
                var index = _claims.IndexOf(existingClaim);
                _claims[index] = claim;
            }
        }
    }
}