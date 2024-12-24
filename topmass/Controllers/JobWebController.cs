using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using topmass.Controllers;
using Topmass.Business.Regional;
using Topmass.Job.Business;
using Topmass.Job.Business.Model;
using TopMass.Core.Result;

namespace topmass.Model
{
    [ApiController]

    public class JobWebController : BaseController
    {
        private readonly ILogger<JobWebController> _logger;
        private readonly IJobBusiness _jobBusiness;
        private readonly IRegionalBusiness _regionalbussiness;
        public JobWebController(ILogger<JobWebController> logger,
              IRegionalBusiness regionalBusiness,
            IJobBusiness jobBusiness
            ) : base(logger)
        {
            _logger = logger;


            _jobBusiness = jobBusiness;
            _regionalbussiness = regionalBusiness;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetDetailInfo([FromQuery] InputJobInfoRequest request)
        {
            var reponse = new BaseResult();
            if (string.IsNullOrEmpty(request.JobId))
            {
                reponse.AddError(nameof(request.JobId), "Missing jobId");
                return StatusCode(reponse.StatusCode, reponse);
            }
            var rquest = new CandidateJobInfoRequest()
            {
                Slug = request.JobId
            };
            if (User.Identity.IsAuthenticated)
            {
                var resultUser = await GetCurrentUser();
                rquest.UserId = int.Parse(resultUser.Id);
            }
            var result = await _jobBusiness.GetInfoJOb(rquest);
            reponse.Data = result;
            var itemGlobal = GlobalRegional.GetRegional();
            if (itemGlobal.DataGlobal == null || itemGlobal.DataGlobal.Count < 1)
            {
                await _regionalbussiness.LoadAllData();
            }
            foreach (var item in result.DataJob.LocationsInfoMation)
            {
                if (string.IsNullOrEmpty(item.Location))
                {
                    continue;
                }
                var textDir1 = itemGlobal.GetRegionalById(item.Location);

                result.DataJob.LocationText = textDir1.Name;
                item.LocationText = textDir1.Name;
                foreach (var item1 in item.Districts)
                {
                    if (string.IsNullOrEmpty(item1.District))
                    {
                        continue;
                    }
                    var textDir = itemGlobal.GetRegionalById(item1.District);
                    item1.DistrictText = textDir.Name;
                }
            }
            return StatusCode(reponse.StatusCode, reponse);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetDetailMetadata(string jobSlug)
        {
            var reponse = new BaseResult();
            if (string.IsNullOrEmpty(jobSlug))
            {
                reponse.AddError(nameof(jobSlug), "Missing jobSlug");
                return StatusCode(reponse.StatusCode, reponse);
            }
            var result = await _jobBusiness.GetDetailMetadata(jobSlug);
            reponse.Data = result;
            return StatusCode(reponse.StatusCode, reponse);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetRelationJob([FromQuery] InputJobInfo request)
        {
            var reponse = new BaseResult();
            if (string.IsNullOrEmpty(request.JobId))
            {
                reponse.AddError(nameof(request.JobId), "Missing jobId");
                return StatusCode(reponse.StatusCode, reponse);
            }
            var rquest = new JobRelattionRequest()
            {
                Slug = request.JobId
            };
            if (User.Identity.IsAuthenticated)
            {
                var resultUser = await GetCurrentUser();
                rquest.UserId = int.Parse(resultUser.Id);
            }
            var result = await _jobBusiness.GetRelationJob(rquest);
            reponse.Data = result;
            return StatusCode(reponse.StatusCode, reponse);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetRecommended([FromQuery] InputJobInfo request)
        {
            var reponse = new BaseResult();
            if (string.IsNullOrEmpty(request.JobId))
            {
                reponse.AddError(nameof(request.JobId), "Missing jobId");
                return StatusCode(reponse.StatusCode, reponse);
            }
            var rquest = new JobRelattionRequest()
            {
                Slug = request.JobId
            };
            if (User.Identity.IsAuthenticated)
            {
                var resultUser = await GetCurrentUser();
                rquest.UserId = int.Parse(resultUser.Id);
            }
            var result = await _jobBusiness.GetRelationJob(rquest);
            reponse.Data = result;
            return StatusCode(reponse.StatusCode, reponse);
        }
    }
}
