using Microsoft.AspNetCore.Http;
using Supabase;
using Supabase.Gotrue;

namespace PROG6212___CMCS___ST10082700.Services
{
    public class SupabaseAuthService
    {
        private readonly Supabase.Client _supabaseClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SupabaseAuthService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            var options = new SupabaseOptions
            {
                AutoRefreshToken = true,
                AutoConnectRealtime = true
            };

            _supabaseClient = new Supabase.Client(
                configuration["Supabase:Url"],
                configuration["Supabase:Key"],
                options
            );

            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Session> SignIn(string email, string password)
        {
            var response = await _supabaseClient.Auth.SignIn(email, password);
            return response;
        }

        public async Task SignOut()
        {
            await _supabaseClient.Auth.SignOut();
        }

        public async Task<User> GetUser()
        {
            // Retrieve the JWT token from the session using IHttpContextAccessor
            var jwtToken = _httpContextAccessor.HttpContext?.Session.GetString("SupabaseToken");

            if (string.IsNullOrEmpty(jwtToken))
            {
                throw new Exception("User is not authenticated. JWT token is missing.");
            }

            // Pass the JWT token to the GetUser() method
            return await _supabaseClient.Auth.GetUser(jwtToken);
        }
    }
}
