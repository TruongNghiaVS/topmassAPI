using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Topmass.Notification.Business;
using Topmass.Recruiter.Model;
using TopMass.Core.Result;

namespace Topmass.Recruiter.Controllers
{
    [ApiController]
    [Authorize]
    public class NotificationController : BaseController
    {
        private readonly ILogger<CVController> _logger;
        private readonly INotificationBussiness _business;
        public NotificationController(ILogger<CVController> logger,
            INotificationBussiness business
            ) : base(logger)
        {
            _logger = logger;
            _business = business;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery] InputGetAllNotification request)
        {
            var resultUser = await GetCurrentUser();
            var reponse = new BaseResult();
            var result = await _business.GetAll(new GetallNotificationPushRequest()
            {
                Status = request.Status,
                RelId = -1,
                UserName = resultUser.UserName
            });
            reponse.Data = result;
            return StatusCode(reponse.StatusCode, reponse);
        }

        [HttpGet]
        public async Task<ActionResult> GetDetail([FromQuery] InputGetDetailInfoNotification request)
        {
            var resultUser = await GetCurrentUser();
            var reponse = new BaseResult();
            if (request.Id < 1)
            {
                reponse.AddError(nameof(request.Id), "Thiếu thông tin Id");
                return StatusCode(reponse.StatusCode, request);
            }
            reponse.Data = await _business.GetDetailById(request.Id);
            return StatusCode(reponse.StatusCode, reponse);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateNotificaton(InputChangeStatusUpdate request)
        {
            var resultUser = await GetCurrentUser();
            var reponse = new BaseResult();
            var result = await _business.UpdateStatus(request.Id, request.Status);
            reponse.Data = result;
            return StatusCode(reponse.StatusCode, reponse);
        }
    }
}
