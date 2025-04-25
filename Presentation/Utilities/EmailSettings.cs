using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net;
using System.Net.Mail;

namespace Presentation.Utilities
{
    public static class EmailSettings
    {
        public static void SendEmail(Email email)
        {
            var Client = new SmtpClient(" smtp.gmail.com",587);
            Client.EnableSsl = true;
            Client.Credentials = new NetworkCredential("themohamedgouda@gmail.com", "wdifosagygtwcxdg\r\n");
            Client.Send("themohamedgouda@gmail.com", email.To, email.Subject, email.Body);
            
        }
    }
}
