using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QuanLyKho.Data;
using QuanLyKho.Models;
using QuanLyKho.Services;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.Configure<SuperAdminDefaultOptions>(builder.Configuration.GetSection("SuperAdminDefaultOptions"));
builder.Services.Configure<SendGridOptions>(builder.Configuration.GetSection("SendGridOptions"));
builder.Services.Configure<SmtpOptions>(builder.Configuration.GetSection("SmtpOptions"));

var identityOptions = builder.Configuration
    .GetSection("IdentityDefaultOptions")
    .Get<IdentityDefaultOptions>();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = identityOptions?.PasswordRequireDigit ?? false;
    options.Password.RequiredLength = identityOptions?.PasswordRequiredLength ?? 6;
    options.Password.RequireNonAlphanumeric = identityOptions?.PasswordRequireNonAlphanumeric ?? false;
    options.Password.RequireUppercase = identityOptions?.PasswordRequireUppercase ?? false;
    options.Password.RequireLowercase = identityOptions?.PasswordRequireLowercase ?? false;
    options.Password.RequiredUniqueChars = identityOptions?.PasswordRequiredUniqueChars ?? 1;

    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(identityOptions?.LockoutDefaultLockoutTimeSpanInMinutes ?? 30);
    options.Lockout.MaxFailedAccessAttempts = identityOptions?.LockoutMaxFailedAccessAttempts ?? 5;
    options.Lockout.AllowedForNewUsers = identityOptions?.LockoutAllowedForNewUsers ?? true;

    options.User.RequireUniqueEmail = identityOptions?.UserRequireUniqueEmail ?? true;
    options.SignIn.RequireConfirmedEmail = identityOptions?.SignInRequireConfirmedEmail ?? false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = identityOptions?.CookieHttpOnly ?? true;
    var cookieExpirationMinutes = identityOptions?.CookieExpiration ?? 60;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(cookieExpirationMinutes);
    options.LoginPath = identityOptions?.LoginPath ?? "/Account/Login";
    options.LogoutPath = identityOptions?.LogoutPath ?? "/Account/Logout";
    options.AccessDeniedPath = identityOptions?.AccessDeniedPath ?? "/Account/AccessDenied";
    options.SlidingExpiration = identityOptions?.SlidingExpiration ?? true;
});

builder.Services.AddScoped<INetcoreService, NetcoreService>();
builder.Services.AddScoped<IRoles, Roles>();
builder.Services.AddTransient<IEmailSender, EmailSender>();

var app = builder.Build();

// Initialize database and seed data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var netcoreService = services.GetRequiredService<INetcoreService>();
        
        DbInitializer.Initialize(context, userManager, roleManager, netcoreService).Wait();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Map attribute-routed API controllers
app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
