using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using topmass.Controllers;
using Topmass.CV.Business;
using Topmass.CV.Business.Model;
using TopMass.Core.Result;

namespace topmass.Model
{
    [ApiController]
    [Authorize]
    public class CVController : BaseController
    {

        private readonly ILogger<CVController> _logger;

        private readonly ICVBusiness _cVBusiness;

        private readonly ICVUserBusiness _userBusiness;
        public CVController(ILogger<CVController> logger,

            ICVBusiness cVBusiness,
             ICVUserBusiness userBusiness
            ) : base(logger)
        {
            _logger = logger;

            _cVBusiness = cVBusiness;
            _userBusiness = userBusiness;
        }

        [HttpPost]
        public async Task<ActionResult> CreateCV(CreateCVAddRequest request)
        {
            var resultUser = await GetCurrentUser();
            var reponse = new BaseResult();

            if (string.IsNullOrEmpty(request.LinkFile))
            {
                reponse.AddError(nameof(request.LinkFile), "Thiếu thông tên");
            }

            if (request.TypeData < 1)
            {
                reponse.AddError(nameof(request.TypeData), "Thiếu thông tin phân loại");
            }
            if (!reponse.Success)
            {
                return StatusCode(reponse.StatusCode, reponse);
            }
            var requestAdd = new CVRequestAdd()
            {
                TypeData = request.TypeData,
                LinkFile = request.LinkFile,
                UserId = int.Parse(resultUser.Id),

                TemplateID = 1,
                HandleBy = int.Parse(resultUser.Id)
            };
            var result = await _cVBusiness.CreateCV(requestAdd);
            reponse.Data = result;
            return StatusCode(reponse.StatusCode, reponse);
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetAllCV()
        {


            var userId = -1;
            if (User.Identity.IsAuthenticated)
            {
                var resultUser = await GetCurrentUser();
                userId = resultUser.UserId;
            }
            else
            {
                var reponse2 = new BaseResult();
                reponse2.Data = new List<dynamic>();
                return StatusCode(reponse2.StatusCode, new
                {
                    Data = new List<Object>()
                });

            }


            var result = await _userBusiness.GetAllCV(userId);
            var itemList = new List<dynamic>();
            foreach (var item in result.Data)
            {
                var itemAdd = new
                {
                    item.Id,
                    typeInfo = item.TypeData,
                    item.LinkFile,
                    item.Create_At
                };
                itemList.Add(itemAdd);
            }
            var reponse = new BaseResult();
            reponse.Data = itemList;
            return StatusCode(reponse.StatusCode, result);
        }


        [HttpGet]
        public async Task<ActionResult> GetInfo([FromQuery] InputGetInfoCV request)
        {
            var resultUser = await GetCurrentUser();
            var reponse = new BaseResult();
            if (request.CVId < 1)
            {
                reponse.AddError(nameof(request.CVId), "Thiếu thông tin CV ");
                return StatusCode(reponse.StatusCode, reponse);
            }

            var result = await _cVBusiness.GetInfo(new GetInfoCVRequest()
            {
                CVId = request.CVId
            });
            reponse.Data = result;
            return StatusCode(reponse.StatusCode, reponse);
        }

    }
}
