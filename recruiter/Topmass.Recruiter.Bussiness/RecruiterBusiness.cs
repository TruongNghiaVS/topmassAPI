using Topmass.Bussiness.Mail;
using Topmass.Core.Model;
using Topmass.Core.Repository;
using Topmass.Recruiter.Bussiness.Model;
using Topmass.Recruiter.Repository;
using Topmass.Utility;
using TopMass.Core.Result;

namespace Topmass.Recruiter.Bussiness
{
    public partial class RecruiterBusiness : IRecruiterBusiness
    {
        private readonly IRecruiterRepository _repository;
        private readonly IRecruiterInfoRepository _infoRepository;
        private readonly IActiveCodeRecruiterRepository _activeCodeRecruiterRepository;
        private readonly IRecruitmentMailBussiness _mailBussiness;

        private readonly IBusinessLicenseLogRepository _businessLicenseLogRepository;

        private readonly IBusinessLicenseRepository _businessLicenseRepository;

        private readonly ICompanyInfoRepository _companyInfoRepository;

        public RecruiterBusiness(IRecruiterRepository userRepository,
             IActiveCodeRecruiterRepository activeCodeRecruiterRepository,
             IRecruiterInfoRepository infoRepository,
        IRecruitmentMailBussiness mailBussiness,
        IBusinessLicenseLogRepository businessLicenseLogRepository,
        IBusinessLicenseRepository businessLicenseRepository,
        ICompanyInfoRepository companyInfoRepository

            )
        {
            _repository = userRepository;
            _activeCodeRecruiterRepository = activeCodeRecruiterRepository;
            _mailBussiness = mailBussiness;
            _infoRepository = infoRepository;
            _businessLicenseLogRepository = businessLicenseLogRepository;
            _businessLicenseRepository = businessLicenseRepository;
            _companyInfoRepository = companyInfoRepository;
        }

        private string GetFullLink(string shortLink)
        {
            if (string.IsNullOrEmpty(shortLink) || string.IsNullOrWhiteSpace(shortLink))
            {
                return string.Empty;
            }
            return "https://www.cdn.topmass.vn/static/" + shortLink;

        }
        private string GetValueString(string valueString)
        {
            if (string.IsNullOrEmpty(valueString) || string.IsNullOrWhiteSpace(valueString))
            {
                return string.Empty;
            }
            return valueString;

        }

