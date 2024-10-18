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
        public IActionResult SubmitClaim(ClaimSubmissionModel model)
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

                if (model.SupportingDocument != null)
                {
                    claim.SupportingDocumentName = model.SupportingDocument.FileName;
                   
                }

                _claimService.AddClaim(claim);
                TempData["Message"] = "Claim was successfully submitted";
                return RedirectToAction("ViewSubmittedClaims");
            }
            return View("EnterClaimDetails", model);
        }

        public IActionResult ViewSubmittedClaims()
        {
            var claims = _claimService.GetClaimsByLecturer("lecturer@keemouniversity.com");
            return View("~/Views/Shared/SubmittedClaims.cshtml", claims);
        }

        public IActionResult ClaimDetails(int id)
        {
            var claim = _claimService.GetClaimById(id);
            return View(claim); 
        }
    }
}