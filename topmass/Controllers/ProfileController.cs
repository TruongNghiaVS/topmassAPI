using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using topmass.Model;
using Topmass.core.Business;
using Topmass.core.Business.Model;

namespace topmass.Controllers
{
    [ApiController]
    [Authorize]
    public class ProfileController : BaseController
    {

        private readonly ILogger<ProfileController> _logger;


        private readonly ICandidateBusiness _candidateBusiness;

        public ProfileController(ILogger<ProfileController> logger,

            ICandidateBusiness candidateBusiness) : base(logger)
        {
            _logger = logger;

            _candidateBusiness = candidateBusiness;

        }

        [HttpPost]
        public async Task<ActionResult> UpdateBasicInfo(UpdateBasicRequest request)
        {
            var resultUser = await GetCurrentUser();
            var dataError = new ErrorData() { };
            if (dataError.HasErorr())
            {
                return StatusCode(303, dataError);
            }
            var requestUpdate = new CandidateInfoUpdate()
            {
                FirstName = request.FirstName,
                AvatarLink = request.AvatarLink,
                FullName = request.FullName,

                HandleBy = int.Parse(resultUser.Id),
                LastName = request.LastName,
                Phone = request.Phone,
                UserId = int.Parse(resultUser.Id)
            };
            var result = await _candidateBusiness.UpdateInfoCandidate(requestUpdate);
            return StatusCode(result.StatusCode, result);
        }



        [HttpPost]
        public async Task<ActionResult> UpdateMode(EnableDisableModeRequest request)
        {
            var resultUser = await GetCurrentUser();
            var dataError = new ErrorData() { };
            if (dataError.HasErorr())
            {
                return StatusCode(303, dataError);
            }
            var requestUpdate = new CandidateInfoUpdate()
            {
                HandleBy = int.Parse(resultUser.Id),

                PrivateMode = request.WorkMode,
                PublicMode = request.SearchMode,
                UserId = int.Parse(resultUser.Id)
            };
            var result = await _candidateBusiness.UpdateInfoCandidate(requestUpdate);
            return StatusCode(result.StatusCode, result);
        }


        [HttpPost]
        [Authorize]
        public async Task<ActionResult> ChangePassword(PasswordChanged request)
        {
            var resultUserChange = await GetCurrentUser();
            var dataError = new ErrorData() { };
            if (string.IsNullOrEmpty(request.Password))
            {
                dataError.AddError(nameof(request.Password), "Thiếu thông tin mật khẩu");
            }
            if (dataError.HasErorr())
            {
                return StatusCode(303, dataError);
            }
            var result = await _candidateBusiness.ChangePassword(request.Password, int.Parse(resultUserChange.Id));
            return StatusCode(result.StatusCode, result);
        }




    }
}
