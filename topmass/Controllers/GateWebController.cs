using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Topmass.core.Business;
using Topmass.core.Business.Model;


namespace topmass.Controllers
{


    [ApiController]

    public class GateWebController : BaseController
    {
        private readonly ILogger<WebController> _logger;
        private readonly IProfileBusiness _profileBusiness;

        private readonly IMetaDataBussiness _metaDataBussiness;
        public GateWebController(ILogger<WebController> logger,
            IProfileBusiness profileBusiness,
            IMetaDataBussiness metaDataBussiness
    ) : base(logger)
        {
            _logger = logger;

            _profileBusiness = profileBusiness;
            _metaDataBussiness = metaDataBussiness;

        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetInfoCandidate(string slug = "minh.pn")
        {

            var baseReult = new TopMass.Core.Result.BaseResult();
            var candidateid = await _profileBusiness.GetInfomationCandidateByMail(slug);
            int userId = candidateid.Id;
            var educations = await _profileBusiness.GetAllEducation(userId);
            var experiences = await _profileBusiness.GetAllExperience(userId);
            var allProjects = await _profileBusiness.GetAllProjectUser(userId);
            var allSkill = await _profileBusiness.GetAllOtherProfileUser(1, userId);
            var allsoftSkill = await _profileBusiness.GetAllOtherProfileUser(2, userId);
            var allTools = await _profileBusiness.GetAllOtherProfileUser(3, userId);
            var allReward = await _profileBusiness.GetAllCertifyUser(userId, 2);
            var allCertify = await _profileBusiness.GetAllCertifyUser(userId, 1);
            var profileCv = await _profileBusiness.GetProfileUserCV(userId);
            var dataInfo = new
            {
                educations = educations.Data,
                experiences = experiences.Data,
                allProjects = allProjects.Data,
                allSkill = allSkill.Data,
                allsoftSkill = allsoftSkill.Data,
                allTools = allTools.Data,
                allReward = allReward.Data,
                allCertify = allCertify.Data,
                profileCv
            };
            baseReult.Data = dataInfo;
            return StatusCode(baseReult.StatusCode, baseReult);
        }




        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetInfoMetadata(string keyScreen = "homePage")
        {
            //public Task<MetaDataReponse> GetInfo(MetaDataRequest request);
            var reponse = new TopMass.Core.Result.BaseResult();
            var result = await _metaDataBussiness.GetInfo(new MetaDataRequest()
            {
                KeyScreen = keyScreen
            });
            reponse.Data = result;
            return StatusCode(reponse.StatusCode, reponse);
        }


        [HttpGet]
        public async Task<ActionResult> GetRegionalSearch()
        {
            var reponse = new TopMass.Core.Result.BaseResult();
            int userid = -1;
            if (User.Identity.IsAuthenticated)
            {
                var userCurrent = await GetCurrentUser();
                userid = userCurrent.UserId;
            }

            reponse.Data = await _profileBusiness.GetRegionSearchSetting(userid);
            return StatusCode(reponse.StatusCode, reponse);

        }
    }
}
