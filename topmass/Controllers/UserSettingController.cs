using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using topmass.Controllers;
using Topmass.core.Business;

namespace topmass.Model
{
    [ApiController]
    [Authorize]
    public class UserSettingController : BaseController
    {

        private readonly ILogger<UserSettingController> _logger;

        private readonly IProfileBusiness _profileBusiness;

        public UserSettingController(ILogger<UserSettingController> logger,
            IProfileBusiness profileBusiness
            ) : base(logger)
        {
            _logger = logger;


            _profileBusiness = profileBusiness;
        }


        [HttpPost]
        public async Task<ActionResult> SaveJobSetting(InputJobSettingRequest request)
        {
            var resultUser = await GetCurrentUser();
            var baseReult = new BaseResult();
            var settingInfo = new JobSettingRequest
            {
                UserId = resultUser.UserId,
                Experience = request.Experience,
                Field = request.Field,
                Gender = request.Gender,
                LocationAddress = request.LocationAddress,
                Position = request.Position,
                RelId = request.UserId,
                Salary = request.Salary,
                Skill = request.Skill
            };
            var data = await _profileBusiness.SaveJobSetting(settingInfo);
            baseReult.Data = data;
            return StatusCode(baseReult.StatusCode, baseReult);
        }
        [HttpGet]
        public async Task<ActionResult> GetJobSetting()
        {
            var resultUser = await GetCurrentUser();
            var baseReult = new BaseResult();
            var data = await _profileBusiness.GetJobSetting(resultUser.UserId);
            baseReult.Data = data;
            return StatusCode(baseReult.StatusCode, baseReult);

        }





    }
}
