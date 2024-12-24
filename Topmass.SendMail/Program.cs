using System.Diagnostics;
using System.Net;
using System.Net.Mail;

namespace Topmass.SendMail
{
    internal class Program
    {

        private static void SendMail(
          string content,
          string emailTo,
          string subjectTitle
          )
        {
            try
            {
                var Port = 587;
                var MailFrom = "noreply@topmass.vn";
                var Host = "mail9066.maychuemail.com";
                var userName = "noreply@topmass.vn";
                var password = "Topmass!@#$2024";
                var contents = content;
                var mailFrom = MailFrom;
                var mailTo = emailTo;
                var subjectInfo = subjectTitle;
                var bodyContent = contents;
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(contents, null, "text/html");
                LinkedResource imageResource = new LinkedResource("C:\\vietbank\\crm\\topmass\\Topmass.Bussiness.Mail\\Template\\mailLogo.png");
                imageResource.ContentId = "imageLogo";
                htmlView.LinkedResources.Add(imageResource);
                message.AlternateViews.Add(htmlView);
                message.From = new MailAddress(mailFrom, "topmass.vn");
                message.To.Add(new MailAddress(mailTo));
                message.Subject = subjectInfo;
                message.IsBodyHtml = true;
                message.Body = bodyContent;
                smtp.Port = Port;
                smtp.Host = Host;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(userName, password);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
                Console.WriteLine("thanh cong");
                //Thread.CurrentThread.Abort();

            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
        }

        private static void SendMailThread()
        {
            SendMail("nghiatest", "nghiait06@gmail.com", "tin tuc");
        }
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < 2; i++)
            {
                var thread = new Thread(() => SendMailThread());
                thread.Start();
            }
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);
        }
    }
}
