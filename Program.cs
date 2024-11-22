using Microsoft.EntityFrameworkCore;
using PROG6212___CMCS___ST10082700.Data;
using PROG6212___CMCS___ST10082700.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add DbContext configuration
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Register ClaimService
builder.Services.AddScoped<IClaimService, ClaimService>();

// Configure Session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(2); 
    options.Cookie.HttpOnly = true;              
    options.Cookie.IsEssential = true;         
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Enable session middleware
app.UseSession();

// Add Supabase token middleware
app.Use(async (context, next) =>
{
    var token = context.Session.GetString("SupabaseToken");
    if (!string.IsNullOrEmpty(token))
    {
        context.Request.Headers.Add("Authorization", $"Bearer {token}");
    }
    await next();
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
