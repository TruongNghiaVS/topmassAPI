namespace topmass.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using topmass.Model;
    using Topmass.core.Business;
    using Topmass.Web.Business;
    using TopMass.Web.Business;
    using TopMass.Web.Business.Model;

    [ApiController]
    [Authorize]
    public class WebController : BaseController
    {
        private readonly ILogger<WebController> _logger;

        private readonly IArticleBusiness _articleBusiness;
        private readonly ICustomerContactBusiness _customerContactBusiness;

        private readonly ICategoryArticleBusiness _categoryArticleBusiness;

        private readonly IPageBusiness _pageBusiness;

        public WebController(ILogger<WebController> logger,
    IArticleBusiness articleBusiness,
    ICategoryArticleBusiness categoryArticleBusiness,
    IPageBusiness pageBusiness,
    ICustomerContactBusiness customerContactBusiness
    ) : base(logger)
        {
            _logger = logger;
            _articleBusiness = articleBusiness;
            _categoryArticleBusiness = categoryArticleBusiness;
            _pageBusiness = pageBusiness;
            _customerContactBusiness = customerContactBusiness;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetAllCategory()
        {
            var dataError = new ErrorData() { };
            var result = await _articleBusiness.GetAllCatagroy();
            return Ok(result);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetBlogsForHomepage()
        {
            var result = new BaseResult();
            ArticleReqest requestGet = new ArticleReqest()
            {
                CategoryId = 8,
                Limit = 4
            };
            var dataAll = await _articleBusiness.GetAll(requestGet);
            result.Data = dataAll;
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetAllArticle([FromQuery] ArticleReqest reqest)
        {
            var result = new BaseResult();
            ArticleReqest requestGet = reqest;
            var dataAll = await _articleBusiness.GetAll(requestGet);
            result.Data = dataAll;
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetArticle(string articleSlug)
        {

            var result = new BaseResult();
            if (string.IsNullOrEmpty(articleSlug))
            {
                result.AddError(nameof(articleSlug), "thiếu thông tin slug");
            }
            if (!result.Success)
            {
                return StatusCode(result.StatusCode, result);
            }
            var itemArticle = await _articleBusiness.GetAll(new ArticleReqest()
            {
                Slug = articleSlug
            });

            var itemDetail = itemArticle.Data.FirstOrDefault();
            if (itemDetail == null)
            {

            }
            result.Data = itemDetail;
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetAllCategoryWithBlogs()
        {

            var result = new BaseResult();

            var dataAll = await _articleBusiness.GetAllBlogsCategory();
            result.Data = dataAll.Data;
            return StatusCode(result.StatusCode, result);
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetAllArticleRelationship(string articleSlug = "-1")
        {
            var result = new BaseResult();
            var itemArticle = await _articleBusiness.GetAll(new ArticleReqest()
            {
                Slug = articleSlug
            });
            var itemDetail = itemArticle.Data.FirstOrDefault();
            if (itemDetail == null)
            {
                result.Data = new
                {
                    Data = new List<object>()
                };
                return StatusCode(result.StatusCode, result);
            }
            var itemData = itemDetail as dynamic;
            var categoryId = itemData.Linked;
            var tempId = -1;
            try
            {
                tempId = int.Parse(categoryId);
            }
            catch (Exception)
            {

                tempId = -1;
            }
            var dataAll = await _articleBusiness.GetAll(new ArticleReqest()
            {
                CategoryId = tempId
            });
            var dataRelation = new List<dynamic>();
            foreach (var item in dataAll.Data)
            {
                var itemCategory = item as dynamic;
                if (dataRelation.Count > 3)
                {
                    continue;
                }

                if (itemCategory.Id != (itemDetail as dynamic).Id)
                {
                    dataRelation.Add(item);
                }

            }
            result.Data = new
            {
                data = dataRelation
            };
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> AddArticle(ArticleRequestAdd reqest)
        {
            ArticleRequestAdd requestGet = reqest;
            var result = await _articleBusiness.AddOrUpdate(requestGet);
            return Ok(result);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> AddCategory(CategoryRequestAdd reqest)
        {
            var requestGet = reqest;
            var result = await _categoryArticleBusiness.AddOrUpdate(requestGet);
            return Ok(result);
        }

        [HttpDelete]
        [AllowAnonymous]
        public async Task<ActionResult> DeleteArticle(DeleteRequest reqest)
        {
            DeleteRequest _deleteRequest = reqest;
            var result = await _articleBusiness.Delete(_deleteRequest);
            return Ok(result);
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
            var contentpage = await _pageBusiness.GetSeoBySlug(pageSlug);
            if (contentpage == null)
            {
                result.AddError(nameof(pageSlug), "không có thông tin");

                return StatusCode(302, result);
            }
            result.Data = contentpage;
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> AddCustomerContact(ContactCustomerRequest request)
        {

            var result = await _customerContactBusiness.AddRequest(request.Name,
               request.Email,
               request.phoneNumber,
               request.Title,
               request.Content,
                 0);
            return StatusCode(result.StatusCode, result);
        }
    }
}
