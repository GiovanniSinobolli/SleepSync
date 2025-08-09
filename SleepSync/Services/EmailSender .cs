namespace SleepSync.Services
{
    using Microsoft.AspNetCore.Identity.UI.Services;
    using System.Threading.Tasks;

    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // no operation — just return completed task
            return Task.CompletedTask;
        }
    }
}
