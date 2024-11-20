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
    public class JobSearchController : BaseController
    {

        private readonly ILogger<JobSearchController> _logger;
        private readonly IJobSearchBusiness _business;
        private readonly IRegionalBusiness _regionalBusiness;
        public JobSearchController(ILogger<JobSearchController> logger,
            IJobSearchBusiness jobsearchBusiness,
            IRegionalBusiness regionalBusiness
            ) : base(logger)
        {
            _logger = logger;
            _business = jobsearchBusiness;
            _regionalBusiness = regionalBusiness;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetAllBestJobOptimization([FromQuery] GetAllBestJobOptimization request)
        {
            var userId = -1;
            if (User.Identity.IsAuthenticated)
            {
                var resultUser = await GetCurrentUser();
                userId = resultUser.UserId;

            }
            var reponse = new BaseResult();
            var searchParma = new GetAllBestJobOptimizationRequest()
            {
                From = request.From,
                To = request.To,
                OrderBy = request.OrderBy,
                Page = request.Page,
                UserId = userId,
                TypeSearch = request.TypeSearch,
                ValueType = request.ValueType,
                Limit = request.Limit
            };
            var result = await _business.GetAllBestJobOptimization(searchParma);
            reponse.Data = result;
            return StatusCode(reponse.StatusCode, reponse);

        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetAttractiveJobs([FromQuery] GetAttractiveJobsRequest request)
        {
            var reponse = new BaseResult();
            var modeGet = request.ModeGet;
            var userId = -1;
            if (User.Identity.IsAuthenticated)
            {
                var resultUser = await GetCurrentUser();
                userId = resultUser.UserId;
            }
            var locationSearch = "";
            if (modeGet > 0)
            {
                var locationInfo = await _regionalBusiness.GetInfoByCode(modeGet.Value);
                if (locationInfo != null)
                {
                    locationSearch = locationInfo.Slug;
                }
            }
            var requestSearch = new GetAttractiveJobs()
            {
                LocationSearch = locationSearch,
                Limit = request.Limit,
                Page = request.Page,
                UserId = userId
            };
            var result = await _business.GetAttractiveJobs(requestSearch);
            reponse.Data = result;
            return StatusCode(reponse.StatusCode, reponse);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetSuitableJob([FromQuery] InputGetSuitableJobRequest request)
        {
            var reponse = new BaseResult();
            var userId = -1;
            if (User.Identity.IsAuthenticated)
            {
                var resultUser = await GetCurrentUser();
                userId = resultUser.UserId;
            }
            var locationSearch = "";
            var requestSearch = new GetSuitableJobRequest()
            {
                LocationSearch = locationSearch,
                Limit = request.Limit,
                Page = request.Page,
                UserId = userId
            };
            var result = await _business.GetSuitableJob(requestSearch);
            reponse.Data = result;
            return StatusCode(reponse.StatusCode, reponse);
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetJobsLastest([FromQuery] GetJobLastestRequest request)
        {
            var reponse = new BaseResult();
            var result = await _business.GetAllBestJobOptimization(new GetAllBestJobOptimizationRequest());
            reponse.Data = result;
            return StatusCode(reponse.StatusCode, reponse);

        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> SearchJob([FromQuery] InputSearchJobsRequest request)
        {

            var userId = -1;
            if (User.Identity.IsAuthenticated)
            {
                var resultUser = await GetCurrentUser();
                userId = resultUser.UserId;

            }
            var reponse = new BaseResult();
            var searchParma = new SearchJobRequest()
            {
                Field = request.Field,
                KeyWord = request.KeyWord,
                Location = request.Location,
                ModeGet = request.ModeGet,
                RankLevel = request.RankLevel,
                TypeOfWork = request.TypeOfWork,
                UserId = userId,
                ExperienceId = request.ExperienceId,
                SalaryTo = request.SalaryTo,
                Gender = request.Gender,
                SalaryFrom = request.SalaryFrom,

                ModeOrderBy = request.ModeOrderBy.HasValue ? request.ModeOrderBy.Value : -1
            };
            var result = await _business.SearchJob(searchParma);
            reponse.Data = result;
            return StatusCode(reponse.StatusCode, reponse);

        }

    }
}
