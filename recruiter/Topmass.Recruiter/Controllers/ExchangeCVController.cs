using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Topmass.CV.Business;
using Topmass.Recruiter.Bussiness;
using Topmass.Recruiter.Bussiness.Model;
using Topmass.Recruiter.Model;
using TopMass.Core.Result;

namespace Topmass.Recruiter.Controllers
{
    [ApiController]
    [Authorize]
    public class ExchangeCVController : BaseController
    {
        private readonly ILogger<ExchangeCVController> _logger;
        private readonly ICVBusiness _cVBusiness;
        private readonly ICVUtilities _cVUtilities;
        private readonly IProfileCVBusiness _business;
        private readonly IExchangeCVBusiness _exchangeCVBusiness;
        private readonly ISearchCVBusiness _searchCVBusiness;

        public ExchangeCVController(ILogger<ExchangeCVController> logger,

            ICVBusiness cVBusiness,
            ICVUtilities cVUtilities,
            IProfileCVBusiness profileCVBusiness,
            ISearchCVBusiness searchCVBusiness,
            IExchangeCVBusiness exchangeCVBusiness
            ) : base(logger)
        {
            _logger = logger;
            _cVBusiness = cVBusiness;
            _cVUtilities = cVUtilities;
            _business = profileCVBusiness;
            _searchCVBusiness = searchCVBusiness;
            _exchangeCVBusiness = exchangeCVBusiness;
        }

        [HttpPost]
        public async Task<ActionResult> ExchangeCV(InputExchangeCVRequest request)
        {
            var resultUser = await GetCurrentUser();
            var reponse = new BaseResult();
            if (string.IsNullOrEmpty(request.Title))
            {
                reponse.AddError(nameof(request.Title), "Thiếu tiêu đề");
            }
            if (string.IsNullOrEmpty(request.Position))
            {
                reponse.AddError(nameof(request.Position), "thiếu vị trí ứng tuyển");
            }
            if (string.IsNullOrEmpty(request.Rank))
            {
                reponse.AddError(nameof(request.Rank), "thiếu cấp độ ứng tuyển");
            }
            if (string.IsNullOrEmpty(request.Experience))
            {
                reponse.AddError(nameof(request.Experience), "thiếu kinh nghiệm ứng tuyển");
            }
            if (request.LinkCVs.Count < 1)
            {
                reponse.AddError("link CV", "thiếu file CV ứng tuyển");
            }
            if (!reponse.Success)
            {
                return StatusCode(reponse.StatusCode, reponse);
            }
            var listfileCV = new List<LinkFileExchangeCV>();
            var point = 0;
            foreach (var item in request.LinkCVs)
            {
                listfileCV.Add(new LinkFileExchangeCV()
                {
                    LinkFile = item
                });
                point += 2;
            }
            var searchRequest = new ExchangeCVRequestAdd()
            {
                BusinessDate = DateTime.Now,
                Experience = request.Experience,
                Position = request.Position,
                Rank = request.Rank,
                Title = request.Title,
                UserId = resultUser.UserId,
                LinkCVs = listfileCV,
                Point = point
            };
            var result = await _exchangeCVBusiness.AddExchange(searchRequest);
            return StatusCode(result.StatusCode, result);
        }


        [HttpGet]
        public async Task<ActionResult> GetHistory([FromQuery] InputExchangeSearchRequest request)
        {
            var resultUser = await GetCurrentUser();
            var result = await _exchangeCVBusiness.GetHistory(request.Status, resultUser.UserId);
            return StatusCode(result.StatusCode, result);
        }


        [HttpGet]
        public async Task<ActionResult> GetDetail([FromQuery] InputExchangeDetail input)
        {
            var resultUser = await GetCurrentUser();
            var reponse = new BaseResult();
            if (input.Id < 1)
            {
                reponse.AddError("Id", "thiếu thông tin Id");
            }
            if (!reponse.Success)
            {
                return StatusCode(reponse.StatusCode, reponse);
            }
            var result = await _exchangeCVBusiness.GetDetail(input.Id);
            return StatusCode(result.StatusCode, result);
        }
        [HttpPost]
        public async Task<ActionResult> CancleItem(InputExchangeDetail input)
        {
            var resultUser = await GetCurrentUser();
            var reponse = new BaseResult();
            if (input.Id < 1)
            {
                reponse.AddError("Id", "thiếu thông tin Id");
            }
            if (!reponse.Success)
            {
                return StatusCode(reponse.StatusCode, reponse);
            }
            var result = await _exchangeCVBusiness.CancelExchange(input.Id);
            reponse.Data = result;
            return StatusCode(result == true ? 200 : 302, reponse);
        }
    }
}
