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
    public class SearchCVController : BaseController
    {
        private readonly ILogger<SearchCVController> _logger;
        private readonly ICVBusiness _cVBusiness;
        private readonly ICVUtilities _cVUtilities;
        private readonly IProfileCVBusiness _business;
        private readonly ISearchCVBusiness _searchCVBusiness;

        public SearchCVController(ILogger<SearchCVController> logger,
            ICVBusiness cVBusiness,
            ICVUtilities cVUtilities,
            IProfileCVBusiness profileCVBusiness,
            ISearchCVBusiness searchCVBusiness
            ) : base(logger)
        {
            _logger = logger;
            _cVBusiness = cVBusiness;
            _cVUtilities = cVUtilities;
            _business = profileCVBusiness;
            _searchCVBusiness = searchCVBusiness;
        }


        [HttpGet]
        public async Task<ActionResult> Search([FromQuery] InputSearchCV request)
        {
            var resultUser = await GetCurrentUser();
            var reponse = new BaseResult();
            var searchRequest = new SearchCVRequestInfo()
            {
                CvKey = request.CvKey,
                DayOfBirth = null,
                FromYear = request.FromYear,
                ToYear = request.ToYear,
                EducationalLevelArray = request.EducationalLevelArray,
                Gender = request.Gender.HasValue ? request.Gender.Value : -1,
                KeyWord = request.KeyWord,
                Limit = request.Limit,
                LocationCode = request.LocationCode,
                Page = request.Page,
                SchoolSearch = request.SchoolSearch
            };
            var result = await _cVBusiness.SearchCV(searchRequest);
            reponse.Data = result;
            return StatusCode(reponse.StatusCode, reponse);
        }

        [HttpGet]
        public async Task<ActionResult> GetProfileGenerate(string? searchId)
        {
            var resultUser = await GetCurrentUser();
            var result = await _business.GetFullProfileUser(searchId, resultUser.UserId);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        public async Task<ActionResult> GetDetailInfo(string searchId)
        {
            var resultUser = await GetCurrentUser();
            var reponse = new BaseResult();
            var result = await _business.GetDetailInfo(searchId, resultUser.UserId);
            reponse.Data = result;
            return StatusCode(reponse.StatusCode, reponse);
        }

        [HttpPost]
        public async Task<ActionResult> SaveCV(SaveSearchCVRequest request)
        {
            var resultUser = await GetCurrentUser();
            var reponse = new BaseResult();
            await _searchCVBusiness.SaveResultSearch(request.SearchId,
                request.LinkFile, resultUser.UserId, request.Campaign, request.JobId);
            return StatusCode(reponse.StatusCode, reponse);
        }
    }
}
