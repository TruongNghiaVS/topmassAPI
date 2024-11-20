using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Topmass.Bussiness.Mail;
using Topmass.Core.Model;
using Topmass.Core.Model.Campagn;
using Topmass.Core.Repository;
using Topmass.Recruiter.Repository;
using TopMass.Core.Result;

namespace Topmass.Recruiter.Bussiness
{
    public partial class AuthenBuisiness : IAuthenBuisiness
    {
        private readonly IRecruiterRepository _repository;

        private readonly IRecruiterInfoRepository _recruiterInfoRepository;
        private readonly IForgetPasswordRepository _forgetPasswordRepository;
        private readonly IRecruitmentMailBussiness _mailBussiness;
        private readonly ILogActionModelRepository _logActionModelRepository;

        private readonly IActiveCodeRecruiterRepository activeCodeRecruiterRepository;

        private BusinessResourceMessage resourceMessage;
        public AuthenBuisiness(IRecruiterRepository userRepository,
            IForgetPasswordRepository forgetPasswordRepository,
            ILogActionModelRepository logActionModelRepository,
            IRecruitmentMailBussiness recruitmentMailBussiness,
             IRecruiterInfoRepository recruiterInfoRepository,
        IActiveCodeRecruiterRepository _activeCodeRecruiterRepository
            )
        {
            _repository = userRepository;
            _forgetPasswordRepository = forgetPasswordRepository;
            resourceMessage = BusinessResourceMessage.GetMessage();
            _mailBussiness = recruitmentMailBussiness;
            activeCodeRecruiterRepository = _activeCodeRecruiterRepository;
            _recruiterInfoRepository = recruiterInfoRepository;
            _logActionModelRepository = logActionModelRepository;
        }
        public async Task<LoginResult> Login(string username, string password)
        {
            var result = new LoginResult();
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {

                result.AddError(resourceMessage.Missing_param);
            }

            var userInfo = await _repository.FindUser(new AuthenRepSearch()
            {
                Email = username,
                Password = password
            });

            if (userInfo == null || userInfo.Id < 1)
            {

                result.Message = resourceMessage.Authen_NotFoundAccout;
                result.AddError("Email", resourceMessage.Authen_NotFoundAccout);
                return result;
            }

            if (userInfo.LevelAuthen == 0)
            {

                result.Message = "Tài khoản chưa kích hoạt email";
                result.AddError("Email", "Tài khoản chưa kích hoạt email");
                return result;
            }


            if (userInfo.Status == 0)
            {

                result.Message = "Tài khoản đang bị khoá. vui lòng liên hệ bộ phận admin";
                result.AddError("Email", "Tài khoản đang bị khoá. vui lòng liên hệ bộ phận admin");
                return result;
            }

            if (userInfo.Status == 2)
            {

                result.Message = "Tài khoản đang bị khoá. vui lòng liên hệ bộ phận admin";
                result.AddError("Email", "Tài khoản đang bị khoá. vui lòng liên hệ bộ phận admin");
                return result;
            }
            result.Message = resourceMessage.SuccessfullAuthenMsg;

            var itemInsert = new LogActionModel
            {
                Actor = 1,
                TypeData = 1,
                Source = 2,
                BusinessTime = DateTime.Now,
                UserId = userInfo.Id,
                Content = "Đăng nhập",

            };
            await _logActionModelRepository.AddOrUPdate(itemInsert);
            var tokenUser = this.GenerateToken(userInfo);
            result.Token = tokenUser;
            result.Message = resourceMessage.SuccessAuthenCreateToken;
            return result;
        }
        protected string GenerateToken(RecruiterModel request)
        {


            var issuer = "topmass.vn";
            var audience = "nguyentruongnghia";
            var key = Encoding.ASCII.GetBytes("product of vietstargroups");
            var userName = request.UserName;
            var email = request.Email;
            var fullName = request.Name;
            var idRequest = request.Id;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                        new Claim("userId", idRequest.ToString()),
                        new Claim("userName", userName),
                        new Claim("fullName", fullName)


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

        public async Task<UserResgisterResult> RegisterUser(RecruiterRegisterRequest request)
        {
            var reponse = new UserResgisterResult();
            if (string.IsNullOrWhiteSpace(request.Email) ||
                string.IsNullOrWhiteSpace(request.Password)
                )
            {
                reponse.AddError(resourceMessage.Missing_param_Email);

            }
            if (string.IsNullOrWhiteSpace(request.Name)
           )
            {
                reponse.AddError(resourceMessage.Missing_param_Name);

            }

            if (string.IsNullOrWhiteSpace(request.Phone)
            )
            {
                reponse.AddError(resourceMessage.Missing_param_PhoneNumber);

            }
            var itemInsert = new RecruiterRepAdd()
            {
                UserName = request.Email,
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
                TaxCode = request.TaxCode,
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
            var itemInfo = await _repository.FindOneByStatementSql<RecruiterModel>("select * from Recruiter where Email = @email", new
            {
                email
            });
            //check 
            if (itemInfo == null)
            {
                reponse.AddError(resourceMessage.Email_notExit_System);
                return reponse;
            }
            // push event send email password

            var forgetPasswordRequest = new ForgetPasswordModel()
            {
                CreateAt = DateTime.Now,
                CreatedBy = 1,
                TypeUser = 1,
                UserId = itemInfo.Id,
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
            await _mailBussiness.RecruitmentCheckMailPassword(itemInfo.Email, randomCode);
            return reponse;
        }
        public async Task<LoginResult> ConfirmHasValidResetPasswordLink(string code)
        {
            var reponse = new LoginResult();
            var codeForgetLink = await _repository
            .FindOneByStatementSql<ForgetPasswordModel>("select * from ForgetPassword where Code = @code and TypeUser = 1 ", new
            {
                code
            });
            if (codeForgetLink == null)
            {
                reponse.AddError(resourceMessage.ValidLinkFail);
                return reponse;
            }

            var infoItem = await _repository.GetById(codeForgetLink.UserId.Value);
            if (infoItem == null)
            {
                reponse.AddError(resourceMessage.NotExitAccount);
                return reponse;
            }
            reponse.Token = GenerateToken(infoItem);
            codeForgetLink.Status = 1;
            await _forgetPasswordRepository.AddOrUPdate(codeForgetLink);
            return reponse;
        }


        public async Task<LoginResult> ConfirmToValidAccoutByCode(string code)
        {
            var reponse = new LoginResult();
            var codeForgetLink = await activeCodeRecruiterRepository
            .FindOneByStatementSql<ActiveCodeRecruiter>("select * from ActiveCodeRecruiter where Code = @code  ", new
            {
                code
            });
            if (codeForgetLink == null)
            {
                reponse.AddError(resourceMessage.ValidLinkFail);
                return reponse;
            }

            var itemRecru = await _repository.FindOneByStatementSql<RecruiterModel>("select top 1 * from Recruiter where Email = @email",
                new
                {
                    email = codeForgetLink.Email
                }
                );

            if (itemRecru != null)
            {
                itemRecru.Status = 1;
                itemRecru.UpdateAt = DateTime.Now;
                await _repository.AddOrUPdate(itemRecru);
            }
            codeForgetLink.Status = 2;// request
            codeForgetLink.UpdateAt = DateTime.Now;



            var itemRecuInfo = await _recruiterInfoRepository.FindOneByStatementSql<RecruiterInfoModel>("select top 1 * from RecruiterInfo where Email = @email",
               new
               {
                   email = codeForgetLink.Email
               }
               );

            if (itemRecuInfo != null)
            {
                itemRecuInfo.LevelAuthen = 1;
                itemRecuInfo.DateActive = DateTime.Now;
                itemRecuInfo.Status = 1;

                await _recruiterInfoRepository.AddOrUPdate(itemRecuInfo);
            }
            await activeCodeRecruiterRepository.AddOrUPdate(codeForgetLink);

            return reponse;
        }
        public async Task<BaseResult> ChangePassword(string password, int userId)
        {
            var reponse = new BaseResult();
            var infoItem = await _repository.GetById(userId);
            infoItem.Password = password;
            infoItem.UpdateAt = DateTime.Now;
            await _repository.AddOrUPdate(infoItem);
            reponse.Message = resourceMessage.ChangePasswordSuccess;

            await _mailBussiness.RecruitmentSucessChangePassNoti(infoItem.Email);
            return reponse;
        }


        public async Task<BaseResult> ChangePasswordNotMail(string password, int userId)
        {
            var reponse = new BaseResult();
            var infoItem = await _repository.GetById(userId);
            infoItem.Password = password;
            infoItem.UpdateAt = DateTime.Now;
            await _repository.AddOrUPdate(infoItem);
            reponse.Message = resourceMessage.ChangePasswordSuccess;
            return reponse;
        }
    }
}
