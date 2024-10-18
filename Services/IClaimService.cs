using PROG6212___CMCS___ST10082700.Models;


namespace PROG6212___CMCS___ST10082700.Services
{
    public interface IClaimService
    {
        void AddClaim(ClaimModel claim);
        List<ClaimModel> GetClaimsByLecturer(string lecturerUsername);
        void UpdateClaim(ClaimModel claim);
        ClaimModel GetClaimById(int id);
        List<ClaimModel> GetAllClaims();
    }
}
