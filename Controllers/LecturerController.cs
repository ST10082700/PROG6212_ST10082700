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
                var claim = new ClaimModel
                {
                    ClaimName = model.ClaimName,
                    ClaimDate = model.ClaimDate,
                    HoursWorked = model.HoursWorked,
                    HourlyRate = model.HourlyRate,
                    Description = model.Description,
                    LecturerUsername = "lecturer@keemouniversity.com"
                };

                // Check if a supporting document is provided
                if (model.SupportingDocument != null)
                {
                    claim.SupportingDocumentName = model.SupportingDocument.FileName;
                }

                // Pass the supporting document along with the claim
                await _claimService.AddClaimAsync(claim, model.SupportingDocument); // Pass the IFormFile

                TempData["Message"] = "Claim was successfully submitted";
                return RedirectToAction("ViewSubmittedClaims");
            }

            return View("EnterClaimDetails", model);
        }
    }
}