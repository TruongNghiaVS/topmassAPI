using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Topmass.Campagn.Business;
using Topmass.Campagn.Business.Model;
using Topmass.Job.Business;
using Topmass.Job.Business.Model;
using Topmass.Recruiter.Model;
using TopMass.Core.Result;

namespace Topmass.Recruiter.Controllers
{
    [ApiController]
    [Authorize]
    public class CampagnController : BaseController
    {

        private readonly ILogger<CampagnController> _logger;
        private readonly ICampagnBusiness _bussiness;
        private readonly IJobBusiness _jobBusiness;

        public CampagnController(ILogger<CampagnController> logger,
            ICampagnBusiness articleBusiness,
            IJobBusiness jobBusiness
            ) : base(logger)
        {
            _logger = logger;
            _bussiness = articleBusiness;
            _jobBusiness = jobBusiness;

        }

        [HttpPost]
        public async Task<ActionResult> Add(CampagnItemRequestAdd request)
        {
            var resultUser = await GetCurrentUser();
            var reponse = new BaseResult();

            if (string.IsNullOrEmpty(request.Name))
            {
                reponse.AddError(nameof(request.Name), "Thiếu thông tên");
            }
            if (!reponse.Success)
            {
                return StatusCode(reponse.StatusCode, reponse);
            }
            var requestAdd = new CampagnItemAdd()
            {
                From = DateTime.Now,
                To = DateTime.Now,
                Name = request.Name,
                Email = resultUser.UserName,
                HandleBy = int.Parse(resultUser.Id)
            };
            var result = await _bussiness.AddCampagn(requestAdd);
            return StatusCode(result.StatusCode, result);
        }
        [HttpPost]
        public async Task<ActionResult> ChangeStatus(CampagnItemStatusRequestUpdate request)
        {
            var resultUser = await GetCurrentUser();
            var reponse = new BaseResult();

            if (request.IdUpdate < 1)
            {
                reponse.AddError(nameof(request.IdUpdate), "Thiếu thông đối tượng ");
            }
            if (request.Status < 0)
            {
                reponse.AddError(nameof(request.Status), "Thiếu thông tin trạng thái");
            }
            if (!reponse.Success)
            {
                return StatusCode(reponse.StatusCode, reponse);
            }
            var requestAdd = new CampagnItemStatusUpdate()
            {
                IdUpdate = request.IdUpdate,
                Status = request.Status
            };
            var result = await _bussiness.ChangeStatusActive(requestAdd);
            return StatusCode(result.StatusCode, result);
        }
        [HttpPost]
        public async Task<ActionResult> Update(CampagnItemRequestUpdate request)
        {
            var resultUser = await GetCurrentUser();
            var reponse = new BaseResult();

            if (request.IdUpdate < 1)
            {
                reponse.AddError(nameof(request.IdUpdate), "Thiếu thông đối tượng ");
            }
            if (string.IsNullOrEmpty(request.Name))
            {
                reponse.AddError(nameof(request.Name), "Thiếu thông tên");
            }
            if (!reponse.Success)
            {
                return StatusCode(reponse.StatusCode, reponse);
            }
            var requestAdd = new CampagnItemUpdate()
            {
                From = DateTime.Now,
                To = DateTime.Now,
                IdUpdate = request.IdUpdate,
                Status = request.Status,

                Name = request.Name,
                Email = resultUser.UserName,
                HandleBy = int.Parse(resultUser.Id)
            };
            var result = await _bussiness.UpdateCampagn(requestAdd);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery] CampagnSearchRequestFilter request)
        {
            var resultUser = await GetCurrentUser();
            var reponse = new BaseResult();
            var requestAdd = new CampagnSearchFilter()
            {
                From = request.From,
                To = request.To,
                Status = request.Status,
                Email = resultUser.UserName,
                HandleBy = int.Parse(resultUser.Id)
            };
            var result = await _bussiness.GetAll(requestAdd);
            return StatusCode(result.StatusCode, result);
        }



        [HttpGet]
        public async Task<ActionResult> GetJobs([FromQuery] JobSearchRequestFilter request)
        {
            var resultUser = await GetCurrentUser();
            var reponse = new BaseResult();

            if (!request.CampaigId.HasValue)
            {
                reponse.AddError(nameof(request.CampaigId), "Không có thông tin");
                return StatusCode(reponse.StatusCode, reponse);
            }
            var requestAdd = new JobSearchRequest()
            {
                From = request.From,
                To = request.To,
                Status = request.Status,
                Email = resultUser.UserName,
                Limit = 10,
                Page = 1,
                CampagnId = request.CampaigId,
                Userid = int.Parse(resultUser.Id)
            };
            var result = await _jobBusiness.GetallJob(requestAdd);
            return StatusCode(result.StatusCode, result);
        }




    }
}
