using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using topmass.Controllers;
using Topmass.Business.Regional;
using Topmass.Sync.Business;
using TopMass.Core.Result;

namespace topmass.Model
{
    [ApiController]

    public class SyncDataCVController : BaseController
    {

        private readonly ILogger<JobWebController> _logger;
        private readonly ISyncBusiness _business;
        private readonly IRegionalBusiness _regionalbussiness;
        public SyncDataCVController(ILogger<JobWebController> logger,
              ISyncBusiness business
            ) : base(logger)
        {
            _logger = logger;
            _business = business;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> SynycDataFromTompmass([FromQuery] InputJobInfoRequest request)
        {
            var reponse = new BaseResult();
            await _business.HandleCVSyncDataFromTopmass();
            return StatusCode(reponse.StatusCode, reponse);

        }

    }
}
