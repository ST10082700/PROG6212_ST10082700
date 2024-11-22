using System.Security.Claims;

namespace PROG6212___CMCS___ST10082700.Services
{
    public class SupabaseAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public SupabaseAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, SupabaseAuthService authService)
        {
            var token = context.Session.GetString("AuthToken");

            if (!string.IsNullOrEmpty(token))
            {
                var user = await authService.GetUser();
                if (user != null)
                {
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Email, user.Email)
                };

                    var identity = new ClaimsIdentity(claims, "Supabase");
                    context.User = new ClaimsPrincipal(identity);
                }
            }

            await _next(context);
        }
    }
}
