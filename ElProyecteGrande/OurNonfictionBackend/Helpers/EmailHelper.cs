using System.Net;
using System.Net.Mail;

namespace OurNonfictionBackend.Helpers
{
    public static class EmailHelper
    {
        private static void SendEmail(MailMessage message)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(Environment.GetEnvironmentVariable("nonfictionuser"), Environment.GetEnvironmentVariable("nonfictionpassword")),
                EnableSsl = true,
            };
            smtpClient.Send(message);
        }

        private static void CreateMessage(MailMessage mailMessage)
        {
            SendEmail(mailMessage);
        }

        public static void CreateWelcomeMessage(string userName, string email)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress("Nonfiction@ours.com"),
                Subject = "Successful Registration ",
                Body =
                    $"<h1>Hello {userName}!</h1><p>We pleasantly welcome you in the family of Our Nonfiction!</p><p>Best regard,</p><p>The team</p>",
                IsBodyHtml = true,
            };
            mailMessage.To.Add(email);
            CreateMessage(mailMessage);
        }

        public static void CreatePasswordRecoveryEmail(string email, string link,string username)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress("Nonfiction@ours.com"),
                Subject = "Password Change",
                Body =
                    $"<h1>Hello {username}!</h1><p>We heard you have problem with your password. Click on this <a href=\"{link}\">link</a> and change it! ;)</p><p>Best regard,</p><p>The team</p>",
                IsBodyHtml = true,
            };
            mailMessage.To.Add(email);
            CreateMessage(mailMessage);

        }

    }
}
