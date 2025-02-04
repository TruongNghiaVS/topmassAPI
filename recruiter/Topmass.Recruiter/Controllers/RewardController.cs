﻿using Microsoft.AspNetCore.Authorization;
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
        [HttpPost]
        public async Task<ActionResult> OpenCVNoSearchCV(OpenSearchCVRequestNoSearch request)
        {
            var resultUser = await GetCurrentUser();
            var reponse = new BaseResult();
            await _rewardBusiness.ExchangePointToOpenCVNoSearchCV(request.SearchId, 2, resultUser.UserId, request.Identify, request.LinkFile);
            return StatusCode(reponse.StatusCode, reponse);
        }

        [HttpPost]
        public async Task<ActionResult> OpenViewer(OpenViewerCVRequest request)
        {
            var resultUser = await GetCurrentUser();
            var reponse = new BaseResult();

            if (request.ViewerId < 1)
            {
                reponse.Message = "Thiếu đối tượng mở";
                return StatusCode(reponse.StatusCode, reponse);
            }
            await _rewardBusiness.ExchangePointToOpenViewer(resultUser.UserId, 2, request.ViewerId.Value);
            return StatusCode(reponse.StatusCode, reponse);
        }
    }
}
