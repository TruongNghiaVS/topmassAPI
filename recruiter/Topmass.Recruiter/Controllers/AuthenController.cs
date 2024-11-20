using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Topmass.Recruiter.Bussiness;
using Topmass.Recruiter.Model;

namespace Topmass.Recruiter.Controllers
{
    [ApiController]
    [Authorize]
    public class AuthenController : BaseController
    {

        private readonly ILogger<AuthenController> _logger;


        private readonly IRecruiterBusiness _bussiness;
        private readonly IAuthenBuisiness _authenBuisiness;
        public AuthenController(ILogger<AuthenController> logger,

            IRecruiterBusiness candidateBusiness,
            IAuthenBuisiness authenBuisiness) : base(logger)
        {
            _logger = logger;

            _bussiness = candidateBusiness;
            _authenBuisiness = authenBuisiness;
        }
        [HttpGet]
        public async Task<ActionResult> GetInfo()
        {
            var resultUser = await GetCurrentUser();
            var result = await _bussiness.GetInfo(new Bussiness.Model.RecruiterInfoRequest()
            {
                Email = resultUser.UserName,
                RecuruiterId = int.Parse(resultUser.Id)
            });
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        public async Task<ActionResult> GetOverViewInfo()
        {
            var resultUser = await GetCurrentUser();
            var result = await _bussiness.GetInfo(new Bussiness.Model.RecruiterInfoRequest()
            {
                Email = resultUser.UserName,
                RecuruiterId = int.Parse(resultUser.Id)
            });
            return StatusCode(result.StatusCode, result);
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> LoginUser(AuthenRequest request)
        {
            var dataError = new ErrorData() { };
            if (string.IsNullOrEmpty(request.UserName))
            {
                dataError.AddError(nameof(request.UserName), "Thiếu thông tin tên đăng nhập");
            }
            if (string.IsNullOrEmpty(request.Password))
            {
                dataError.AddError(nameof(request.Password), "Thiếu thông tin mật khẩu");
            }
            if (dataError.HasErorr())
            {
                return StatusCode(303, dataError);
            }
            var result = await _authenBuisiness.Login(request.UserName, request.Password);
            return StatusCode(result.StatusCode, result);
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmAccount(ValidLinkRequest request)
        {
            var dataError = new ErrorData() { };
            if (string.IsNullOrEmpty(request.Code))
            {
                dataError.AddError(nameof(request.Code), "Thiếu tham số code");
            }
            if (dataError.HasErorr())
            {
                return StatusCode(303, dataError);
            }
            var result = await _authenBuisiness.ConfirmToValidAccoutByCode(request.Code);
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
            var result = await _authenBuisiness.ChangePasswordNotMail(request.Password, int.Parse(resultUserChange.Id));
            return StatusCode(result.StatusCode, result);
        }





    }
}
