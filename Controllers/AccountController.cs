using Microsoft.AspNetCore.Mvc;
using PROG6212___CMCS___ST10082700.Models;
using PROG6212___CMCS___ST10082700.Services;
using Supabase;
using System.Collections.Generic;

namespace PROG6212___CMCS___ST10082700.Controllers
{
    public class AccountController : Controller
    {
        private readonly Client _supabaseClient;
        private readonly IConfiguration _configuration;

        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
            var url = configuration["Supabase:Url"];
            var key = configuration["Supabase:Key"];

            var options = new SupabaseOptions
            {
                AutoRefreshToken = true,
                AutoConnectRealtime = true
            };

            _supabaseClient = new Client(url, key, options);
        }

        // Add a GET method to render the login view
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                // Sign in with Supabase
                var session = await _supabaseClient.Auth.SignIn(model.Username, model.Password);

                // Store the session token
                HttpContext.Session.SetString("SupabaseToken", session.AccessToken);

                // Get user role from Supabase database
                var response = await _supabaseClient
                    .From<UserProfile>()
                    .Select("role")
                    .Match(new Dictionary<string, string> { { "email", model.Username } })
                    .Single();

                var userRole = response?.Role ?? "Lecturer"; // Default to Lecturer if role not found

                // Set welcome message based on role
                switch (userRole.ToLower())
                {
                    case "lecturer":
                        return RedirectToAction("Dashboard", "Lecturer");

                    case "coordinator":
                        TempData["WelcomeMessage"] = "Welcome Coordinator";
                        return RedirectToAction("Dashboard", "Admin");

                    case "admin":
                        TempData["WelcomeMessage"] = "Welcome Administrator";
                        return RedirectToAction("Dashboard", "Admin");

                    case "hr":
                        TempData["WelcomeMessage"] = "Welcome HR Manager";
                        return RedirectToAction("Dashboard", "Admin");

                    default:
                        ModelState.AddModelError(string.Empty, "Invalid role assigned to user");
                        return View(model);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Invalid login credentials");
                return View(model);
            }
        }

        // User Profile model to match Supabase database
        public class UserProfile : Supabase.Postgrest.Models.BaseModel
        {
            public string Email { get; set; }
            public string Role { get; set; }
        }
    }
}