        public async Task<BaseResult> GetInfo(RecruiterInfoRequest request)
        {
            var reponse = new BaseResult();
            var result = new RecruiterInfoResult();
            var recruiterItem = await _repository.GetById(request.RecuruiterId);
            if (recruiterItem == null)
            {
                return reponse;
            }
            result.NumberLightning = recruiterItem.NumberLightning;
            result.Name = GetValueString(recruiterItem.Name);
            result.RecruiterCode = recruiterItem.Code;
            result.Email = GetValueString(recruiterItem.Email);
            result.IsBlock = (recruiterItem.Status == 2 || recruiterItem.Status == 0) ? true : false;
            var recruiterInfo = await _infoRepository.FindOneByStatementSql<RecruiterInfoModel>
                ("select top 1 * from RecruiterInfo where RelId = @RelId",
                new { RelId = request.RecuruiterId }
              );
            if (recruiterInfo == null)
            {
                recruiterInfo = new RecruiterInfoModel()
                {
                    LevelAuthen = 0,
                    Status = 0,
                    RelId = recruiterItem.Id,
                    CreatedBy = recruiterItem.CreatedBy,
                    UpdatedBy = recruiterItem.CreatedBy,
                    AvatarLink = "",
                    Email = recruiterItem.Email,
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                    DateActive = null,
                };
                await _infoRepository.AddOrUPdate(recruiterInfo);
            }
            result.Level = recruiterInfo.LevelAuthen;
            result.Phone = GetValueString(recruiterItem.Phone);
            result.AvatarLink = GetFullLink(recruiterInfo.AvatarLink);

            var tempGenger = 0;
            if (recruiterInfo.Gender == true)
            {
                tempGenger = 1;
            }
            else if (recruiterInfo.Gender == false)
            {
                tempGenger = 2;
            }

            result.Gender = tempGenger;

            //get companyInfo
            var companyItemInfo = await _companyInfoRepository.GetByUserName(recruiterItem.Email);
            if (companyItemInfo == null || companyItemInfo.Id < 1)
            {
                companyItemInfo.RelId = recruiterItem.Id;
                companyItemInfo.Email = recruiterItem.Email;
                companyItemInfo.TaxCode = recruiterItem.TaxCode;
                companyItemInfo.FullName = recruiterItem.Name;
                await _companyInfoRepository.AddOrUPdate(companyItemInfo);

            }


            var itemCompanyReponse = new CompanyInfoItem()
            {
                AddressInfo = GetValueString(companyItemInfo.AddressInfo),
                Capacity = GetValueString(companyItemInfo.Capacity),
                Email = GetValueString(companyItemInfo.EmailCompany),
                CoverLink = GetFullLink(companyItemInfo.CoverLink),
                FullName = GetValueString(companyItemInfo.FullName),
                LogoLink = GetFullLink(companyItemInfo.LogoLink),
                Phone = GetValueString(companyItemInfo.Phone),
                ShortDes = GetValueString(companyItemInfo.shortDes),
                TaxCode = GetValueString(companyItemInfo.TaxCode),
                Website = GetValueString(companyItemInfo.Website),
                IframeEmbeddedMap = companyItemInfo.IframeEmbeddedMap,
                RelId = companyItemInfo.Field

            };
            result.CompanyInfo = itemCompanyReponse;
            var _businessLicense = await _businessLicenseRepository.GetByUserName(recruiterItem.Email);
            if (_businessLicense == null || _businessLicense.Id < 1)
            {
                _businessLicense.RelId = _businessLicense.Id;
                _businessLicense.Email = _businessLicense.Email;
                _businessLicense.LinkFile = "";

                _businessLicense.Status = 0;
                await _businessLicenseRepository
                    .AddOrUPdate(_businessLicense);
            }
            var itemReponse = new BusinessLicenseItem();
            itemReponse.LinkFile = GetFullLink(_businessLicense.LinkFile);
            itemReponse.StatusText = getStatusBusiness(_businessLicense.Status);
            itemReponse.ReasonReject = _businessLicense.ReasonReject;
            itemReponse.DocumnetType = _businessLicense.DocumnetType;
            itemReponse.StatusCode = _businessLicense.Status;
            itemReponse.Note = GetValueString(_businessLicense.Note);
            result.BusinessLicenseInfo = itemReponse;
            reponse.Data = result;
            return reponse;
        }
        private string getStatusBusiness(int inputStatus)
        {
            if (inputStatus == 0)
            {
                return "Chưa ghi nhận thông tin";
            }
            if (inputStatus == 1)
            {
                return "Chờ duyệt";
            }

            if (inputStatus == 2)
            {
                return "Từ chối";
            }
            if (inputStatus == 3)
            {
                return "Đã duyệt";
            }
            return "Chưa nhận thông tin";
        }
        public async Task<BaseResult> UpdateInfoRecruiter(RecruiterInfoUpdate request)
        {
            var reponse = new BaseResult();


            var itemInfo = await _repository.GetById(request.RecuruiterId);


            if (itemInfo == null)
            {
                return reponse;
            }

            if (!string.IsNullOrEmpty(request.Phone))
            {
                itemInfo.Phone = request.Phone;
            }
            if (!string.IsNullOrEmpty(request.Taxcode))
            {
                itemInfo.TaxCode = request.Taxcode;
            }
            if (!string.IsNullOrEmpty(request.Name))
            {
                itemInfo.Name = request.Name;
            }
            itemInfo.UpdatedBy = request.HandleBy;
            itemInfo.UpdateAt = DateTime.Now;
            await _repository.AddOrUPdate(itemInfo);

            var recuruiterInfo = await _infoRepository.GetByUserName(itemInfo.Email);

            if (recuruiterInfo != null)
            {
                if (!string.IsNullOrEmpty(request.AvatarLink))
                {
                    recuruiterInfo.AvatarLink = request.AvatarLink;
                }

                if (request.Gender.HasValue)
                {
                    recuruiterInfo.Gender = request.Gender;
                }
                recuruiterInfo.UpdatedBy = request.HandleBy;
                recuruiterInfo.UpdateAt = DateTime.Now;
                await _infoRepository.AddOrUPdate(recuruiterInfo);

            }
            return reponse;
        }

        public async Task<BaseResult> UpdateCompanyInfo(CompanyInfoRequestUpdate request)
        {
            var reponse = new BaseResult();
            var itemInfo = await _companyInfoRepository.GetByUserName(request.Email);
            if (itemInfo == null)
            {
                itemInfo = new CompanyInfoModel();
                itemInfo.RelId = request.HandleBy;
                itemInfo.Email = request.Email;
            }

            itemInfo.Website = request.Website;
            itemInfo.Capacity = request.Capacity;
            itemInfo.LogoLink = request.LogoLink;
            itemInfo.TaxCode = request.TaxCode;
            itemInfo.CoverLink = request.CoverLink;
            itemInfo.FullName = request.FullName;
            itemInfo.AddressInfo = request.AddressInfo;
            itemInfo.shortDes = request.ShortDes;
            itemInfo.UpdatedBy = request.HandleBy;
            itemInfo.EmailCompany = request.EmailCompany;
            itemInfo.Phone = request.Phone;
            itemInfo.MapInfo = request.MapInfo;
            itemInfo.UpdateAt = DateTime.Now;
            itemInfo.Field = request.Field;
            itemInfo.IframeEmbeddedMap = request.IframeEmbeddedMap;
            itemInfo.Slug = Utilities.SlugifySlug(request.FullName);
            await _companyInfoRepository.AddOrUPdate(itemInfo);
            return reponse;
        }

