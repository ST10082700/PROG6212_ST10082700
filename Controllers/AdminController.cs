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
            // The view will now use JavaScript to fetch claims via API
            return View("~/Views/Shared/SubmittedClaims.cshtml");
        }

        public IActionResult ClaimDetails(int id)
        {
            // The view will now use JavaScript to fetch claim details via API
            return View("~/Views/Shared/ClaimDetails.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> ApproveClaim(int id)
        {
            var result = await _claimService.ApproveClaimAsync(id);
            if (result)
            {
                TempData["Message"] = "Claim was successfully accepted";
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public async Task<IActionResult> RejectClaim(int id)
        {
            var result = await _claimService.RejectClaimAsync(id);
            if (result)
            {
                TempData["Message"] = "Claim was successfully rejected";
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}
