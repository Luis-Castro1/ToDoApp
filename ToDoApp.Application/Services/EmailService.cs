using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using ToDoApp.Application.Interfaces;
namespace ToDoApp.Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _emailConfiguration;

        public EmailService(IConfiguration configuration)
        {
            _emailConfiguration = configuration.GetSection("Email").GetSection("Smtp");
        }
        async Task IEmailService.SendEmailAsync(string to, string subject, string body)
        {
            var smtpHost = _emailConfiguration["Host"];
            var smtpPort = int.Parse(_emailConfiguration["Port"]);
            var smtpUser = _emailConfiguration["User"];
            var smtpPass = _emailConfiguration["Password"];
            var from = _emailConfiguration["From"];

            using var client = new SmtpClient(smtpHost, smtpPort)
            {
                Credentials = new NetworkCredential(smtpUser, smtpPass),
                EnableSsl = true
            };

            var mailMessage = new MailMessage(from, to, subject, body)
            {
                IsBodyHtml = true
            };

            await client.SendMailAsync(mailMessage);
        }
    }
}
