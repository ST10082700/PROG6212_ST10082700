using PROG6212___CMCS___ST10082700.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace PROG6212___CMCS___ST10082700.Services
{
    public class ClaimService : IClaimService
    {
        private static List<ClaimModel> _claims = new List<ClaimModel>();
        private readonly ILogger<ClaimService> _logger;

        public ClaimService(ILogger<ClaimService> logger)
        {
            _logger = logger;
            _claims.Add(new ClaimModel
            {
                Id = 1,
                ClaimName = "Test Claim",
                ClaimDate = DateTime.Now,
                HoursWorked = 5,
                HourlyRate = 50,
                Description = "Test Description",
                Status = "Pending",
                LecturerUsername = "lecturer@keemouniversity.com"
            });
        }

        public void AddClaim(ClaimModel claim)
        {
            claim.Id = _claims.Count + 1;
            _claims.Add(claim);
            _logger.LogInformation($"Claim added: {claim.Id} - {claim.ClaimName}");
        }

        public List<ClaimModel> GetClaimsByLecturer(string lecturerUsername)
        {
            var claims = _claims.Where(c => c.LecturerUsername == lecturerUsername).ToList();
            _logger.LogInformation($"Retrieved {claims.Count} claims for lecturer: {lecturerUsername}");
            return claims;
        }

        public void UpdateClaim(ClaimModel claim)
        {
            var existingClaim = _claims.FirstOrDefault(c => c.Id == claim.Id);
            if (existingClaim != null)
            {
                existingClaim.Status = claim.Status;
                _logger.LogInformation($"Claim updated: {claim.Id} - New status: {claim.Status}");
            }
        }

        // Implement the missing methods
        public ClaimModel GetClaimById(int id)
        {
            var claim = _claims.FirstOrDefault(c => c.Id == id);
            if (claim != null)
            {
                _logger.LogInformation($"Retrieved claim: {claim.Id} - {claim.ClaimName}");
            }
            else
            {
                _logger.LogWarning($"Claim with id {id} not found");
            }
            return claim;
        }

        public List<ClaimModel> GetAllClaims()
        {
            _logger.LogInformation($"Retrieved all claims. Total count: {_claims.Count}");
            return _claims;
        }
    }
}