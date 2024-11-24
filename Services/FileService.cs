namespace PROG6212___CMCS___ST10082700.Services
{
    public class FileService : IFileService
    {
        private readonly string[] allowedExtensions = { ".pdf", ".doc", ".docx", ".xls", ".xlsx" };
        private readonly string uploadDirectory;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            uploadDirectory = Path.Combine(webHostEnvironment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadDirectory))
            {
                Directory.CreateDirectory(uploadDirectory);
            }
        }

        public async Task<string> SaveFileAsync(IFormFile file, string claimId)
        {
            if (file == null) return null;

            var fileName = $"{claimId}_{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(uploadDirectory, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileName;
        }

        public async Task DeleteFileAsync(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                var fullPath = Path.Combine(uploadDirectory, filePath);
                if (File.Exists(fullPath))
                {
                    await Task.Run(() => File.Delete(fullPath));
                }
            }
        }

        public bool IsValidFileType(IFormFile file)
        {
            if (file == null) return false;
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            return allowedExtensions.Contains(extension);
        }

        public bool IsValidFileSize(IFormFile file, long maxSize)
        {
            return file != null && file.Length <= maxSize;
        }
    }
}
