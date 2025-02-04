﻿using System.Net;
using System.Net.Mail;
using Topmass.Core.Model;
using Topmass.Recruiter.Repository;

namespace Topmass.Bussiness.Mail
{
    public partial class RecruitmentMailBussiness : IRecruitmentMailBussiness
    {

        private readonly IRecruiterRepository _candidateRepository;
        private readonly IActiveCodeRecruiterRepository _activeCodeRecruiterRepository;


        public RecruitmentMailBussiness(

                    IRecruiterRepository candidateRepository,
                    IActiveCodeRecruiterRepository activeCodeRecruiterRepository
            )
        {

            _candidateRepository = candidateRepository;
            _activeCodeRecruiterRepository = activeCodeRecruiterRepository;
        }

        private async Task SendMail(
            string content,
            string emailTo,
            string subjectTitle
            )
        {

            try
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



            }
            catch (Exception e)
            {


            }


        }
        public async Task<MailReponse> PushMail(MailItem mailItem)
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

        public async Task<ResultRequestSendMail> RecruitmentCheckMailPassword(string email, string code)
        {
            var reponse = new ResultRequestSendMail();
            var candidateInfo = await _candidateRepository
            .FindOneByStatementSql<RecruiterModel>
            (" select top 1 * from  Recruiter where email = @Email",
            new
            {
                Email = email
            });

            if (candidateInfo == null)
            {
                reponse.IsSucess = false;
                return reponse;
            }
            var pathTemplate = @"C:\vietbank\crm\topmass\Topmass.Bussiness.Mail\Template\NTD\resetPasswordTD.html";
            var contents = File.ReadAllText(pathTemplate);
            contents = contents.Replace("{fullName}", candidateInfo.Name);
            contents = contents.Replace("{baseUrl}", "https://tuyendung.topmass.vn");
            contents = contents.Replace("{codeGen}", code);
            var mailData = new MailItem()
            {
                Data = new DataMailInfo()
                {
                    Content = contents,
                    Subject = "Cấp lại mật khẩu – Topmass.vn"
                },
                MailTo = email

            };
            await PushMail(mailData);
            return reponse;
        }

        public async Task<ResultRequestSendMail> RecruitmentSuccessRegister(string email)
        {
            var reponse = new ResultRequestSendMail();
            var itemInfo = await _candidateRepository
            .FindOneByStatementSql<RecruiterModel>
            ("select top 1 * from  Recruiter where email = @Email",
            new
            {
                Email = email
            });

            if (itemInfo == null)
            {
                reponse.IsSucess = false;
                return reponse;
            }

            var codeGen = await _activeCodeRecruiterRepository
            .FindOneByStatementSql<ActiveCodeRecruiter>("select top 1 * from  ActiveCodeRecruiter where email = @Email", new
            {
                Email = email
            });

            if (codeGen == null)
            {
                reponse.IsSucess = false;
                return reponse;
            }

            var pathTemplate = @"C:\vietbank\crm\topmass\Topmass.Bussiness.Mail\Template\NTD\RegisterActiveNTD.html";
            var contents = File.ReadAllText(pathTemplate);
            contents = contents.Replace("{code}", codeGen.Code);
            contents = contents.Replace("{fullName}", itemInfo.Name);
            contents = contents.Replace("{email}", itemInfo.Email);
            var mailData = new MailItem()
            {
                Data = new DataMailInfo()
                {
                    Content = contents,
                    Subject = "Kích hoạt tài khoản Nhà tuyển dụng – Topmass.vn"
                },
                MailTo = email
            };
            await PushMail(mailData);
            return reponse;
        }


        public async Task<ResultRequestSendMail> RecruitmentSucessChangePassNoti(string email)
        {
            var reponse = new ResultRequestSendMail();
            var candidateInfo = await _candidateRepository
            .FindOneByStatementSql<RecruiterModel>
            (" select top 1 * from  Recruiter where email = @Email",
            new
            {
                Email = email
            });

            if (candidateInfo == null)
            {
                reponse.IsSucess = false;
                return reponse;
            }
            var pathTemplate = @"C:\vietbank\crm\topmass\Topmass.Bussiness.Mail\Template\\NTD\\notificationPassword.html";
            var contents = File.ReadAllText(pathTemplate);
            contents = contents.Replace("{fullName}", (candidateInfo.Name));

            var mailData = new MailItem()
            {
                Data = new DataMailInfo()
                {
                    Content = contents,
                    Subject = "Thông báo thay đổi mật khẩu thành công"
                },
                MailTo = email

            };
            await PushMail(mailData);
            return reponse;
        }
        public async Task<ResultRequestSendMail> NotifyJobApplyChangeStatus(
        string emailto,
        string companyName,
        string position,
        string fullName
        )
        {
            var reponse = new ResultRequestSendMail();
            var pathTemplate = @"C:\vietbank\crm\topmass\Topmass.Bussiness.Mail\Template\NTD\notifyWhenStatusChange.html";
            var contents = File.ReadAllText(pathTemplate);

            contents = contents.Replace("{CompanyName}", companyName);
            contents = contents.Replace("{positionJob}", position);
            contents = contents.Replace("{fullName}", fullName);
            var mailData = new MailItem()
            {
                Data = new DataMailInfo()
                {
                    Content = contents,
                    Subject = "Thông báo thay đổi trạng thái ứng tuyển – Topmass.vn"
                },
                MailTo = emailto

            };


            await PushMail(mailData);
            return reponse;
        }

    }
}
