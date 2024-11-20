using System.Net;
using System.Net.Mail;


namespace sendmail
{
    public class SendMail
    {

        public void Send(MailItem mailItem)
        {
            if (mailItem == null ||
                string.IsNullOrEmpty(mailItem.MailTo)
                )
            {
                return;
            }

            var mailData = mailItem.Data;
            var mailconfig = new
            {
                Port = 587,
                mailFrom = "noreply@jobvieclam.com",
                Host = "mail9066.maychuemail.com",
                userName = "noreply@jobvieclam.com",
                password = "Jobvieclam@2024"
            };
            var dataText = new
            {
                name = "Nguyễn Trường Nghĩa",
                link = "dddd"
            };
            //string contents = File.ReadAllText(@"C:\vietbank\crm\topmass\sendmail\Template\RegisterSuccessNTD.html");
            var contents = mailData.Content;
            contents = contents.Replace("{fullName}", dataText.name);
            var mailFrom = mailconfig.mailFrom;
            var mailTo = mailItem.MailTo;
            var subjectInfo = mailData.Subject;
            var bodyContent = contents;
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            message.From = new MailAddress(mailFrom);
            message.To.Add(new MailAddress(mailTo));
            message.Subject = subjectInfo;
            message.IsBodyHtml = true;
            message.Body = bodyContent;
            smtp.Port = mailconfig.Port;
            smtp.Host = mailconfig.Host;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(mailconfig.userName, mailconfig.password);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);
        }
    }
}
