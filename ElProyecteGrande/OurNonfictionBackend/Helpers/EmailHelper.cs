using System.Net.Mail;
using System.Net;

namespace OurNonfictionBackend.Helpers
{
    public static class EmailHelper
    {
        public static void SendEmail( string userName,string email)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(Environment.GetEnvironmentVariable("nonfictionuser"), Environment.GetEnvironmentVariable("nonfictionpassword")),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("Nonfiction@ours.com"),
                Subject = "Successful Registration ",
                Body = $"<h1>Hello {userName}!</h1><p>We pleasantly welcome you in the family of Our Nonfiction!</p><p>Best regard,</p><p>The team</p>",
                IsBodyHtml = true,
            };
            mailMessage.To.Add(email);

            smtpClient.Send(mailMessage);
        }
    }
}
