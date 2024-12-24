using Microsoft.AspNetCore.Mvc;
using topmass.Model;
using Topmass.core.Business;

namespace topmass.Controllers
{
    [ApiController]

    public class EmailController : BaseController
    {

        private readonly ILogger<EmailController> _logger;
        private readonly ICandidateMailBusiness _candidateMail;

        public EmailController(ILogger<EmailController> logger,

            ICandidateBusiness candidateBusiness,
            ICandidateMailBusiness candidateMail) : base(logger)
        {
            _logger = logger;
            _candidateMail = candidateMail;

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
            var dataResult = await _candidateMail.RequestMailValidAccount(email);
            return StatusCode(dataResult.StatusCode, dataResult);
        }


        [HttpPost]
        public async Task<ActionResult> RequestSendMailChangePassword(MailRequest request)
        {
            var reponse = new BaseResult();
            if (string.IsNullOrEmpty(request.Email))
            {
                reponse.Message = "Email không tồn tại";
                return StatusCode(reponse.StatusCode, reponse);
            }
            var result = await _candidateMail.HandleRequestPassword(request.Email);
            return StatusCode(result.StatusCode, result);
        }

    }
}
