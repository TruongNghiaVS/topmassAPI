using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Topmass.CV.Business;
using Topmass.Recruiter.Bussiness;
using Topmass.Recruiter.Model;
using TopMass.Core.Result;

namespace Topmass.Recruiter.Controllers
{
    [ApiController]
    [Authorize]
    public class RewardController : BaseController
    {
        private readonly ILogger<SearchCVController> _logger;
        private readonly IRewardBusiness _rewardBusiness;

        private readonly ISearchCVBusiness _searchCVBusiness;
        public RewardController(ILogger<SearchCVController> logger,
            IRewardBusiness rewardBusiness,
            ISearchCVBusiness searchCVBusiness
            ) : base(logger)
        {
            _logger = logger;
            _rewardBusiness = rewardBusiness;
            _searchCVBusiness = searchCVBusiness;
        }

        [HttpPost]
        public async Task<ActionResult> OpenCV(OpenSearchCVRequest request)
        {
            var resultUser = await GetCurrentUser();
            var reponse = new BaseResult();
            var serchId = request.SearchId.HasValue ? request.SearchId.Value : -1;
            await _rewardBusiness.ExchangePointToOpenCV(serchId,
                2, resultUser.UserId, request.Campaign);
            return StatusCode(reponse.StatusCode, reponse);
        }



    }
}
