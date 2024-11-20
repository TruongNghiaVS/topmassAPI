using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Topmass.CV.Business;
using Topmass.CV.Business.Model;
using Topmass.Recruiter.Model;
using TopMass.Core.Result;

namespace Topmass.Recruiter.Controllers
{
    [ApiController]
    [Authorize]
    public class CVController : BaseController
    {

        private readonly ILogger<CVController> _logger;


        private readonly ICVBusiness _cVBusiness;
        private readonly ICVUtilities _cVUtilities;
        public CVController(ILogger<CVController> logger,

            ICVBusiness cVBusiness,
            ICVUtilities cVUtilities
            ) : base(logger)
        {
            _logger = logger;

            _cVBusiness = cVBusiness;
            _cVUtilities = cVUtilities;
        }



        [HttpGet]
        public async Task<ActionResult> GetAllCVOfJob([FromQuery] InputGetAllCVApplyOfJob request)
        {
            var resultUser = await GetCurrentUser();
            var reponse = new BaseResult();
            var requestAdd = new GetAllCVOfJobRequest()
            {
                TypeData = request.TypeData,
                JobId = request.JobId,
                UserId = int.Parse(resultUser.Id)
            };
            var result = await _cVBusiness.GetAllCVOfJob(requestAdd);
            reponse.Data = result;
            return StatusCode(reponse.StatusCode, reponse);
        }

        [HttpGet]
        public async Task<ActionResult> GetAllSearchCVOfJob([FromQuery] InputGetAllSearchCVApplyOfJob request)
        {
            var resultUser = await GetCurrentUser();
            var reponse = new BaseResult();
            var requestAdd = new GetAllCVOfJobRequest()
            {
                TypeData = 3,
                JobId = request.JobId,
                UserId = int.Parse(resultUser.Id)
            };

            var result = await _cVBusiness.GetAllCVOfJob(requestAdd);
            reponse.Data = result;
            return StatusCode(reponse.StatusCode, reponse);
        }


        [HttpGet]
        public async Task<ActionResult> GetAllCVApply([FromQuery] InputGetAllCVApply request)
        {
            var resultUser = await GetCurrentUser();
            var reponse = new BaseResult();
            var requestAdd = new GetAllOfHumanRequest()
            {
                TypeData = request.TypeData,
                JobId = request.JobId,
                CampagnId = request.Recruitment.HasValue ? request.Recruitment.Value : -1,
                Status = request.Status,
                Key = request.Key,
                Source = request.Source,

                UserId = int.Parse(resultUser.Id)
            };
            var result = await _cVBusiness.GetAllCVApply(requestAdd);
            reponse.Data = result;
            return StatusCode(reponse.StatusCode, reponse);
        }

        [HttpGet]
        public async Task<ActionResult> GetAllCVApplyOfCampaign([FromQuery] InputGetAllCVApplyOfCampagn request)
        {
            var resultUser = await GetCurrentUser();
            var reponse = new BaseResult();

            if (request.CampagnId < 1)
            {
                reponse.AddError(nameof(request.CampagnId), "Thiếu thông tin chiến dịch");
            }
            var requestAdd = new GetAllOfHumanRequest()
            {
                TypeData = -1,
                JobId = -1,
                CampagnId = request.CampagnId,
                UserId = int.Parse(resultUser.Id)
            };
            var result = await _cVBusiness.GetAllCVApply(requestAdd);
            reponse.Data = result;
            return StatusCode(reponse.StatusCode, reponse);
        }
        [HttpPost]
        public async Task<ActionResult> UpdateViewModel(IntputCVChangeViewModeRequest request)
        {
            var resultUser = await GetCurrentUser();
            var reponse = new BaseResult();

            if (request.Identi < 1)
            {
                reponse.AddError(nameof(request.Identi), "Thiếu thông tin đối tượng");
            }

            if (request.ViewMode < 0)
            {
                reponse.AddError(nameof(request.ViewMode), "Thiếu thông tin");
            }

            var result = await _cVUtilities.UpdateViewModel(
                new CVChangeViewModeRequest()
                {

                    Identi = request.Identi,
                    HandleBy = int.Parse(resultUser.Id),
                    ViewMode = request.ViewMode,
                }
              );
            reponse.Data = result;
            return StatusCode(reponse.StatusCode, reponse);
        }



        [HttpPost]
        public async Task<ActionResult> AddLogStatus(IntputCVStatusHistoryRequest request)
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

            if (string.IsNullOrEmpty(request.Noted))
            {
                reponse.AddError(nameof(request.Noted), "Thiếu thông tin");
            }

            var result = await _cVUtilities.AddHistoryStatus(
                new CVStatusHistoryRequest()
                {
                    Identi = request.Identi,
                    HandleBy = int.Parse(resultUser.Id),
                    Noted = request.Noted,
                    NoteCode = request.NoteCode
                }
              );
            reponse.Data = result;
            return StatusCode(reponse.StatusCode, reponse);
        }






    }
}
