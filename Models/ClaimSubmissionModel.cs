using System.ComponentModel.DataAnnotations;

namespace PROG6212___CMCS___ST10082700.Models
{
    public class ClaimSubmissionModel
    {
        [Required(ErrorMessage = "Claim name is required")]
        [Display(Name = "Claim Name")]
        public string ClaimName { get; set; }

        [Required(ErrorMessage = "Claim date is required")]
        [Display(Name = "Claim Date")]
        [DataType(DataType.Date)]
        public DateTime ClaimDate { get; set; }

        [Required(ErrorMessage = "Hours worked is required")]
        [Display(Name = "Hours Worked")]
        [Range(0.1, double.MaxValue, ErrorMessage = "Hours worked must be greater than 0")]
        public decimal HoursWorked { get; set; }

        [Required(ErrorMessage = "Hourly rate is required")]
        [Display(Name = "Hourly Rate")]
        [Range(0.1, double.MaxValue, ErrorMessage = "Hourly rate must be greater than 0")]
        [DataType(DataType.Currency)]
        public decimal HourlyRate { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [Display(Name = "Description")]
        [StringLength(1000, MinimumLength = 10, ErrorMessage = "Description must be between 10 and 1000 characters")]
        public string Description { get; set; }

        [Display(Name = "Supporting Document")]
        [AllowedExtensions(new string[] { ".pdf", ".doc", ".docx", ".xls", ".xlsx" })]
        [MaxFileSize(5 * 1024 * 1024)] // 5MB max file size
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

    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;
        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                if (file.Length > _maxFileSize)
                {
                    return new ValidationResult($"File size cannot exceed {_maxFileSize / 1024 / 1024}MB");
                }
            }
            return ValidationResult.Success;
        }
    }
}
