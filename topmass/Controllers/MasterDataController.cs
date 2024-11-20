using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using topmass.Model;
using Topmass.Core.Common;

namespace topmass.Controllers
{
    [ApiController]
    //[Authorize]
    public class MasterDataController : BaseController
    {

        private readonly ILogger<MasterDataController> _logger;
        private readonly IMasterDataBusiness _business;

        public MasterDataController(ILogger<MasterDataController> logger,
            IMasterDataBusiness articleBusiness

            ) : base(logger)
        {
            _logger = logger;
            _business = articleBusiness;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> InfoRealms()
        {
            var result = await _business.FillterData((int)MasterDataType.Realm);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> AllEducationLevel()
        {
            var result = await _business.FillterData((int)MasterDataType.RankEdu);
            return StatusCode(result.StatusCode, result);
        }




        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetAllCareer()
        {
            var result = await _business.FillterData((int)MasterDataType.Career);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetAllJobType()
        {
            var result = await _business.FillterData((int)MasterDataType.JobType);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetAllRankCandidate()
        {
            var result = await _business.FillterData((int)MasterDataType.Rank);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetAllExperience()
        {
            var result = await _business.FillterData((int)MasterDataType.Experience);
            return StatusCode(result.StatusCode, result);
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetAllStatusApply()
        {
            var result = await _business.FillterData((int)MasterDataType.StatusAply);
            return StatusCode(result.StatusCode, result);
        }
    }
}
