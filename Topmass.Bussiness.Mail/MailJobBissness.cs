using System.Text.Json;
using Topmass.Core.Model;
using Topmass.Core.Model.Campagn;
using Topmass.Core.Repository;
namespace Topmass.Bussiness.Mail
{
    public partial class MailJobBissness : BaseMailBissness, IMailJobBissness
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly IJobInfoRepository _jobInfoRepository;
        public MailJobBissness(ICandidateRepository candidateRepository,
                    IJobInfoRepository jobInfoRepository
            )
        {
            _candidateRepository = candidateRepository;
            _jobInfoRepository = jobInfoRepository;
        }

        public async Task<ResultNotficationRecruiterWhenHasApplyRequest>
            NotficationRecruiterWhenHasApply(NotficationRecruiterWhenHasApplyRequest request)
        {
            var reponse = new ResultNotficationRecruiterWhenHasApplyRequest();
            var jobInfo = await _jobInfoRepository
            .FindOneByStatementSql<JobInfoModel>
            ("select top 1 * from  jobInfo where JobId = @JobId",
            new
            {
                request.JobId
            });
            var candatidateInfo = await _jobInfoRepository
            .FindOneByStatementSql<CandidateModel>
            ("select * from Candidate where id  = @id",
            new
            {
                id = request.UserId
            });
            if (jobInfo == null || candatidateInfo == null)
            {
                reponse.IsSucess = false;
                return reponse;
            }
            var mails = new List<EmailProper>();
            if (!string.IsNullOrEmpty(jobInfo.Emails))
            {
                mails = JsonSerializer.Deserialize<List<EmailProper>>(jobInfo.Emails);
            }
            var recurInfo = await _jobInfoRepository
            .FindOneByStatementSql<RecruiterModel>
            ("select * from Recruiter where id = @id",
            new
            {
                id = jobInfo.CreatedBy
            });

            var pathTemplate = @"C:\vietbank\crm\topmass\Topmass.Bussiness.Mail\Template\\notifyHasApply.html";
            var contents = File.ReadAllText(pathTemplate);
            var fullNameEmail = request.NameInput;
            if (string.IsNullOrEmpty(fullNameEmail))
            {
                fullNameEmail = candatidateInfo.FirstName + " " + candatidateInfo.FullName;
            }
            contents = contents.Replace("{useName}", fullNameEmail);
            contents = contents.Replace("{jobName}", (jobInfo.Name));
            contents = contents.Replace("{companyName}", (recurInfo.Name));
            contents = contents.Replace("{Introduction}", (request.Introduction));
            foreach (var item in mails)
            {
                var mailData = new MailItem()
                {
                    Data = new DataMailInfo()
                    {
                        Content = contents,
                        Subject = "Thông báo lượt ứng tuyển mới – Topmass.vn"
                    },
                    MailTo = item.Email
                };
                await PushMail(mailData);
            }
            return reponse;
        }
    }
}
