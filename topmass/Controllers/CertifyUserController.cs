using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using topmass.Controllers;
using Topmass.core.Business;

namespace topmass.Model
{
    [ApiController]
    [Authorize]
    public class CertifyUserController : BaseController
    {
        private readonly ILogger<CertifyUserController> _logger;

        private readonly IProfileBusiness _profileBusiness;

        public CertifyUserController(ILogger<CertifyUserController> logger,

            IProfileBusiness profileBusiness
            ) : base(logger)
        {
            _logger = logger;

            _profileBusiness = profileBusiness;
        }

        [HttpPost]
        public async Task<ActionResult> AddCertifyUser(InputCertifyUserRequestAdd requestAdd)
        {
            var resultUser = await GetCurrentUser();
            var baseReult = new BaseResult();

            if (string.IsNullOrEmpty(requestAdd.FullName))
            {
                baseReult.AddError(nameof(requestAdd.FullName), "Thiếu thông tin trường");
            }

            if (!baseReult.Success)
            {
                return StatusCode(baseReult.StatusCode, baseReult);
            }

            var request = new CertifyUserInfoRequest()
            {

                Introduction = requestAdd.Introduction,
                LinkFile = requestAdd.LinkFile,
                FullName = requestAdd.FullName,
                CompanyName = requestAdd.CompanyName,
                MonthGet = requestAdd.MonthGet,
                UserId = int.Parse(resultUser.Id),
                YearGet = requestAdd.YearGet


            };
            var data = await _profileBusiness.AddCertify(request);
            baseReult.Data = data;
            return StatusCode(baseReult.StatusCode, baseReult);

        }

        [HttpPost]
        public async Task<ActionResult> SaveCertifyUsers(List<InputCertifyUserRequestAdd> requestAdd)
        {
            var resultUser = await GetCurrentUser();
            var baseReult = new BaseResult();


            foreach (var item in requestAdd)
            {
                if (string.IsNullOrEmpty(item.FullName))
                {
                    baseReult.AddError(nameof(item.FullName), "Thiếu thông tin trường");
                }
            }

            if (!baseReult.Success)
            {
                return StatusCode(baseReult.StatusCode, baseReult);
            }
            var request = new CertifyUserInfoRequest()
            {
                UserId = int.Parse(resultUser.Id)
            };

            var allIdExited = await _profileBusiness.GetAllId(resultUser.UserId, 4);
            foreach (var item in requestAdd)
            {
                if (item.Id < 1)
                {

                    var requestInsert = new CertifyUserInfoUpdateRequest()
                    {
                        Introduction = item.Introduction,
                        LinkFile = item.LinkFile,
                        IsExpired = item.IsExpired,
                        MonthExpired = item.MonthExpired,
                        YearExpired = item.YearExpired,
                        FullName = item.FullName,
                        CompanyName = item.CompanyName,
                        MonthGet = item.MonthGet,
                        TypeData = 1,
                        UserId = int.Parse(resultUser.Id),
                        YearGet = item.YearGet
                    };
                    await _profileBusiness.AddCertify(requestInsert);

                }
                else
                {
                    var requestUpdate = new CertifyUserInfoUpdateRequest()
                    {



                        Introduction = item.Introduction,
                        LinkFile = item.LinkFile,
                        FullName = item.FullName,
                        CompanyName = item.CompanyName,
                        MonthGet = item.MonthGet,
                        IsExpired = item.IsExpired,
                        MonthExpired = item.MonthExpired,
                        YearExpired = item.YearExpired,
                        TypeData = 1,
                        UserId = int.Parse(resultUser.Id),
                        YearGet = item.YearGet,
                        Id = item.Id
                    };
                    await _profileBusiness.UpdateCertify(requestUpdate);
                }
            }

            var deleteAll = allIdExited.Select(a => a.Id).Except(requestAdd.Select(b => b.Id)).ToList();
            foreach (var item in deleteAll)
            {
                await _profileBusiness.DeleteCertifyUser(new CertifyUserInfoDeleteRequest()
                {
                    Id = item
                });
            }
            baseReult.Data = true;
            return StatusCode(baseReult.StatusCode, baseReult);

        }

