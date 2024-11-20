using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Topmass.Business.History;
using Topmass.Recruiter.Model;
using TopMass.Core.Result;

namespace Topmass.Recruiter.Controllers
{
    [ApiController]
    [Authorize]
    public class HistoryController : BaseController
    {
        private readonly ILogger<JobController> _logger;
        private readonly IHistoryBussiness _bussiness;
        public HistoryController(ILogger<JobController> logger,
            IHistoryBussiness historyBussiness
            ) : base(logger)
        {
            _logger = logger;
            _bussiness = historyBussiness;

        }
        [HttpGet]
        public async Task<ActionResult> GetHistoryLogin([FromQuery] HistoryRequest request)
        {
            var resultUser = await GetCurrentUser();
            var reponse = new BaseResult();
            request.From = new DateTime(request.From.Value.Year, request.From.Value.Month, request.From.Value.Day, 0, 0, 0);
            request.To = new DateTime(request.To.Value.Year, request.To.Value.Month, request.To.Value.Day, 23, 59, 59);
            var result = await _bussiness.GetAccessLog(resultUser.UserId, 2, 1, request.From, request.To);
            reponse.Data = result;
            return StatusCode(reponse.StatusCode, reponse);
        }
        [HttpGet]
        public async Task<ActionResult> GetLogUpdateAccount([FromQuery] HistoryRequest request)
        {
            var resultUser = await GetCurrentUser();
            var reponse = new BaseResult();
            request.From = new DateTime(request.From.Value.Year, request.From.Value.Month, request.From.Value.Day, 0, 0, 0);
            request.To = new DateTime(request.To.Value.Year, request.To.Value.Month, request.To.Value.Day, 23, 59, 59);
            var result = await _bussiness.GetAccessLog(resultUser.UserId, 2, 2, request.From, request.To);
            reponse.Data = result;
            return StatusCode(reponse.StatusCode, reponse);
        }
    }
}
