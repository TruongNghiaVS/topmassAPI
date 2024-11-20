using Microsoft.AspNetCore.Mvc;
using Topmass.Recruiter.Bussiness;
using Topmass.Recruiter.Model;

namespace Topmass.Recruiter.Controllers
{
    [ApiController]
    public class RegisterController : BaseController
    {
        private readonly ILogger<RegisterController> _logger;
        private readonly IRecruiterBusiness bussiness;

        public RegisterController(ILogger<RegisterController> logger,
            IRecruiterBusiness candidateBusiness
           ) : base(logger)
        {
            _logger = logger;
            bussiness = candidateBusiness;

        }
        [HttpPost]
        public async Task<ActionResult> RegisterUser(RecruiterRegisterRequest request)
        {

            var dataError = new ErrorData() { };
            if (string.IsNullOrEmpty(request.Name))
            {
                dataError.AddError(nameof(request.Name), "Thiếu thông tin tên");
            }

            if (string.IsNullOrEmpty(request.Phone))
            {
                dataError.AddError(nameof(request.Phone), "Thiếu thông tin Tên");
            }
            if (string.IsNullOrEmpty(request.Email))
            {
                dataError.AddError(nameof(request.Email), "Thiếu thông tin Email");
            }

            if (string.IsNullOrEmpty(request.TaxCode))
            {
                dataError.AddError(nameof(request.TaxCode), "Thiếu thông tin mã số thuế");
            }
            if (dataError.HasErorr())
            {
                return StatusCode(303, dataError);
            }
            var result = await bussiness.RegisterUser(request);
            return StatusCode(result.StatusCode, result);
        }






    }
}
