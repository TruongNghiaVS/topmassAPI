using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Topmass.Recruiter.Bussiness;
using Topmass.Recruiter.Model;
using TopMass.Core.Result;

namespace Topmass.Recruiter.Controllers
{
    [ApiController]

    public class EmailController : BaseController
    {

        private readonly ILogger<EmailController> _logger;
        private readonly IRecruiterBusiness _bussiness;
        private readonly IRecruiterMail _recruiterMail;
        private readonly IAuthenBuisiness _authenBuisiness;
        public EmailController(ILogger<EmailController> logger,

            IRecruiterBusiness candidateBusiness,
            IRecruiterMail recruiterMail,
            IAuthenBuisiness authenBuisiness) : base(logger)
        {
            _logger = logger;

            _bussiness = candidateBusiness;
            _recruiterMail = recruiterMail;
            _authenBuisiness = authenBuisiness;
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> RequestSendMailChangePassword(RequestChangePasswordRequest request)
        {
            var reponse = new BaseResult();
            var email = request.Email;
            if (string.IsNullOrEmpty(email))
            {
                reponse.Message = "Email không tồn tại";
                return StatusCode(reponse.StatusCode, reponse);
            }
            var result = await _authenBuisiness.HandleRequestPassword(request.Email);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<ActionResult> RequestMailValidAccount(MailRequest request)
        {
            var reponse = new BaseResult();
            var email = request.Email;
            if (string.IsNullOrEmpty(email))
            {
                reponse.Message = "Email không tồn tại";
                return StatusCode(reponse.StatusCode, reponse);
            }
            var dataResult = await _recruiterMail.RequestMailValidAccount(email);
            return StatusCode(dataResult.StatusCode, dataResult);
        }



    }
}
