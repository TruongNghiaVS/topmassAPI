using System.Net;
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
            try
            {
                await smtp.SendMailAsync(message);
            }
            catch (Exception e)
            {
                throw;
            }

            return true;

        }
        public async Task<MailReponse> PushMail(MailItem mailItem)
        {
            if (mailItem == null ||
                 string.IsNullOrEmpty(mailItem.MailTo)
                 )
            {
                return new MailReponse();
            }
            await SendMail(mailItem.Data.Content, mailItem.MailTo, mailItem.Data.Subject);
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
                    Subject = "Yêu cầu Cấp lại mật khẩu"
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
            .FindOneByStatementSql<CandidateModel>
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
            var pathTemplate = @"C:\vietbank\crm\topmass\Topmass.Bussiness.Mail\Template\\NTD\\RegisterActiveNTD.html";
            var contents = File.ReadAllText(pathTemplate);
            contents = contents.Replace("{code}", codeGen.Code);
            contents = contents.Replace("{email}", itemInfo.Email);
            var mailData = new MailItem()
            {
                Data = new DataMailInfo()
                {
                    Content = contents,
                    Subject = "Kích hoạt tài khoản"
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
    }
}
