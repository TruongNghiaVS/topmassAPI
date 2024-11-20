using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Topmass.Recruiter.Bussiness;
namespace Topmass.Recruiter.Controllers
{
    [ApiController]
    [Authorize]
    public class CompanyController : BaseController
    {

        private readonly ILogger<CompanyController> _logger;
        private readonly ICompanyBusiness _companyBusiness;
        public CompanyController(ILogger<CompanyController> logger,
             ICompanyBusiness companyBusiness
            ) : base(logger)
        {
            _logger = logger;
            _companyBusiness = companyBusiness;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetAllPartner()
        {

            var datas =
                await _companyBusiness
                .GetAllPartner();

            return StatusCode(datas.StatusCode, datas);
        }

    }
}
