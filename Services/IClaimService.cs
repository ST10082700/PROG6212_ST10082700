using PROG6212___CMCS___ST10082700.Models;


namespace PROG6212___CMCS___ST10082700.Services
{
    public interface IClaimService
    {
        Task<IEnumerable<ClaimModel>> GetAllClaimsAsync();  // For fetching all claims
        Task<IEnumerable<ClaimModel>> GetClaimsByLecturerAsync(string lecturerUsername);  // For fetching claims by a specific lecturer
        Task<ClaimModel> GetClaimByIdAsync(int id);  // For fetching a specific claim by ID
        Task AddClaimAsync(ClaimModel claim);  // For adding a new claim
        Task<bool> ApproveClaimAsync(int id);
        Task<bool> RejectClaimAsync(int id);
        Task UpdateClaimAsync(ClaimModel claim);
    }
}
