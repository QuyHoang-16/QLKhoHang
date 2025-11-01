using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuanLyKho.Models;
using QuanLyKho.Services;

namespace QuanLyKho.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            INetcoreService netcoreService)
        {
            try
            {
                // Ensure database is created
                context.Database.EnsureCreated();

                // Run pending migrations
                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }

                // Check if super admin already exists
                var superAdminEmail = "super@admin.com"; // Default from appsettings.json
                var superAdmin = await userManager.FindByEmailAsync(superAdminEmail);

                if (superAdmin == null)
                {
                    // Create default super admin
                    await netcoreService.CreateDefaultSuperAdmin();
                }

                // Note: InitDemo() can be called separately if needed for demo data
                // await netcoreService.InitDemo();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while seeding the database: {ex.Message}", ex);
            }
        }
    }
}

