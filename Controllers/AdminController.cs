using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace PROG6212___CMCS___ST10082700.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Dashboard()
        {
            if (User.IsInRole("Coordinator"))
            {
                ViewBag.WelcomeMessage = "Welcome Coordinator";
            }
            else if (User.IsInRole("Manager"))
            {
                ViewBag.WelcomeMessage = "Welcome Manager";
            }
            return View();
        }

        public IActionResult ClaimDetails(int id)
        {
            // Logic for claim details, will implement later
            return View();
        }

        [HttpPost]
        public IActionResult ApproveClaim(int id)
        {
            // Logic for approving claim, will implement later
            return RedirectToAction("Dashboard");
        }

        [HttpPost]
        public IActionResult RejectClaim(int id)
        {
            // Logic for rejecting claim, will implement later
            return RedirectToAction("Dashboard");
        }
    }
}
