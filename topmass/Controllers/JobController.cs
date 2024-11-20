using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using topmass.Controllers;
using Topmass.CV.Business;
using Topmass.CV.Business.Model;
using Topmass.Job.Business;
using TopMass.Core.Result;

namespace topmass.Model
{
    [ApiController]
    [Authorize]
    public class JobController : BaseController
    {
        private readonly ILogger<JobController> _logger;
        private readonly ICVBusiness _cVBusiness;
        private readonly IJobBusiness _jobBusiness;

        public JobController(ILogger<JobController> logger,

            ICVBusiness cVBusiness,
            IJobBusiness jobBusiness
            ) : base(logger)
        {
            _logger = logger;

            _cVBusiness = cVBusiness;
            _jobBusiness = jobBusiness;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> AddViewForJob(AddViewForJobRequest request)
        {
            var reponse = new BaseResult();
            var userId = -1;
            if (User.Identity.IsAuthenticated)
            {
                var resultUser = await GetCurrentUser();
                userId = resultUser.UserId;
            }

            if (string.IsNullOrEmpty(request.JobId))
            {
                reponse.AddError("thiếu thông tin jobid");
                return StatusCode(reponse.StatusCode, reponse);
            }
            var result = await _jobBusiness.AddViewJob(
                new Topmass.Job.Business.Model.ViewJobUserAddRequest()
                {
                    jobslug = request.JobId,
                    Userid = userId
                }
             );
            reponse.Data = result;
            return StatusCode(reponse.StatusCode, reponse);
        }

        [HttpPost]
        public async Task<ActionResult> ApplyJob(InputApplyJobAddRequest request)
        {
            var resultUser = await GetCurrentUser();
            var reponse = new BaseResult();
            if (request.CVId < 1)
            {
                reponse.AddError(nameof(request.CVId), "Thiếu thông tin file CV");
            }
            if (string.IsNullOrEmpty(request.JobId))
            {
                reponse.AddError(nameof(request.JobId), "Thiếu thông tin việc làm");
            }
            if (!reponse.Success)
            {
                return StatusCode(reponse.StatusCode, reponse);
            }
            var requestAdd = new ApplyJobRequestAdd()
            {
                JobSlug = request.JobId,
                CVId = request.CVId,
                FullName = request.FullName,
                Phone = request.Phone,
                Email = request.Email,
                HandleBy = int.Parse(resultUser.Id)
            };
            var result = await _cVBusiness.ApplyJob(requestAdd);

            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<ActionResult> ApplyJobWithCreateCV(InputApplyJobWithCreateCVRequest request)
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
            var requestAdd = new ApplyJobWithCreateCVAdd()
            {
                Email = request.Email,
                UserId = int.Parse(resultUser.Id),
                TypeData = request.TypeData.HasValue ? request.TypeData.Value : 0,
                FullName = request.FullName,
                jobSlug = request.JobId,
                LinkFile = request.LinkFile,
                Phone = request.Phone,
                TemplateID = request.TemplateID

            };
            var result = await _cVBusiness.ApplyJobWithCV(requestAdd);

            return StatusCode(result.StatusCode, result);
        }



    }
}
