namespace PROG6212___CMCS___ST10082700.Services
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(IFormFile file, string claimId);
        Task DeleteFileAsync(string filePath);
        bool IsValidFileType(IFormFile file);
        bool IsValidFileSize(IFormFile file, long maxSize);
    }
}
