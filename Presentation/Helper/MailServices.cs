using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using Presentation.Settings;
using Presentation.Utilities;
using System.Net.Mail;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace Presentation.Helper
{
    public class MailServices(IOptions<MailSettings> options) : IMailServices
    {
        public void Send(Email email)
        {
            var mail = new MimeMessage()
            { 
                Sender = MailboxAddress.Parse(options.Value.Email),
                Subject = email.Subject,
            };
            mail.To.Add(MailboxAddress.Parse(email.To));

            mail.From.Add(new MailboxAddress(options.Value.Email ,options.Value.DisplayName));

            var builder = new BodyBuilder();

            builder.TextBody = email.Body;

            mail.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();

            smtp.Connect(options.Value.Host, options.Value.Port,MailKit.Security.SecureSocketOptions.StartTls);

            smtp.Authenticate(options.Value.Email , options.Value.Password);

            smtp.Send(mail);

            smtp.Dispose();
        }
    }
}
