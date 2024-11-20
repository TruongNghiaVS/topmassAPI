using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using topmass.Controllers;
using Topmass.CV.Business;
using Topmass.Job.Business;
using TopMass.Core.Result;

namespace topmass.Model
{
    [ApiController]
    [Authorize]
    public class JobUtilitiesController : BaseController
    {

        private readonly ILogger<JobUtilitiesController> _logger;
        private readonly IJobUtilitiesBusiness _jobBusiness;



        public JobUtilitiesController(ILogger<JobUtilitiesController> logger,

            ICVBusiness cVBusiness,
            IJobUtilitiesBusiness jobBusiness
            ) : base(logger)
        {
            _logger = logger;

            _jobBusiness = jobBusiness;
        }

        [HttpPost]
        public async Task<ActionResult> AddJobSave(InputJobSave request)
        {
            var resultUser = await GetCurrentUser();
            var reponse = new BaseResult();

            if (string.IsNullOrEmpty(request.JobId))
            {
                reponse.AddError(nameof(request.JobId), "Thiếu thông tin việc làm");
            }
            if (!reponse.Success)
            {
                return StatusCode(reponse.StatusCode, reponse);
            }

            await _jobBusiness.AddJobSave(request.JobId, int.Parse(resultUser.Id));

            return StatusCode(reponse.StatusCode, true);
        }

        [HttpPost]
        public async Task<ActionResult> RemoveJobSave(InputJobId request)
        {
            var resultUser = await GetCurrentUser();
            var reponse = new BaseResult();
            if (request.JobId < 1)
            {
                reponse.AddError(nameof(request.JobId), "Thiếu thông tin việc làm");
            }
            if (!reponse.Success)
            {
                return StatusCode(reponse.StatusCode, reponse);
            }
            await _jobBusiness.RemoveJobSave(request.JobId, resultUser.UserId);
            return StatusCode(reponse.StatusCode, true);
        }

    }
}
