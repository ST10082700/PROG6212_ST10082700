using PROG6212___CMCS___ST10082700.Models;


namespace PROG6212___CMCS___ST10082700.Services
{
    public interface IClaimService
    {
        List<ClaimModel> GetAllClaims();
        ClaimModel GetClaimById(int id);
        void AddClaim(ClaimModel claim);
        void UpdateClaim(ClaimModel claim);
        List<ClaimModel> GetClaimsByLecturer(string lecturerUsername);
    }
}
