using Topmass.Bussiness.Mail;
using Topmass.core.Business.Model;
using Topmass.Core.Model;
using Topmass.Core.Repository;
using Topmass.Core.Repository.Model;


namespace Topmass.core.Business
{
    public partial class CandidateBusiness : ICandidateBusiness
    {
        private readonly ICandidateRepository _repository;
        private readonly ICandidateInfoRepository _candidateInfoRepository;
        private readonly IMailBussiness _mailBussiness;


        public CandidateBusiness(ICandidateRepository userRepository,
            IMailBussiness mailBussiness,
            ICandidateInfoRepository candidateInfoRepository
            )
        {
            _repository = userRepository;

            _mailBussiness = mailBussiness;
            _candidateInfoRepository = candidateInfoRepository;
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
            reponse.Message = "Cập nhật mật khẩu thành công";

            return reponse;
        }
        public async Task<BaseResult> UpdateInfoCandidate(CandidateInfoUpdate request)
        {
            var reponse = new UserResgisterResult();
            var candidateUpdate = await _repository.GetById(request.UserId.Value);

            if (candidateUpdate == null)
            {
                return reponse;
            }

            if (!string.IsNullOrEmpty(request.FirstName))
            {
                candidateUpdate.FirstName = request.FirstName;
            }
            if (!string.IsNullOrEmpty(request.LastName))
            {
                candidateUpdate.FullName = request.LastName;
            }

            if (!string.IsNullOrEmpty(request.Phone))
            {
                candidateUpdate.Phone = request.Phone;
            }
            candidateUpdate.UpdatedBy = request.HandleBy.Value;

            await _repository.AddOrUPdate(candidateUpdate);

            var candidateInfo = await _candidateInfoRepository.FindOneByStatementSql<CandidateInfoModel>("select top 1 * from CandidateInfo where email = @email", new { email = candidateUpdate.Email });

            if (candidateInfo == null)
            {
                return reponse;
            }

            if (!string.IsNullOrEmpty(request.AvatarLink))
            {
                candidateInfo.AvatarLink = request.AvatarLink;
            }
            if (request.PrivateMode.HasValue)
            {
                candidateInfo.PrivateMode = request.PrivateMode.Value;
            }

            if (request.PublicMode.HasValue)
            {
                candidateInfo.PublicMode = request.PublicMode.Value;

            }
            await _candidateInfoRepository.AddOrUPdate(candidateInfo);

            return reponse;
        }

        public async Task<UserResgisterResult> RegisterUser(CandidateRegisterRequest request)
        {
            var reponse = new UserResgisterResult();
            if (string.IsNullOrWhiteSpace(request.Email) ||
                string.IsNullOrWhiteSpace(request.Password)
                )
            {
                reponse.AddError("email", "Thiếu thông tin email hoặc mật khẩu");

            }

            if (string.IsNullOrWhiteSpace(request.FirstName) ||
           string.IsNullOrWhiteSpace(request.LastName)
           )
            {
                reponse.AddError("FirstName", "Thiếu thông tin họ và tên");

            }

            if (string.IsNullOrWhiteSpace(request.Phone)
            )
            {
                reponse.AddError("phone", "Thiếu thông tin số điện thoại");

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
            var candidateDup = await _repository
                .FindOneByStatementSql<CandidateModel>("select top 1 * from  Candidate where email = @Email", new
                {
                    itemInsert.Email
                });

            if (candidateDup != null)
            {
                reponse.AddError(nameof(itemInsert.Email), "Trùng lặp thông tin Email ");
            }
            var result = await _repository.AddUser(itemInsert);
            if (result == true)
            {
                await _mailBussiness.CandidateSuccessRegister(itemInsert.Email);
            }
            return reponse;
        }

        private string GetFullLink(string shortLink)
        {
            if (string.IsNullOrEmpty(shortLink))
            {
                return "";
            }
            return "https://www.cdn.topmass.vn/static/" + shortLink;
        }
        public async Task<BaseResult> GetInfo(CandidateInfoRequest request)
        {
            var reponse = new BaseResult();

            var itemInfoCan = new CandidateInfoResult();
            if (string.IsNullOrWhiteSpace(request.Email)
                )
            {
                return reponse;
            }
            var candidateItem = await _repository
            .FindOneByStatementSql<CandidateModel>("select top 1 * from  Candidate where email = @Email", new
            {
                request.Email
            });

            if (candidateItem == null)
            {
                return reponse;
            }
            var candidateItemInfo = await _repository
            .FindOneByStatementSql<CandidateInfoModel>("select top 1 * from  CandidateInfo where email = @Email", new
            {
                request.Email
            });
            itemInfoCan.AuthenticationLevelText = "Đã xác thực";
            itemInfoCan.Email = candidateItem.Email;
            itemInfoCan.FirstName = candidateItem.FirstName;
            itemInfoCan.LastName = candidateItem.FullName;

            itemInfoCan.UserName = candidateItem.Email;
            itemInfoCan.UserId = candidateItem.Id;
            itemInfoCan.Phone = candidateItem.Phone;
            if (candidateItemInfo != null)
            {
                itemInfoCan.WorkMode = candidateItemInfo.PrivateMode;
                itemInfoCan.SearchMode = candidateItemInfo.PublicMode;
                itemInfoCan.AvatarLink = GetFullLink(candidateItemInfo.AvatarLink);
                itemInfoCan.Status = candidateItemInfo.Status;

            }
            else
            {
                var itemCaIn = new CandidateInfoModel()
                {
                    Email = candidateItem.Email,
                    AvatarLink = "",
                    CreatedBy = candidateItem.Id,
                    PrivateMode = false,
                    PublicMode = true,
                    Status = 1,
                    UserId = candidateItem.Id,
                    UpdateAt = DateTime.Now,
                    CreateAt = DateTime.Now,
                    UpdatedBy = candidateItem.Id,
                    Deleted = false

                };
                await _candidateInfoRepository.AddOrUPdate(itemCaIn);
            }

            reponse.Data = itemInfoCan;
            return reponse;
        }

    }
}
