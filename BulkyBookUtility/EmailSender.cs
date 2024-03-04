using Microsoft.AspNetCore.Identity.UI.Services;

namespace BulkyBookUtility
{
    public class EmailSender : IEmailSender
    {
        // Method to send an email asynchronously
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Task.CompletedTask;
        }
    }
}
