using PROG6212___CMCS___ST10082700.Models;

namespace PROG6212___CMCS___ST10082700.Services
{
    public class ClaimService : IClaimService
    {
        private static List<ClaimModel> _claims = new List<ClaimModel>();

        public List<ClaimModel> GetAllClaims()
        {
            return _claims;
        }

        public ClaimModel GetClaimById(int id)
        {
            return _claims.FirstOrDefault(c => c.Id == id);
        }

        public void AddClaim(ClaimModel claim)
        {
            claim.Id = _claims.Count + 1;
            _claims.Add(claim);
        }

        public void UpdateClaim(ClaimModel claim)
        {
            var existingClaim = _claims.FirstOrDefault(c => c.Id == claim.Id);
            if (existingClaim != null)
            {
                existingClaim.Status = claim.Status;
            }
        }

        public List<ClaimModel> GetClaimsByLecturer(string lecturerUsername)
        {
            return _claims.Where(c => c.LecturerUsername == lecturerUsername).ToList();
        }
    }
}
