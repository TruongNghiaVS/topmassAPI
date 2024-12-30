using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Topmass.CV.Business;
using Topmass.Recruiter.Model;
using TopMass.Core.Result;

namespace Topmass.Recruiter.Controllers
{
    [ApiController]
    [Authorize]
    public class CandidateViewerController : BaseController
    {

        private readonly ILogger<CVController> _logger;
        private readonly ICVBusiness _cVBusiness;
        private readonly ICVUtilities _cVUtilities;
        public CandidateViewerController(ILogger<CVController> logger,
            ICVBusiness cVBusiness,
            ICVUtilities cVUtilities
            ) : base(logger)
        {
            _logger = logger;
            _cVBusiness = cVBusiness;
            _cVUtilities = cVUtilities;
        }

        [HttpPost]
        public async Task<ActionResult> AddLogStatus(IntputJobViewerRequest request)
        {
            var resultUser = await GetCurrentUser();
            var reponse = new BaseResult();
            if (request.Identi < 1)
            {
                reponse.AddError(nameof(request.Identi), "Thiếu thông tin đối tượng");
            }
            if (request.NoteCode < 0)
            {
                reponse.AddError(nameof(request.NoteCode), "Thiếu thông tin");
            }
            var result = await _cVUtilities.CandidateViewerAddStatus(request.Identi, resultUser.UserId, request.NoteCode, request.Noted);
            reponse.Data = result;
            return StatusCode(reponse.StatusCode, reponse);
        }

    }
}
