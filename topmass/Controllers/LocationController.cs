namespace topmass.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Topmass.Business.Regional;
    using TopMass.Core.Result;

    [ApiController]
    [Authorize]
    public class LocationController : BaseController
    {
        private readonly ILogger<LocationController> _logger;
        private readonly IRegionalBusiness bussiness;
        public LocationController(ILogger<LocationController> logger,
             IRegionalBusiness articleBusiness
     ) : base(logger)
        {
            _logger = logger;
            bussiness = articleBusiness;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetAllProvinces()
        {
            var baseResult = new BaseResult();
            var result = await bussiness.GetAllProvices();
            baseResult.Data = result;
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetAllDistrict([FromQuery] GetAllDistrictRequest requestAll)
        {
            var baseResult = new BaseResult();
            var result = await bussiness.GetAllDistrict(requestAll);
            baseResult.Data = result;
            return StatusCode(result.StatusCode, result);
        }
    }
}