        [HttpGet]
        public async Task<ActionResult> GetAllCertifyUser()
        {
            var resultUser = await GetCurrentUser();
            var baseReult = new BaseResult();


            baseReult.Data = await _profileBusiness.GetAllCertifyUser(resultUser.UserId, 1);
            return StatusCode(baseReult.StatusCode, baseReult);

        }
        [HttpPost]
        public async Task<ActionResult> SaveRewardUsers(List<InputCertifyUserRequestAdd> requestAdd)
        {
            var resultUser = await GetCurrentUser();
            var baseReult = new BaseResult();


            foreach (var item in requestAdd)
            {
                if (string.IsNullOrEmpty(item.FullName))
                {
                    baseReult.AddError(nameof(item.FullName), "Thiếu thông tin trường");
                }
            }

            if (!baseReult.Success)
            {
                return StatusCode(baseReult.StatusCode, baseReult);
            }
            var request = new CertifyUserInfoRequest()
            {
                UserId = int.Parse(resultUser.Id)
            };

            var allIdExited = await _profileBusiness.GetAllId(resultUser.UserId, 10);

            foreach (var item in requestAdd)
            {
                if (item.Id < 1)
                {

                    var requestInsert = new CertifyUserInfoUpdateRequest()
                    {
                        Introduction = item.Introduction,
                        LinkFile = item.LinkFile,
                        FullName = item.FullName,
                        CompanyName = item.CompanyName,
                        MonthGet = item.MonthGet,
                        TypeData = 2,
                        UserId = int.Parse(resultUser.Id),
                        YearGet = item.YearGet
                    };
                    await _profileBusiness.AddCertify(requestInsert);

                }
                else
                {
                    var requestUpdate = new CertifyUserInfoUpdateRequest()
                    {



                        Introduction = item.Introduction,
                        LinkFile = item.LinkFile,
                        FullName = item.FullName,
                        CompanyName = item.CompanyName,
                        MonthGet = item.MonthGet,
                        TypeData = 2,
                        UserId = int.Parse(resultUser.Id),
                        YearGet = item.YearGet,
                        Id = item.Id
                    };
                    await _profileBusiness.UpdateCertify(requestUpdate);
                }
            }

            var deleteAll = allIdExited.Select(a => a.Id).Except(requestAdd.Select(b => b.Id)).ToList();
            foreach (var item in deleteAll)
            {
                await _profileBusiness.DeleteCertifyUser(new CertifyUserInfoDeleteRequest()
                {
                    Id = item
                });
            }
            baseReult.Data = true;
            return StatusCode(baseReult.StatusCode, baseReult);

        }

        [HttpGet]

        public async Task<ActionResult> GetAllRewardUser()
        {
            var resultUser = await GetCurrentUser();
            var baseReult = new BaseResult();
            baseReult.Data = await _profileBusiness.GetAllCertifyUser(resultUser.UserId, 2);
            return StatusCode(baseReult.StatusCode, baseReult);

        }
        [HttpPost]
        public async Task<ActionResult> UpdateCertifyUser(InputCertifyUserInfoUpdateRequest requestAdd)
        {
            var resultUser = await GetCurrentUser();
            var baseReult = new BaseResult();

            if (string.IsNullOrEmpty(requestAdd.FullName))
            {
                baseReult.AddError(nameof(requestAdd.FullName), "Thiếu thông tin trường");
            }

            if (!baseReult.Success)
            {
                return StatusCode(baseReult.StatusCode, baseReult);
            }
            var request = new CertifyUserInfoUpdateRequest()
            {

                Introduction = requestAdd.Introduction,
                LinkFile = requestAdd.LinkFile,

                FullName = requestAdd.FullName,
                CompanyName = requestAdd.CompanyName,
                MonthGet = requestAdd.MonthGet,


                YearGet = requestAdd.YearGet,

                UserId = int.Parse(resultUser.Id),
                Id = requestAdd.Id,

            };
            var data = await _profileBusiness.UpdateCertify(request);
            baseReult.Data = data;
            return StatusCode(baseReult.StatusCode, baseReult);

        }

        [HttpPost]
        public async Task<ActionResult> DeleteCertifyUser(InputProfileDelete requestAdd)
        {
            var resultUser = await GetCurrentUser();
            var baseReult = new BaseResult();
            if (requestAdd.Id < 1)
            {
                baseReult.AddError(nameof(requestAdd.Id), "Thiếu thông tin trường Id");
            }

            if (!baseReult.Success)
            {
                return StatusCode(baseReult.StatusCode, baseReult);
            }
            var data = await _profileBusiness.DeleteCertifyUser(new CertifyUserInfoDeleteRequest()
            {
                Id = requestAdd.Id
            });
            baseReult.Data = data;
            return StatusCode(baseReult.StatusCode, baseReult);

        }


    }
}
