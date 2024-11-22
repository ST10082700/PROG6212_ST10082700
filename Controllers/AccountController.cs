using Microsoft.AspNetCore.Mvc;
using PROG6212___CMCS___ST10082700.Models;


namespace  PROG6212___CMCS___ST10082700.Controllers 
{
    public class AccountController : Controller
    {
        private readonly IConfiguration _configuration;

        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                switch (model.Username.ToLower())
                {
                    case "lecturer@keemouniversity.com":
                        // Generate JWT token for API access
                        var token = GenerateJwtToken(model.Username, "Lecturer");
                        HttpContext.Session.SetString("JwtToken", token);
                        return RedirectToAction("Dashboard", "Lecturer");

                    case "coordinator@keemouniversity.com":
                        token = GenerateJwtToken(model.Username, "Coordinator");
                        HttpContext.Session.SetString("JwtToken", token);
                        TempData["WelcomeMessage"] = "Welcome Coordinator";
                        return RedirectToAction("Dashboard", "Admin");

                        // ... rest of the cases
                }
            }
            return View(model);
        }

        private string GenerateJwtToken(string username, string role)
        {
            // Implement JWT token generation
            // You'll need to add JWT authentication configuration in Program.cs
            return "";
        }
    }
}