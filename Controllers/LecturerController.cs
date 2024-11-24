using Microsoft.AspNetCore.Mvc;
using PROG6212___CMCS___ST10082700.Models;
using PROG6212___CMCS___ST10082700.Services;

namespace PROG6212___CMCS___ST10082700.Controllers
{
    public class LecturerController : Controller
    {
        private readonly IClaimService _claimService;

        public LecturerController(IClaimService claimService)
        {
            _claimService = claimService;
        }

        public IActionResult Dashboard()
        {
            var model = new LecturerDashboardModel
            {
                LecturerUsername = "lecturer@keemouniversity.com"
            };
            return View(model);
        }

        public IActionResult EnterClaimDetails()
        {
            return View(new ClaimSubmissionModel());
        }

        [HttpPost]
        public async Task<IActionResult> SubmitClaim(ClaimSubmissionModel model)
        {
            if (ModelState.IsValid)
            {
                // Recalculate total amount server-side for security
                decimal totalAmount = model.HoursWorked * model.HourlyRate;
                var claim = new ClaimModel
                {
                    ClaimName = model.ClaimName,
                    ClaimDate = model.ClaimDate,
                    HoursWorked = model.HoursWorked,
                    HourlyRate = model.HourlyRate,
                    TotalAmount = totalAmount,
                    Description = model.Description,
                    LecturerUsername = User.Identity.Name ?? "lecturer@iievarsitycollege.com",
                    Status = "Pending",
                    SubmissionDate = DateTime.UtcNow
                };

                if (model.SupportingDocument != null)
                {
                    claim.SupportingDocumentName = model.SupportingDocument.FileName;
                }

                await _claimService.AddClaimAsync(claim, model.SupportingDocument);
                TempData["Success"] = "Claim successfully submitted";
                return RedirectToAction("ViewSubmittedClaims");
            }
            return View("EnterClaimDetails", model);
        }

        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> UpdateClaimStatus(int claimId, string status)
        {
            if (string.IsNullOrEmpty(status) || !new[] { "Approved", "Rejected" }.Contains(status))
            {
                return BadRequest("Invalid status");
            }

            var username = User?.Identity?.Name ?? "UnknownUser";

            bool success = status == "Approved"
                ? await _claimService.ApproveClaimAsync(claimId, username)
                : await _claimService.RejectClaimAsync(claimId, username);

            if (!success)
            {
                return NotFound("Claim not found");
            }

            return RedirectToAction("ViewClaims");
        }

    }
}