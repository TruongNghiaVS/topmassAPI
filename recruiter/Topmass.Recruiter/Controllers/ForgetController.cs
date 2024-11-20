using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Topmass.Recruiter.Bussiness;
using Topmass.Recruiter.Model;

namespace Topmass.Recruiter.Controllers
{
    [ApiController]
    [Authorize]
    public class ForgetController : BaseController
    {

        private readonly ILogger<ForgetController> _logger;
        private readonly IRecruiterBusiness _bussiness;
        private readonly IAuthenBuisiness _authenBuisiness;
        public ForgetController(ILogger<ForgetController> logger,

            IRecruiterBusiness candidateBusiness,
            IAuthenBuisiness authenBuisiness) : base(logger)
        {
            _logger = logger;

            _bussiness = candidateBusiness;
            _authenBuisiness = authenBuisiness;
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> RequestChangePassword(RequestChangePasswordRequest request)
        {

            var dataError = new ErrorData() { };
            if (string.IsNullOrEmpty(request.Email))
            {
                dataError.AddError(nameof(request.Email), "Thiếu thông tin email");
            }
            if (dataError.HasErorr())
            {
                return StatusCode(303, dataError);
            }
            var result = await _authenBuisiness.HandleRequestPassword(request.Email);
            return StatusCode(result.StatusCode, result);
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmToPageChangePassword(ValidLinkRequest request)
        {
            var dataError = new ErrorData() { };
            if (string.IsNullOrEmpty(request.Code))
            {
                dataError.AddError(nameof(request.Code), "Thiếu tham số đường link");
            }
            if (dataError.HasErorr())
            {
                return StatusCode(303, dataError);
            }
            var result = await _authenBuisiness.ConfirmHasValidResetPasswordLink(request.Code);
            return StatusCode(result.StatusCode, result);
        }




        [HttpPost]
        [Authorize]
        public async Task<ActionResult> ChangePassword(PasswordChanged request)
        {
            var resultUserChange = await GetCurrentUser();
            var dataError = new ErrorData() { };
            if (string.IsNullOrEmpty(request.Password))
            {
                dataError.AddError(nameof(request.Password), "Thiếu thông tin mật khẩu");
            }
            if (dataError.HasErorr())
            {
                return StatusCode(303, dataError);
            }
            var result = await _authenBuisiness.ChangePassword(request.Password, int.Parse(resultUserChange.Id));
            return StatusCode(result.StatusCode, result);
        }

    }
}
