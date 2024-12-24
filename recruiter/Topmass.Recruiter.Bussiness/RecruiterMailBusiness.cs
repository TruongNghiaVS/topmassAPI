using Topmass.Bussiness.Mail;
using Topmass.Core.Model;
using Topmass.Core.Repository;
using Topmass.Recruiter.Repository;
using TopMass.Core.Result;

namespace Topmass.Recruiter.Bussiness
{
    public partial class RecruiterMailBusiness : IRecruiterMail
    {
        private readonly IRecruiterRepository _repository;
        private readonly IRecruiterInfoRepository _infoRepository;
        private readonly IActiveCodeRecruiterRepository _activeCodeRecruiterRepository;
        private readonly IRecruitmentMailBussiness _mailBussiness;
        private readonly IBusinessLicenseLogRepository _businessLicenseLogRepository;
        private readonly IBusinessLicenseRepository _businessLicenseRepository;
        private readonly ICompanyInfoRepository _companyInfoRepository;
        private readonly IJobApplyRepository _jobApplyRepository;
        private readonly IJobRepository _jobRepository;
        public RecruiterMailBusiness(IRecruiterRepository userRepository,
             IActiveCodeRecruiterRepository activeCodeRecruiterRepository,
             IRecruiterInfoRepository infoRepository,
        IRecruitmentMailBussiness mailBussiness,
        IBusinessLicenseLogRepository businessLicenseLogRepository,
        IBusinessLicenseRepository businessLicenseRepository,
        ICompanyInfoRepository companyInfoRepository,
            IJobApplyRepository jobApplyRepository,
            IJobRepository jobRepository

            )
        {
            _repository = userRepository;
            _activeCodeRecruiterRepository = activeCodeRecruiterRepository;
            _mailBussiness = mailBussiness;
            _infoRepository = infoRepository;
            _businessLicenseLogRepository = businessLicenseLogRepository;
            _businessLicenseRepository = businessLicenseRepository;
            _companyInfoRepository = companyInfoRepository;
            _jobApplyRepository = jobApplyRepository;
            _jobRepository = jobRepository;
        }


        public async Task<BaseResult> RequestMailValidAccount(string email)
        {
            var reponse = new BaseResult();

            var itemInfo = await _repository.FindOneByStatementSql<RecruiterModel>("select * from Recruiter where Email = @email", new
            {
                email
            });
            //check 
            if (itemInfo == null)
            {
                reponse.Message = "Địa chỉ Email, không có trong hệ thống";
            }

            var item = new ActiveCodeRecruiter();
            var randomCode = "" + new Random().Next(1000, 10000) + DateTime.Now.Ticks + "";
            item.Code = randomCode;
            item.Email = email;
            item.Status = 1;
            item.CreateAt = DateTime.Now;
            await _activeCodeRecruiterRepository.AddOrUPdate(item);
            await _mailBussiness.RecruitmentSuccessRegister(item.Email);
            return reponse;

        }

        public async Task<BaseResult> NotificationMailWhenJobAplyStatusChange(int applyId)
        {
            var reponse = new BaseResult();
            var result = await _jobApplyRepository.GetById(applyId);
            if (result == null)
            {
                reponse.Message = "Không tồn tại thông tin";
                return reponse;
            }
            var jobId = result.JobId;
            var jobInfo = await _jobRepository.GetById(jobId);
            if (jobInfo == null)
            {
                reponse.Message = "Không tồn tại thông tin";
                return reponse;
            }
            var _companyInfo = await _companyInfoRepository.FindOneByStatementSql<CompanyInfoModel>("select top 1 * from CompanyInfo where RelId = @relId", new
            {
                relId = jobInfo.CreatedBy
            });
            if (jobInfo == null || _companyInfo == null)
            {
                reponse.Message = "Không tồn tại thông tin";
                return reponse;
            }
            var companyName = _companyInfo.FullName;
            var positionText = jobInfo.Name;
            var candidateInfo = await _companyInfoRepository.FindOneByStatementSql<CandidateModel>("select * from Candidate where id in ( select top 1  f.UserId  from jobApply e inner join resumes  f on e.CVId = f.id where e.id = @relid)", new
            {
                relId = applyId
            });
            var email = result.Email;
            if (string.IsNullOrEmpty(email))
            {
                email = candidateInfo.Email;
            }
            await _mailBussiness.NotifyJobApplyChangeStatus(email, companyName, positionText, candidateInfo.FirstName + " " + candidateInfo.FullName);
            return reponse;
        }
    }
}
