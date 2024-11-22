using PROG6212___CMCS___ST10082700.Models;


namespace PROG6212___CMCS___ST10082700.Services
{
    public interface IClaimService
    {
        Task<IEnumerable<ClaimModel>> GetAllClaimsAsync();
        Task<IEnumerable<ClaimModel>> GetClaimsByLecturerAsync(string lecturerUsername);
        Task<ClaimModel> GetClaimByIdAsync(int id);
        Task AddClaimAsync(ClaimModel claim, IFormFile supportingDocument);
        Task<bool> ApproveClaimAsync(int id, string approverUsername);
        Task<bool> RejectClaimAsync(int id, string approverUsername);
        Task UpdateClaimAsync(ClaimModel claim);
        Task<string> GetSupportingDocumentPathAsync(int claimId); 
    }
}
