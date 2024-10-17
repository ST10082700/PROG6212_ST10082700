using System.ComponentModel.DataAnnotations;

namespace PROG6212___CMCS___ST10082700.Models
{
    public class ClaimModel
    {
        public int Id { get; set; }
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
        public string SupportingDocumentName { get; set; }
        public string Status { get; set; } = "Pending";
        public string LecturerUsername { get; set; }
    }
}
