using Microsoft.AspNetCore.Mvc;
using PROG6212_CMCS_ST10082700.Models;

namespace PROG6212_CMCS_ST10082700.Controllers
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
                // Will implement this later, with functionality to check if the user exists in the database

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            return View(model);
        }
    }
}