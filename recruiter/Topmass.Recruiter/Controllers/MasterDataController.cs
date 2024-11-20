using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Topmass.Core.Common;
using Topmass.Recruiter.Model;

namespace Topmass.Recruiter.Controllers
{
    [ApiController]
    //[Authorize]
    public class MasterDataController : BaseController
    {

        private readonly ILogger<MasterDataController> _logger;
        private readonly IMasterDataBusiness _articleBusiness;

        public MasterDataController(ILogger<MasterDataController> logger,
            IMasterDataBusiness articleBusiness

            ) : base(logger)
        {
            _logger = logger;
            _articleBusiness = articleBusiness;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> InfoRealms()
        {
            var result = await _articleBusiness.FillterData((int)MasterDataType.Career);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetAllCareer()
        {
            var result = await _articleBusiness.FillterData((int)MasterDataType.Career);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetAllJobType()
        {
            var result = await _articleBusiness.FillterData((int)MasterDataType.JobType);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetAllRankCandidate()
        {
            var result = await _articleBusiness.FillterData((int)MasterDataType.Rank);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetAllExperience()
        {
            var result = await _articleBusiness.FillterData((int)MasterDataType.Experience);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetAllStatusApply()
        {
            var result = await _articleBusiness.FillterData((int)MasterDataType.StatusAply);
            return StatusCode(result.StatusCode, result);
        }
    }
}
