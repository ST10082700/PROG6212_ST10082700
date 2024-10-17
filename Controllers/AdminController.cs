using Microsoft.AspNetCore.Mvc;
using PROG6212___CMCS___ST10082700.Models;
using PROG6212___CMCS___ST10082700.Services;

namespace PROG6212___CMCS___ST10082700.Controllers
{
    public class AdminController : Controller
    {
        private readonly IClaimService _claimService;

        public AdminController(IClaimService claimService)
        {
            _claimService = claimService;
        }

        public IActionResult Dashboard()
        {
            var model = new AdminDashboardModel
            {
                WelcomeMessage = User.IsInRole("Coordinator") ? "Welcome Coordinator." : "Welcome Manager."
            };
            return View(model);
        }

        public IActionResult ViewSubmittedClaims()
        {
            var claims = _claimService.GetAllClaims();
            return View("~/Views/Shared/SubmittedClaims.cshtml", claims);
        }

        public IActionResult ClaimDetails(int id)
        {
            var claim = _claimService.GetClaimById(id);
            return View(claim);
        }

        [HttpPost]
        public IActionResult ApproveClaim(int id)
        {
            var claim = _claimService.GetClaimById(id);
            claim.Status = "Accepted";
            _claimService.UpdateClaim(claim);
            TempData["Message"] = "Claim was successfully accepted";
            return RedirectToAction("ViewSubmittedClaims");
        }

        [HttpPost]
        public IActionResult RejectClaim(int id)
        {
            var claim = _claimService.GetClaimById(id);
            claim.Status = "Rejected";
            _claimService.UpdateClaim(claim);
            TempData["Message"] = "Claim was successfully rejected";
            return RedirectToAction("ViewSubmittedClaims");
        }
    }
}
