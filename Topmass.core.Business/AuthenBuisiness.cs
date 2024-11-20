using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Topmass.Bussiness.Mail;
using Topmass.core.Business.Model;
using Topmass.Core.Model;
using Topmass.Core.Model.Campagn;
using Topmass.Core.Repository;
using Topmass.Core.Repository.Model;


namespace Topmass.core.Business
{
    public partial class AuthenBuisiness : IAuthenBuisiness
    {
        private readonly ICandidateRepository _repository;
        private readonly IForgetPasswordRepository _forgetPasswordRepository;
        private readonly ILogActionModelRepository _logActionModelRepository;

        private BusinessResourceMessage resourceMessage;

        private IMailBussiness _mailBussiness;
        public AuthenBuisiness(ICandidateRepository userRepository,
            IForgetPasswordRepository forgetPasswordRepository,
            IMailBussiness mailBussiness,
            ILogActionModelRepository logActionModelRepository
            )
        {
            _repository = userRepository;
            _forgetPasswordRepository = forgetPasswordRepository;
            resourceMessage = BusinessResourceMessage.GetMessage();
            _mailBussiness = mailBussiness;
            _logActionModelRepository = logActionModelRepository;
        }
        public async Task<LoginResult> LoginCandidate(string username, string password)
        {
            var result = new LoginResult();
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {

                result.AddError(resourceMessage.Missing_param);
            }

            var userInfo = await _repository.FindUser(new CandidateSearch()
            {
                Email = username,
                Password = password
            });

            if (userInfo == null || userInfo.Id < 1)
            {

                result.Message = resourceMessage.Authen_NotFoundAccout;
                result.AddError(resourceMessage.Authen_NotFoundAccout);
                return result;
            }
            var itemInsert = new LogActionModel
            {
                Actor = 1,
                TypeData = 1,
                Source = 1,
                BusinessTime = DateTime.Now,
                UserId = userInfo.Id,
                Content = "Đăng nhập",

            };
            await _logActionModelRepository.AddOrUPdate(itemInsert);

            result.Message = resourceMessage.SuccessfullAuthenMsg;
            var userAuthorize = userInfo as BaseUserInfo;
            var tokenUser = this.GenerateToken(userAuthorize);
            result.Token = tokenUser;

            result.Message = resourceMessage.SuccessAuthenCreateToken;
            return result;
        }
        protected string GenerateToken(BaseUserInfo request, int typeUser = (int)TypeUser.CANDIDATE)
        {
            var issuer = "jobvieclam.com";
            var audience = "nguyentruongnghia";
            var key = Encoding.ASCII.GetBytes("product of vietstargroup");
            var userName = request.UserName;
            var email = request.Email;
            var firstName = request.FirstName;
            var fullName = request.FullName;
            var idRequest = request.Id;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                        new Claim("userId", idRequest.ToString()),
                        new Claim("userName", userName),

                        new Claim("fullName", fullName),
                        new Claim("firstName", firstName)
                }),
                Expires = DateTime.UtcNow.AddMinutes(60),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(key),
                SecurityAlgorithms.Aes128CbcHmacSha256)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;

        }

        public async Task<UserResgisterResult> RegisterUser(CandidateRegisterRequest request)
        {
            var reponse = new UserResgisterResult();
            if (string.IsNullOrWhiteSpace(request.Email) ||
                string.IsNullOrWhiteSpace(request.Password)
                )
            {
                reponse.AddError(resourceMessage.Missing_param_Email);

            }
            if (string.IsNullOrWhiteSpace(request.FirstName) ||
           string.IsNullOrWhiteSpace(request.LastName)
           )
            {
                reponse.AddError(resourceMessage.Missing_param_Name);

            }

            if (string.IsNullOrWhiteSpace(request.Phone)
            )
            {
                reponse.AddError(resourceMessage.Missing_param_PhoneNumber);

            }
            var itemInsert = new CandidateAdd()
            {
                UserName = request.Email,
                FirstName = request.FirstName,
                FullName = request.LastName,
                Email = request.Email,
                Phone = request.Phone,
                CreatedBy = 1,
                Pass = request.Password
            };
            await _repository.AddUser(itemInsert);
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
                reponse.AddError(resourceMessage.Email_notExit_System);
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
            reponse.Message = resourceMessage.Message_CheckMail;
            await _mailBussiness.CanddidateCheckMailPassword(candidateInfo.Email, randomCode);
            return reponse;

        }

        public async Task<LoginResult> ConfirmHasValidResetPasswordLink(string code)
        {
            var reponse = new LoginResult();
            var codeForgetLink = await _repository
            .FindOneByStatementSql<ForgetPasswordModel>("select * from ForgetPassword where Code = @code", new
            {
                code
            });
            if (codeForgetLink == null)
            {
                reponse.AddError(resourceMessage.ValidLinkFail);
                return reponse;
            }

            var candidateInfo = await _repository.GetById(codeForgetLink.UserId.Value);
            if (codeForgetLink == null)
            {
                reponse.AddError(resourceMessage.NotExitAccount);
                return reponse;
            }
            var userBasic = candidateInfo as BaseUserInfo;

            reponse.Token = GenerateToken(userBasic);
            codeForgetLink.Status = 1;
            await _forgetPasswordRepository.AddOrUPdate(codeForgetLink);
            return reponse;
        }

        public async Task<BaseResult> ChangePassword(string password, int userId)
        {
            var reponse = new BaseResult();
            var candidateInfo = await _repository.GetById(userId);
            if (candidateInfo == null)
            {
                return reponse;
            }
            candidateInfo.Password = password;
            candidateInfo.UpdateAt = DateTime.Now;
            await _repository.AddOrUPdate(candidateInfo);
            reponse.Message = resourceMessage.ChangePasswordSuccess;
            if (reponse.Success)
            {
                await _mailBussiness.CandidateSucessChangePassNoti(candidateInfo.Email);
            }

            return reponse;
        }
    }
}
