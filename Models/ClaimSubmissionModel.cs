using System.ComponentModel.DataAnnotations;

namespace PROG6212___CMCS___ST10082700.Models
{
    public class ClaimSubmissionModel
    {
        [Required]
        public string ClaimName { get; set; }

        [Required]
        public DateTime ClaimDate { get; set; }

        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Hours worked must be greater than 0")]
        public decimal HoursWorked { get; set; }

        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Hourly rate must be greater than 0")]
        public decimal HourlyRate { get; set; }

        [Required]
        public string Description { get; set; }

        [AllowedExtensions(new string[] { ".pdf", ".doc", ".docx", ".xls", ".xlsx" })]
        public IFormFile SupportingDocument { get; set; }
    }

    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;
        public AllowedExtensionsAttribute(string[] extensions)
        {
            _extensions = extensions;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                var extension = Path.GetExtension(file.FileName);
                if (!_extensions.Contains(extension.ToLower()))
                {
                    return new ValidationResult($"File extension {extension} is not allowed. Allowed extensions are: {string.Join(", ", _extensions)}");
                }
            }
            return ValidationResult.Success;
        }
    }
}
