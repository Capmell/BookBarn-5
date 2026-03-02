using Microsoft.AspNetCore.Identity.UI.Services;

namespace BookBarn.Services
{
    // Demo-only: prevents Identity UI registration from failing.
    // This course demo does not send real emails.
    public class DevEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Task.CompletedTask;
        }
    }
}