using Topmass.Bussiness.Mail;
using Topmass.Core.Model;
using Topmass.Core.Repository;


namespace Topmass.core.Business
{
    public partial class CandidateMailBusiness : ICandidateMailBusiness
    {
        private readonly ICandidateRepository _repository;
        private readonly IForgetPasswordRepository _forgetPasswordRepository;
        private readonly ILogActionModelRepository _logActionModelRepository;
        private readonly IActiveCodeMemberRepository _activeCodeMemberRepository;

        private BusinessResourceMessage resourceMessage;

        private IMailBussiness _mailBussiness;
        public CandidateMailBusiness(ICandidateRepository userRepository,
            IForgetPasswordRepository forgetPasswordRepository,
            IMailBussiness mailBussiness,
            ILogActionModelRepository logActionModelRepository,
            IActiveCodeMemberRepository activeCodeMemberRepository
            )
        {
            _repository = userRepository;
            _forgetPasswordRepository = forgetPasswordRepository;
            resourceMessage = BusinessResourceMessage.GetMessage();
            _mailBussiness = mailBussiness;
            _logActionModelRepository = logActionModelRepository;
            _activeCodeMemberRepository = activeCodeMemberRepository;
        }
        public async Task<BaseResult> RequestMailValidAccount(string email)
        {
            var reponse = new LoginResult();
            var item = new ActiveCodeMember();
            var randomCode = "" + new Random().Next(1000, 10000) + DateTime.Now.Ticks + "";
            item.Code = randomCode;
            item.Email = email;
            item.Status = 1;
            item.CreateAt = DateTime.Now;
            var candidateInfo = await _repository
            .FindOneByStatementSql<CandidateModel>("select top 1 * from  Candidate where email = @Email", new
            {
                Email = email
            });
            if (candidateInfo == null)
            {
                reponse.Message = "Email không được đăng ký trên hệ thống";
                return reponse;
            }
            await _activeCodeMemberRepository.AddOrUPdate(item);
            await _mailBussiness.ValidateCandidateMail(email, randomCode);
            return reponse;
        }

        public async Task<BaseResult> HandleRequestPassword(string email)
        {
            //check email or username
            var reponse = new BaseResult();
            var candidateInfo = await _repository
                .FindOneByStatementSql<CandidateModel>
                ("select  top 1 * from Candidate where Email = @email", new
                {
                    email
                });
            //check 
            if (candidateInfo == null)
            {
                reponse.Message = "Email không được đăng ký trên hệ thống";
                return reponse;
            }
            // push event send email password

            var forgetPasswordRequest = new ForgetPasswordModel()
            {
                CreateAt = DateTime.Now,
                CreatedBy = 1,
                TypeUser = 0,
                UserId = candidateInfo.Id,
                Status = 0,
                SendMailStatus = 0,
                Deleted = false,
                UpdateAt = DateTime.Now,
                Email = email,
                UpdatedBy = 1,
            };
            var randomCode = "" + new Random().Next(1000, 10000) + DateTime.Now.Ticks + "";
            forgetPasswordRequest.Code = randomCode;
            await _forgetPasswordRepository.AddOrUPdate(forgetPasswordRequest);
            //reponse.Message = resourceMessage.Message_CheckMail;
            await _mailBussiness.CanddidateCheckMailPassword(candidateInfo.Email, randomCode);
            return reponse;

        }

    }
}
