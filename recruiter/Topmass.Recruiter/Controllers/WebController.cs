using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Topmass.Recruiter.Bussiness;
using Topmass.Web.Business;
using TopMass.Core.Result;
using TopMass.Web.Business;
using TopMass.Web.Business.Model;
namespace Topmass.Recruiter.Controllers
{
    [ApiController]

    public class WebController : BaseController
    {
        private readonly ILogger<WebController> _logger;
        private readonly IPageBusiness _pageBusiness;
        private readonly IRecruiterBusiness _recruiterBusiness;
        private readonly ICompanyBusiness _companyBusiness;
        private readonly ICustomerContactBusiness _customerContactBusiness;
        private readonly IMetaWebDataBussiness _metaWebDataBussiness;
        public WebController(ILogger<WebController> logger,
            IRecruiterBusiness recruiterBusiness,
            ICustomerContactBusiness customerContactBusiness,
            ICompanyBusiness companyBusiness,
            IMetaWebDataBussiness metaWebDataBussiness
          ) : base(logger)
        {
            _logger = logger;
            _recruiterBusiness = recruiterBusiness;
            _companyBusiness = companyBusiness;
            _customerContactBusiness = customerContactBusiness;
            _metaWebDataBussiness = metaWebDataBussiness;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetContentPage(string pageSlug)
        {
            var result = new BaseResult();
            if (string.IsNullOrEmpty(pageSlug))
            {
                result.AddError(nameof(pageSlug), "thiếu thông tin slug");
            }
            if (!result.Success)
            {
                return StatusCode(result.StatusCode, result);
            }
            var contentpage = await _pageBusiness.GetContentBySlug(pageSlug);
            if (contentpage == null)
            {
                result.AddError(nameof(pageSlug), "không có thông tin");

                return StatusCode(302, result);
            }
            result.Data = contentpage;
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetInfomationSEO(string pageSlug)
        {
            var result = new BaseResult();
            if (string.IsNullOrEmpty(pageSlug))
            {
                result.AddError(nameof(pageSlug), "thiếu thông tin slug");
            }
            if (!result.Success)
            {
                return StatusCode(result.StatusCode, result);
            }
            var contentpage = await _pageBusiness.GetContentBySlug(pageSlug);
            if (contentpage == null)
            {
                result.AddError(nameof(pageSlug), "không có thông tin");

                return StatusCode(302, result);
            }
            result.Data = contentpage;
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetAllPartner()
        {
            var datas = await _companyBusiness.GetAllPartner();

            return StatusCode(datas.StatusCode, datas.Data);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetInfoMetadata(string keyScreen)
        {
            var result = new BaseResult();
            if (string.IsNullOrEmpty(keyScreen))
            {
                result.AddError(nameof(keyScreen), "thiếu thông tin slug");
            }
            if (!result.Success)
            {
                return StatusCode(result.StatusCode, result);
            }

            var result1 = await _metaWebDataBussiness.GetInfo(new MetaDataRequest()
            {
                KeyScreen = keyScreen
            });
            return StatusCode(result.StatusCode, result1);
        }

        
    }

}
