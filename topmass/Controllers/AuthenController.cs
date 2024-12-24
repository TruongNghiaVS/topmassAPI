using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using topmass.Model;
using Topmass.core.Business;

namespace topmass.Controllers
{
    [ApiController]
    [Authorize]
    public class AuthenController : BaseController
    {

        private readonly ILogger<AuthenController> _logger;
        private readonly ICandidateBusiness _candidateBusiness;
        private readonly IAuthenBuisiness _authenBuisiness;
        public AuthenController(ILogger<AuthenController> logger,

            ICandidateBusiness candidateBusiness,
            IAuthenBuisiness authenBuisiness) : base(logger)
        {
            _logger = logger;
            _candidateBusiness = candidateBusiness;
            _authenBuisiness = authenBuisiness;
        }
        [HttpGet]
        public async Task<ActionResult> GetUserCurrent()
        {
            var resultUser = await GetCurrentUser();
            var result = await _candidateBusiness.GetInfo(new Topmass.core.Business.Model.CandidateInfoRequest()
            {
                Email = resultUser.UserName
            });
            return StatusCode(result.StatusCode, result);
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> LoginUser(AuthenRequest request)
        {
            var result = await _authenBuisiness.LoginCandidate(request.UserName, request.Password);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> ValidAccount(ValidLinkRequest request)
        {
            var dataResult = new BaseResult();
            if (string.IsNullOrEmpty(request.Code))
            {
                dataResult.Message = "Thiếu tham số Code";
                return StatusCode(dataResult.StatusCode, dataResult);
            }
            var result = await _authenBuisiness.ConfirmAccoutCandidate(request.Code);
            return StatusCode(result.StatusCode, result);
        }
    }
}
