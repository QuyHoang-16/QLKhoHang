using Microsoft.AspNetCore.Identity;
using QuanLyKho.Models;
using QuanLyKho.Models.Invent;

namespace QuanLyKho.Services
{
    public interface INetcoreService
    {
        Task SendEmailBySendGridAsync(string apiKey, string fromEmail, string fromFullName, string subject, string message, string email);

        Task<bool> IsAccountActivatedAsync(string email, UserManager<ApplicationUser> userManager);

        Task SendEmailByGmailAsync(string fromEmail,
            string fromFullName,
            string subject,
            string messageBody,
            string toEmail,
            string toFullName,
            string smtpUser,
            string smtpPassword,
            string smtpHost,
            int smtpPort,
            bool smtpSSL);

        Task<string> UploadFile(List<IFormFile> files, Microsoft.AspNetCore.Hosting.IHostingEnvironment env);

        Task UpdateRoles(ApplicationUser appUser, ApplicationUser currentUserLogin);

        Task CreateDefaultSuperAdmin();

        VMStock GetStockByProductAndWarehouse(string productId, string warehouseId);

        List<VMStock> GetStockPerWarehouse();

        Task InitDemo();
    }
}
