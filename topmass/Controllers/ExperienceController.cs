using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using topmass.Controllers;
using Topmass.core.Business;

namespace topmass.Model
{
    [ApiController]
    [Authorize]
    public class ExperienceController : BaseController
    {

        private readonly ILogger<ExperienceController> _logger;

        private readonly IProfileBusiness _profileBusiness;

        public ExperienceController(ILogger<ExperienceController> logger,

            IProfileBusiness profileBusiness
            ) : base(logger)
        {
            _logger = logger;

            _profileBusiness = profileBusiness;
        }

        [HttpPost]
        public async Task<ActionResult> SaveExperiences(List<InputExperienceUserInfoUpdateRequest> requestAdd)
        {
            var resultUser = await GetCurrentUser();
            var baseReult = new BaseResult();


            foreach (var item in requestAdd)
            {
                if (string.IsNullOrEmpty(item.CompanyName))
                {
                    baseReult.AddError(nameof(item.CompanyName), "Thiếu thông tin trường");
                }
            }

            if (!baseReult.Success)
            {
                return StatusCode(baseReult.StatusCode, baseReult);
            }

            var allIdExited = await _profileBusiness.GetAllId(resultUser.UserId, 2);
            foreach (var item in requestAdd)
            {
                if (item.Id < 1)
                {

                    var requestInsert = new ExperienceUserInfoRequest()
                    {
                        FromMonth = item.FromMonth,
                        FromYear = item.FromYear,
                        Introduction = item.Introduction,
                        LinkFile = item.LinkFile,
                        CompanyName = item.CompanyName,
                        IsEnd = item.IsEnd,
                        Major = "",
                        Position = item.Position,
                        ToMonth = item.ToMonth,
                        ToYear = item.ToYear,
                        UserId = int.Parse(resultUser.Id)
                    };
                    await _profileBusiness.AddExperience(requestInsert);

                }
                else
                {
                    var requestUpdate = new ExperienceUserInfoUpdateRequest()
                    {
                        FromMonth = item.FromMonth,
                        FromYear = item.FromYear,
                        Introduction = item.Introduction,
                        LinkFile = item.LinkFile,
                        CompanyName = item.CompanyName,
                        IsEnd = item.IsEnd,
                        Major = "",
                        Position = item.Position,
                        ToMonth = item.ToMonth,
                        ToYear = item.ToYear,
                        UserId = int.Parse(resultUser.Id),
                        Id = item.Id
                    };
                    await _profileBusiness.UpdateExperience(requestUpdate);
                }
            }

            var deleteAll = allIdExited.Select(a => a.Id).Except(requestAdd.Select(b => b.Id)).ToList();

            foreach (var item in deleteAll)
            {
                await _profileBusiness.DeleteExperience(new ExperienceUserInfoDeleteRequest()
                {
                    Id = item
                });
            }


            baseReult.Data = true;
            await _profileBusiness.ReloadGenFileCV(resultUser.UserId);
            return StatusCode(baseReult.StatusCode, baseReult);

        }

        [HttpPost]
        public async Task<ActionResult> AddExperience(InputExperienceUserRequestAdd requestAdd)
        {
            var resultUser = await GetCurrentUser();
            var baseReult = new BaseResult();

            if (string.IsNullOrEmpty(requestAdd.CompanyName))
            {
                baseReult.AddError(nameof(requestAdd.CompanyName), "Thiếu thông tin trường");
            }

            if (!baseReult.Success)
            {
                return StatusCode(baseReult.StatusCode, baseReult);
            }

            var request = new ExperienceUserInfoRequest()
            {
                FromMonth = requestAdd.FromMonth,
                FromYear = requestAdd.FromYear,
                Introduction = requestAdd.Introduction,
                LinkFile = requestAdd.LinkFile,
                CompanyName = requestAdd.CompanyName,
                IsEnd = requestAdd.IsEnd,

                Position = requestAdd.Position,
                ToMonth = requestAdd.ToMonth,
                ToYear = requestAdd.ToYear,
                UserId = int.Parse(resultUser.Id)
            };
            var data = await _profileBusiness.AddExperience(request);
            baseReult.Data = data;
            await _profileBusiness.ReloadGenFileCV(resultUser.UserId);
            return StatusCode(baseReult.StatusCode, baseReult);

        }


        [HttpPost]
        public async Task<ActionResult> UpdateExperience(InputExperienceUserInfoUpdateRequest requestAdd)
        {
            var resultUser = await GetCurrentUser();
            var baseReult = new BaseResult();

            if (string.IsNullOrEmpty(requestAdd.CompanyName))
            {
                baseReult.AddError(nameof(requestAdd.CompanyName), "Thiếu thông tin trường");
            }

            if (!baseReult.Success)
            {
                return StatusCode(baseReult.StatusCode, baseReult);
            }
            var request = new ExperienceUserInfoUpdateRequest()
            {
                FromMonth = requestAdd.FromMonth,
                FromYear = requestAdd.FromYear,
                Introduction = requestAdd.Introduction,
                LinkFile = requestAdd.LinkFile,
                CompanyName = requestAdd.CompanyName,
                IsEnd = requestAdd.IsEnd,

                Position = requestAdd.Position,
                ToMonth = requestAdd.ToMonth,
                ToYear = requestAdd.ToYear,
                UserId = int.Parse(resultUser.Id),
                Id = requestAdd.Id
            };
            var data = await _profileBusiness.UpdateExperience(request);
            baseReult.Data = data;
            await _profileBusiness.ReloadGenFileCV(resultUser.UserId);
            return StatusCode(baseReult.StatusCode, baseReult);

        }

        [HttpPost]
        public async Task<ActionResult> DeleteExperience(InputProfileDelete requestAdd)
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
            var data = await _profileBusiness.DeleteExperience(new ExperienceUserInfoDeleteRequest()
            {
                Id = requestAdd.Id
            });
            baseReult.Data = data;
            return StatusCode(baseReult.StatusCode, baseReult);

        }

        [HttpGet]
        public async Task<ActionResult> GetAllExperience()
        {
            var resultUser = await GetCurrentUser();
            var baseReult = new BaseResult();
            var data = await _profileBusiness.GetAllExperience(int.Parse(resultUser.Id));
            baseReult.Data = data;
            return StatusCode(baseReult.StatusCode, baseReult);

        }
    }
}
