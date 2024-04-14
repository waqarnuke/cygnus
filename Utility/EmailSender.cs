using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;

namespace Utility
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var mail = "testcygnus@outlook.com";
            var pw = "Gamma@346**";
            var client =  new SmtpClient("smtp-mail.outlook.com",587)
            {
                EnableSsl =true,
                Credentials=new NetworkCredential(mail,pw)
            };

            return client.SendMailAsync(
                new MailMessage(mail, to:email, subject,htmlMessage));
        }
    }
}