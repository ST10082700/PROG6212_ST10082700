using Supabase.Gotrue;
using Supabase;

namespace PROG6212___CMCS___ST10082700.Services
{
    public class SupabaseAuthService
    {
        private readonly Supabase.Client _supabaseClient;

        public SupabaseAuthService(IConfiguration configuration)
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
            return await _supabaseClient.Auth.GetUser();
        }
    }
}
