using Microsoft.AspNetCore.Mvc;
using PROG6212___CMCS___ST10082700.Models;


namespace  PROG6212___CMCS___ST10082700.Controllers 
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Check the username and redirect accordingly
                switch (model.Username.ToLower())
                {
                    case "lecturer@keemouniversity.com":
                        return RedirectToAction("Dashboard", "Lecturer");

                    case "coordinator@keemouniversity.com":
                        TempData["WelcomeMessage"] = "Welcome Coordinator";
                        return RedirectToAction("Dashboard", "Admin");

                    case "manager@keemouniversity.com":
                        TempData["WelcomeMessage"] = "Welcome Manager";
                        return RedirectToAction("Dashboard", "Admin");

                    default:
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                        break;
                }
            }

            // If we got this far, something failed; redisplay the form
            return View(model);
        }
    }
}