using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Topmass.Recruiter.Bussiness;
using Topmass.Recruiter.Bussiness.Model;
using Topmass.Recruiter.Model;
using TopMass.Core.Result;

namespace Topmass.Recruiter.Controllers
{
    [ApiController]
    [Authorize]
    public class SupportController : BaseController
    {

        private readonly ILogger<SupportController> _logger;
        private readonly ISupportBusiness _bussiness;

        public SupportController(ILogger<SupportController> logger,
            ISupportBusiness articleBusiness

            ) : base(logger)
        {
            _logger = logger;
            _bussiness = articleBusiness;

        }

        [HttpPost]
        public async Task<ActionResult> CreateTicket(InputTicketItemAdd request)
        {
            var resultUser = await GetCurrentUser();
            var reponse = new BaseResult();

            if (string.IsNullOrEmpty(request.Title))
            {
                reponse.AddError(nameof(request.Title), "Thiếu thông tin tiêu đề");
            }
            if (string.IsNullOrEmpty(request.Content))
            {
                reponse.AddError(nameof(request.Content), "Hãy để lại mô  tả  ngắn");
            }
            if (string.IsNullOrEmpty(request.LinkFile))
            {
                reponse.AddError(nameof(request.LinkFile), "Đưa cho chúng tôi, hình ảnh minh hoạ...");
            }
            if (!reponse.Success)
            {
                return StatusCode(reponse.StatusCode, reponse);
            }

            var requestAdd = new TicketItemAdd()
            {
                Content = request.Content,
                Title = request.Title,
                LinkFile = request.LinkFile,
                HandleBy = int.Parse(resultUser.Id)
            };
            var result = await _bussiness.AddTicket(requestAdd);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<ActionResult> CreateRequestContact(InputTicketItemAdd request)
        {
            var resultUser = await GetCurrentUser();
            var reponse = new BaseResult();

            if (string.IsNullOrEmpty(request.Title))
            {
                reponse.AddError(nameof(request.Title), "Thiếu thông tin tiêu đề");
            }

            if (!reponse.Success)
            {
                return StatusCode(reponse.StatusCode, reponse);
            }

            var requestAdd = new TicketItemAdd()
            {
                Content = request.Content,
                Title = request.Title,
                LinkFile = request.LinkFile,
                HandleBy = int.Parse(resultUser.Id)
            };
            var result = await _bussiness.AddTicket(requestAdd);
            return StatusCode(result.StatusCode, result);
        }

    }
}
