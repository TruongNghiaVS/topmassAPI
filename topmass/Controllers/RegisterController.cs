using Microsoft.AspNetCore.Mvc;
using topmass.Model;
using Topmass.core.Business;
using Topmass.core.Business.Model;

namespace topmass.Controllers
{
    [ApiController]
    public class RegisterController : BaseController
    {
        private readonly ILogger<RegisterController> _logger;
        private readonly ICandidateBusiness _candidateBusiness;
        private readonly IAuthenBuisiness _authenBuisiness;
        public RegisterController(ILogger<RegisterController> logger,

            ICandidateBusiness candidateBusiness,
            IAuthenBuisiness authenBuisiness) : base(logger)
        {
            _logger = logger;
            _candidateBusiness = candidateBusiness;
            _authenBuisiness = authenBuisiness;
        }
        [HttpPost]
        public async Task<ActionResult> RegisterUser(CandidateRegisterRequest request)
        {

            var dataError = new ErrorData() { };
            if (string.IsNullOrEmpty(request.FirstName)
                || string.IsNullOrWhiteSpace(request.FirstName)
                )
            {
                dataError.AddError(nameof(request.FirstName), "Thiếu thông tin Họ");
            }
            if (string.IsNullOrEmpty(request.LastName)
                  || string.IsNullOrWhiteSpace(request.LastName)
                )
            {
                dataError.AddError(nameof(request.LastName), "Thiếu thông tin Tên");
            }
            if (string.IsNullOrEmpty(request.Phone) || string.IsNullOrWhiteSpace(request.Phone))
            {
                dataError.AddError(nameof(request.Phone), "Thiếu thông tin số điện thoại");
            }
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrWhiteSpace(request.Email))
            {
                dataError.AddError(nameof(request.Email), "Thiếu thông tin Email");
            }

            if (string.IsNullOrEmpty(request.Password) || string.IsNullOrWhiteSpace(request.Password))
            {
                dataError.AddError(nameof(request.Password), "Thiếu thông tin mật khẩu");
            }
            if (dataError.HasErorr())
            {
                return StatusCode(303, dataError);
            }
            var result = await _candidateBusiness.RegisterUser(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<ActionResult> TestEmail()
        {
            var request = new CandidateRegisterRequest();

            var result = await _candidateBusiness.RegisterUser(request);
            return StatusCode(result.StatusCode, result);
        }

    }
}
