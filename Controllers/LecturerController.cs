using Microsoft.AspNetCore.Mvc;
using PROG6212___CMCS___ST10082700.Models;

namespace PROG6212___CMCS___ST10082700.Controllers
{
    public class LecturerController : Controller
    {
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
                // Process the claim submission, will implement later
                // Save to database, etc. will implement later
                if (model.SupportingDocument != null)
                {
                    // Process the uploaded file, will implement later
                    // Save it to a specific location or database, will implement later
                }
                // Redirect to a success page or back to the dashboard
                return RedirectToAction("Dashboard");
            }
            // If the model is not valid, return to the form with error messages
            return View("EnterClaimDetails", model);
        }
    }
}