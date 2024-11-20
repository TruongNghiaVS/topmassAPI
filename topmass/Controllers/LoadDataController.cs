using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using topmass.Controllers;
using Topmass.Business.Regional;

namespace topmass.Model
{
    [ApiController]

    public class LoadDataController : BaseController
    {

        private readonly ILogger<LocationController> _logger;
        private readonly IRegionalBusiness bussiness;
        public LoadDataController(ILogger<LocationController> logger,
             IRegionalBusiness articleBusiness
     ) : base(logger)
        {
            _logger = logger;
            bussiness = articleBusiness;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> LoadDataRegional()
        {

            var result = await bussiness.LoadAllData();
            return Ok(true);
        }





    }
}
