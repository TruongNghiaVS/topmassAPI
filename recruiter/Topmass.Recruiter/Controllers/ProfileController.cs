using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Topmass.Business.History;
using Topmass.Recruiter.Bussiness;
using Topmass.Recruiter.Model;
using TopMass.Core.Result;


namespace Topmass.Recruiter.Controllers
{
    [ApiController]
    [Authorize]
    public class ProfileController : BaseController
    {

        private readonly ILogger<ProfileController> _logger;


        private readonly IRecruiterBusiness _humanBussiness;

        private readonly IHistoryBussiness _historyBussiness;
        public ProfileController(ILogger<ProfileController> logger,

            IRecruiterBusiness candidateBusiness,
            IHistoryBussiness historyBussiness
            ) : base(logger)
        {
            _logger = logger;
            _humanBussiness = candidateBusiness;
            _historyBussiness = historyBussiness;

        }

        [HttpPost]
        public async Task<ActionResult> UpdateBasicInfo(UpdateBasicRequest request)
        {
            var resultUser = await GetCurrentUser();
            var reponse = new BaseResult();

            if (string.IsNullOrEmpty(request.FullName))
            {
                reponse.AddError(nameof(request.FullName), "Thiếu thông tên");
            }
            if (!reponse.Success)
            {
                return StatusCode(reponse.StatusCode, reponse);
            }

            bool? genderTemp = null;
            if (request.Gender == 1)
            {
                genderTemp = true;
            }
            else if (request.Gender == 2)
            {
                genderTemp = false;
            }


            var requestUpdate = new RecruiterInfoUpdate()
            {
                Name = request.FullName,
                Gender = genderTemp,
                AvatarLink = request.AvatarLink,
                HandleBy = int.Parse(resultUser.Id),
                Phone = request.Phone,
                Email = resultUser.UserName,
                RecuruiterId = int.Parse(resultUser.Id)
            };

            await _historyBussiness.Add(new Business.History.model.HIstoryDataRequestAdd()
            {
                Actor = 1,
                BusinessTime = DateTime.Now,
                Content = "Cập nhật thông tin cá nhân",
                TypeData = 2,
                Source = 2,
                UserId = resultUser.UserId
            });
            var result = await _humanBussiness.UpdateInfoRecruiter(requestUpdate);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateCompanyInfo(CompanyInfoRequest request)
        {
            var resultUser = await GetCurrentUser();
            var reponse = new BaseResult();

            if (string.IsNullOrEmpty(request.FullName))
            {
                reponse.AddError(nameof(request.FullName), "Thiếu thông tên công ty");
            }
            if (!reponse.Success)
            {
                return StatusCode(reponse.StatusCode, reponse);
            }

            var requestUpdate = new CompanyInfoRequestUpdate()
            {
                Capacity = request.Capacity,
                CoverLink = request.CoverLink,
                EmailCompany = request.Email,
                MapInfo = request.MapInfo,
                Field = request.RelId,
                FullName = request.FullName,
                AddressInfo = request.AddressInfo,
                Email = resultUser.UserName,
                HandleBy = int.Parse(resultUser.Id),
                Phone = request.Phone,
                LogoLink = request.LogoLink,
                ShortDes = request.ShortDes,

                TaxCode = request.TaxCode,
                Website = request.Website
            };
            var result = await _humanBussiness.UpdateCompanyInfo(requestUpdate);

            await _historyBussiness.Add(new Business.History.model.HIstoryDataRequestAdd()
            {
                Actor = 1,
                BusinessTime = DateTime.Now,
                Content = "Cập nhật thông tin công ty",
                TypeData = 2,
                Source = 2,
                UserId = resultUser.UserId
            });
            return StatusCode(result.StatusCode, result);
        }
        [HttpPost]
        public async Task<ActionResult> AddBusinesslicense
        (BusinessLicenseRequest request)
        {
            var resultUser = await GetCurrentUser();
            var reponse = new BaseResult();

            if (string.IsNullOrEmpty(request.DocumentLink))
            {
                reponse.AddError(nameof(request.DocumentLink), "Thiếu thông tin file");
            }
            if (!reponse.Success)
            {
                return StatusCode(reponse.StatusCode, reponse);
            }
            var requestUpdate = new BusinessLicenseRequestUpdate()
            {
                LinkFile = request.DocumentLink,
                Email = resultUser.UserName,
                HandleBy = int.Parse(resultUser.Id)
            };

            await _historyBussiness.Add(new Business.History.model.HIstoryDataRequestAdd()
            {
                Actor = 1,
                BusinessTime = DateTime.Now,
                Content = "Cập nhật giấy đăng ký doanh nghiệp",
                TypeData = 2,
                Source = 2,
                UserId = resultUser.UserId
            });
            var result = await _humanBussiness.UpdateBusinessLicense(requestUpdate);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> ChangePassword(PasswordChanged request)
        {
            var resultUserChange = await GetCurrentUser();
            BaseResult reponse = new BaseResult();
            if (string.IsNullOrEmpty(request.Password))
            {
                reponse.AddError(nameof(request.Password), "Thiếu thông tin mật khẩu");
            }
            if (!reponse.Success)
            {
                return StatusCode(reponse.StatusCode, reponse);
            }
            var result = await _humanBussiness.ChangePassword(request.Password, int.Parse(resultUserChange.Id));

            await _historyBussiness.Add(new Business.History.model.HIstoryDataRequestAdd()
            {
                Actor = 1,
                BusinessTime = DateTime.Now,
                Content = "Cập nhật giấy đăng ký doanh nghiệp",
                TypeData = 1,
                Source = 2,
                UserId = resultUserChange.UserId
            });
            return StatusCode(result.StatusCode, result);
        }


    }
}
