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
        public decimal HoursWorked { get; set; }

        [Required]
        public decimal HourlyRate { get; set; }

        [Required]
        public string Description { get; set; }

        public IFormFile SupportingDocument { get; set; }
    }
}
