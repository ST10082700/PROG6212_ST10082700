using Microsoft.AspNetCore.Mvc;
using PROG6212___CMCS___ST10082700.Models;
using PROG6212___CMCS___ST10082700.Services;


namespace  PROG6212___CMCS___ST10082700.Controllers 
{
    public class AccountController : Controller
    {
        private readonly SupabaseAuthService _authService;

        public AccountController(SupabaseAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            try
            {
                var session = await _authService.SignIn(model.Email, model.Password);

                // Store the session token in a cookie or session
                HttpContext.Session.SetString("AuthToken", session.AccessToken);

                // Redirect based on user role
                var user = await _authService.GetUser();
                var userRole = await DetermineUserRole(user);

                return RedirectToAction("Index", userRole);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(model);
            }
        }
    }
}