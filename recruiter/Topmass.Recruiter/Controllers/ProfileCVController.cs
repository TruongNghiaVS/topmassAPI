using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Topmass.CV.Business;

namespace Topmass.Recruiter.Controllers
{
    [ApiController]
    [Authorize]
    public class ProfileCVController : BaseController
    {

        private readonly ILogger<SearchCVController> _logger;
        private readonly ICVBusiness _cVBusiness;
        private readonly ICVUtilities _cVUtilities;
        public ProfileCVController(ILogger<SearchCVController> logger,

            ICVBusiness cVBusiness,
            ICVUtilities cVUtilities
            ) : base(logger)
        {

            _logger = logger;
            _cVBusiness = cVBusiness;
            _cVUtilities = cVUtilities;
        }




    }
}
