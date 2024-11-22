using PROG6212___CMCS___ST10082700.Models;


namespace PROG6212___CMCS___ST10082700.Services
{
    public interface IClaimService
    {
        IEnumerable<ClaimModel> GetAllClaims();
        IEnumerable<ClaimModel> GetClaimsByLecturer(string lecturerUsername);
        ClaimModel GetClaimById(int id);
        void AddClaim(ClaimModel claim);
        void UpdateClaim(ClaimModel claim);
        Task<bool> ApproveClaimAsync(int id);
        Task<bool> RejectClaimAsync(int id);
    }
}
