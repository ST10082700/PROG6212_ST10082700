using System.ComponentModel.DataAnnotations;

namespace PROG6212___CMCS___ST10082700.Models
{
    public class ClaimModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string ClaimName { get; set; }

        [Required]
        public DateTime ClaimDate { get; set; }

        [Required]
        [EmailAddress]
        public string LecturerUsername { get; set; }

        public DateTime SubmissionDate { get; set; }

        [Required]
        public string Status { get; set; } // "Pending", "Approved", "Rejected"

        [Required]
        [Range(0.5, 24)]
        public decimal HoursWorked { get; set; }

        [Required]
        [Range(0.1, 1000)]
        public decimal HourlyRate { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        public bool IsVerified { get; set; }

        public DateTime? VerificationDate { get; set; }

        public string VerifiedBy { get; set; }

        public string InvoiceNumber { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; }

        public string SupportingDocumentName { get; set; }

        public string SupportingDocumentPath { get; set; }

    }
}