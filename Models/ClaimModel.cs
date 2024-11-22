using System.ComponentModel.DataAnnotations;

namespace PROG6212___CMCS___ST10082700.Models
{
    public class ClaimModel
    {
        public int Id { get; set; }
        public string ClaimName { get; set; }
        public DateTime ClaimDate { get; set; }
        public string LecturerUsername { get; set; }
        public DateTime SubmissionDate { get; set; }
        public string Status { get; set; }
        public decimal HoursWorked { get; set; }
        public decimal HourlyRate { get; set; }
        public decimal TotalAmount => HoursWorked * HourlyRate;
        public bool IsVerified { get; set; }
        public DateTime? VerificationDate { get; set; }
        public string VerifiedBy { get; set; }
        public string InvoiceNumber { get; set; }
        public string Description { get; set; }
        public string SupportingDocumentName
        {
            get; set;
        }

    }
}