using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using topmass.Controllers;
using Topmass.Bussiness.Company;
using Topmass.Bussiness.Company.Model;
using Topmass.core.Business;

namespace topmass.Model
{
    [ApiController]
    [Authorize]
    public class CompanyController : BaseController
    {
        private readonly ILogger<CompanyController> _logger;
        private readonly ICompanyBusiness _companyBusiness;
        public CompanyController(ILogger<CompanyController> logger,
             ICompanyBusiness companyBusiness
            ) : base(logger)
        {
            _logger = logger;
            _companyBusiness = companyBusiness;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetAllCompany([FromQuery] InputCompanyRequestGetAll request)
        {
            var resultUser = await GetCurrentUser();
            var baseReult = new BaseResult();
            var datas =
                await _companyBusiness
                .GetAllCompany(new GetAllCompanyRequest()
                {
                    Keyword = request.KeyWord,
                    LoadJob = request.LoadDataJob,
                    UserId = resultUser.UserId
                });
            baseReult.Data = datas;
            return StatusCode(baseReult.StatusCode, baseReult);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetDetail([FromQuery] InputCompanyGetInfo request)
        {


            var baseReult = new BaseResult();
            if (string.IsNullOrEmpty(request.Slug))
            {
                baseReult.AddError(nameof(request.Slug), "thiếu thông tin đối tượng");
            }
            var userId = -1;
            if (User.Identity.IsAuthenticated)
            {
                var resultUser = await GetCurrentUser();
                userId = resultUser.UserId;

            }
            var datas =
               await _companyBusiness
               .GetInfomationDetail(request.Slug, userId);
            baseReult.Data = datas;
            return StatusCode(baseReult.StatusCode, baseReult);
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetAllJobOfCompany([FromQuery] InputCompanyGetInfo request)
        {

            var baseReult = new BaseResult();
            if (string.IsNullOrEmpty(request.Slug))
            {
                baseReult.AddError(nameof(request.Slug), "thiếu thông tin đối tượng");
            }
            var userId = -1;
            if (User.Identity.IsAuthenticated)
            {
                var resultUser = await GetCurrentUser();
                userId = resultUser.UserId;

            }
            var datas =
               await _companyBusiness
               .GetAllJobOfCompany(request.Slug, userId, request.Location, request.Keyword);
            baseReult.Data = datas;
            return StatusCode(baseReult.StatusCode, baseReult);
        }

        [HttpPost]
        public async Task<ActionResult> AddFolow(InputCompanyFlower request)
        {
            var resultUser = await GetCurrentUser();
            var baseReult = new BaseResult();
            if (request.Slug < 1)
            {
                baseReult.AddError(nameof(request.Slug), "thiếu thông tin đối tượng");
            }
            var datas =
               await _companyBusiness
               .AddFollow(request.Slug, resultUser.UserId);
            baseReult.Data = datas;
            return StatusCode(baseReult.StatusCode, baseReult);
        }


        [HttpPost]
        public async Task<ActionResult> RemoveFolow(InputCompanyFlower request)
        {
            var resultUser = await GetCurrentUser();
            var baseReult = new BaseResult();
            if (request.Slug < 1)
            {
                baseReult.AddError(nameof(request.Slug), "thiếu thông tin đối tượng");
            }
            var datas =
               await _companyBusiness
               .RemoveFolow(request.Slug, resultUser.UserId);
            baseReult.Data = datas;
            return StatusCode(baseReult.StatusCode, baseReult);
        }




    }
}
