using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using topmass.Controllers;
using Topmass.CV.Business;
using Topmass.CV.Business.Model;
using TopMass.Core.Result;

namespace topmass.Model
{
    [ApiController]
    [Authorize]
    public class DigitalCVController : BaseController
    {

        private readonly ILogger<DigitalCVController> _logger;

        private readonly ICVBusiness _cVBusiness;

        private readonly ICVUserBusiness _userBusiness;
        public DigitalCVController(ILogger<DigitalCVController> logger,

            ICVBusiness cVBusiness,
             ICVUserBusiness userBusiness
            ) : base(logger)
        {
            _logger = logger;

            _cVBusiness = cVBusiness;
            _userBusiness = userBusiness;
        }


        [HttpPost]
        public async Task<ActionResult> CheckGenFileCV()
        {
            var resultUser = await GetCurrentUser();
            var reponse = new BaseResult();
            var result = await _cVBusiness.CheckGenFileDigital(resultUser.UserId);
            reponse.Data = result;
            return StatusCode(reponse.StatusCode, reponse);
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrUpdateCVWithTemplate([FromForm] CreateCVAddRequestWithTemplate request)
        {
            var resultUser = await GetCurrentUser();
            var fullName = resultUser.FullName;
            var reponse = new BaseResult();
            if (request.TemplateID < 0)
            {
                reponse.AddError(nameof(request.TemplateID), "Thiếu thông tin file");
            }
            if (request.FileCV == null)
            {
                reponse.AddError(nameof(request.FileCV), "Thiếu thông tin file");
            }
            if (!reponse.Success)
            {
                return StatusCode(reponse.StatusCode, reponse);
            }
            var requestAdd = new CVRequestDigitalAdd()
            {
                TypeData = 4,
                UserId = resultUser.UserId,
                TemplateID = 1,
                FileCV = request.FileCV,
                FullName = fullName,
                HandleBy = resultUser.UserId
            };
            var result = await _cVBusiness.AddOrUpdateCVDigital(requestAdd);
            reponse.Data = result;
            return StatusCode(reponse.StatusCode, reponse);
        }
    }
}
