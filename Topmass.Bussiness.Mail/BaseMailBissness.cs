﻿using System.Net;
using System.Net.Mail;

namespace Topmass.Bussiness.Mail
{
    public partial class BaseMailBissness
    {
        public BaseMailBissness()
        {

        }

        private async Task<bool> SendMail(
            string content,
            string emailTo,
            string subjectTitle
            )
        {

            var mailconfig = new
            {
                MailConfigValue.Port,
                MailConfigValue.MailFrom,
                MailConfigValue.Host,
                MailConfigValue.userName,
                MailConfigValue.password
            };
            var contents = content;
            var mailFrom = mailconfig.MailFrom;
            var mailTo = emailTo;
            var subjectInfo = subjectTitle;
            var bodyContent = contents;
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            message.From = new MailAddress(mailFrom, "topmass.vn");
            message.To.Add(new MailAddress(mailTo));
            message.Subject = subjectInfo;
            message.IsBodyHtml = true;
            message.Body = bodyContent;
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(contents, null, "text/html");
            LinkedResource imageResource = new LinkedResource("C:\\vietbank\\crm\\topmass\\Topmass.Bussiness.Mail\\Template\\mailLogo.png");
            imageResource.ContentId = "imageLogo";
            htmlView.LinkedResources.Add(imageResource);
            message.AlternateViews.Add(htmlView);
            smtp.Port = mailconfig.Port;
            smtp.Host = mailconfig.Host;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(mailconfig.userName, mailconfig.password);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            await smtp.SendMailAsync(message);
            return true;
        }
        protected async Task<MailReponse> PushMail(MailItem mailItem)
        {
            if (mailItem == null ||
                 string.IsNullOrEmpty(mailItem.MailTo)
                 )
            {
                return new MailReponse();
            }
            var thread = new Thread(async () => await SendMail(mailItem.Data.Content, mailItem.MailTo, mailItem.Data.Subject));
            thread.Start();
            return new MailReponse();
        }

    }
}