        public async Task<BaseResult> UpdateBusinessLicense(BusinessLicenseRequestUpdate request)
        {
            var reponse = new BaseResult();
            var itemInfo = await _businessLicenseRepository.GetByUserName(request.Email);
            if (itemInfo == null || itemInfo.Id < 1)
            {
                itemInfo = new BusinessLicenseModel();
                itemInfo.Email = request.Email;
                itemInfo.Status = 0;
                itemInfo.CreateAt = DateTime.Now;
                itemInfo.CreatedBy = request.HandleBy;
                itemInfo.UpdateAt = DateTime.Now;
                itemInfo.UpdatedBy = request.HandleBy;
            }
            if (!string.IsNullOrEmpty(request.LinkFile))
            {
                if (itemInfo.LinkFile != request.LinkFile)
                {
                    itemInfo.LinkFile = request.LinkFile;
                    itemInfo.Status = 1;
                }

            }
            itemInfo.DocumnetType = request.DocumnetType;
            await _businessLicenseRepository.AddOrUPdate(itemInfo);

            return reponse;
        }

        public async Task<UserResgisterResult> RegisterUser(RecruiterRegisterRequest request)
        {
            var reponse = new UserResgisterResult();

            if (string.IsNullOrEmpty(request.Name) ||
            string.IsNullOrWhiteSpace(request.Name)
            )
            {
                reponse.AddError("Name", "Thiếu thông tin MST");
            }
            if (string.IsNullOrEmpty(request.Email) ||
                string.IsNullOrWhiteSpace(request.Email)
                )
            {
                reponse.AddError("Email", "Thiếu thông tin email");
            }
            if (string.IsNullOrEmpty(request.Password) ||
              string.IsNullOrWhiteSpace(request.Password)
              )
            {
                reponse.AddError("Password", "Thiếu thông tin mật khẩu");
            }

            if (string.IsNullOrEmpty(request.TaxCode) ||
                 string.IsNullOrWhiteSpace(request.TaxCode))
            {
                reponse.AddError("TaxCode", "Thiếu thông tin MST");
            }

            if (string.IsNullOrWhiteSpace(request.Phone)
            )
            {
                reponse.AddError("Phone", "Thiếu thông tin số điện thoại");
            }
            var itemInsert = new RecruiterRepAdd()
            {
                UserName = request.Email,
                Name = request.Name,
                TaxCode = request.TaxCode,
                Email = request.Email,
                Phone = request.Phone,
                CreatedBy = 1,
                Pass = request.Password
            };
            var itemDup = await _repository
            .FindOneByStatementSql<RecruiterModel>("select top 1 * from  Recruiter where email = @Email", new
            {
                itemInsert.Email
            });
            if (itemDup != null)
            {
                reponse.AddError(nameof(itemInsert.Email), "Trùng lặp thông tin Email ");

            }
            if (!reponse.Success)
            {
                return reponse;
            }
            await _repository.AddUser(itemInsert);
            var item = new ActiveCodeRecruiter();
            var randomCode = "" + new Random().Next(1000, 10000) + DateTime.Now.Ticks + "";
            item.Code = randomCode;
            item.Email = itemInsert.Email;
            item.Status = 1;
            item.CreateAt = DateTime.Now;
            await _activeCodeRecruiterRepository.AddOrUPdate(item);
            await _mailBussiness.RecruitmentSuccessRegister(item.Email);
            return reponse;

        }

        public async Task<UserResgisterResult> Checkmail(RecruiterRegisterRequest request)
        {
            var reponse = new UserResgisterResult();
            await _mailBussiness.RecruitmentSuccessRegister("trang.nguyen@topmass.vn");
            return reponse;

        }


        public async Task<BaseResult> ChangePassword(string password, int userId)
        {
            var reponse = new BaseResult();
            var itemChange = await _repository.GetById(userId);
            if (itemChange == null)
            {
                return reponse;
            }
            itemChange.Password = password;
            itemChange.UpdateAt = DateTime.Now;
            await _repository.AddOrUPdate(itemChange);


            return reponse;
        }
    }
}
