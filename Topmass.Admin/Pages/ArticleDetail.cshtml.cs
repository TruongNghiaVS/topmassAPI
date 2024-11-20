using Microsoft.AspNetCore.Mvc;
using Topmass.Admin;
using Topmass.Admin.Business;
using Topmass.Admin.Pages.Model;
using Topmass.Admin.Pages.Model.search;

namespace crmHuman.Pages
{
    //[Authorize]

    public class ArticleDetaillModel : BaseModel
    {
        private readonly ILogger<ArticleDetaillModel> _logger;

        public List<string> TableColumnTextAdmin { get; set; }
        public NTDRequest RequestSearch { get; set; }
        public BaseList DataAll { get; set; }

        public string TitlePage { get; set; }

        public string TextButton { get; set; }

        public string NameController { get; set; }
        public string KeyPage { get; set; }
        public dynamic ResultData;
        public List<string> TableColumnText { get; set; }
        private IAdminArticleBusiness business { get; set; }

        public int TotalRecord
        {

            get
            {
                return DataAll.Total;

            }
        }
        public ArticleDetaillModel(ILogger<ArticleDetaillModel> logger,
            IAdminArticleBusiness _business

            )
        {
            _logger = logger;
            TitlePage = "Chi tiết bài viết";
            KeyPage = "NTD";
            TableColumnText = new List<string>()
            {
                "STT","Mã","Tên người đại diện","Tên đăng nhập","Tên công ty", "MST", "Số điện thoại", "Trạng thái", "Cấp độ xác thực","Cập nhật gần nhất","Thao tác"
            };

            TextButton = "Cập nhật";

            NameController = "NTDDetail";
            business = _business;
            DataAll = new BaseList();
        }
        public async Task<ActionResult> OnGet(int id)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return Redirect("/Login");
            }

            return await GetDetail(id);
        }

        public async Task<ActionResult> GetDetail(int orderId)
        {

            var resultView = await business.Detail(orderId);
            var dataView = resultView.Data.Data;
            if (dataView.Id < 1)
            {
                TitlePage = "Thêm mới bài viết";
                TextButton = "Thêm mới";
            }
            ResultData = dataView;
            return Page();
        }


        public async Task<IActionResult> OnpostUpdate(ArticleRequestUpdate request)
        {
            var listEror = new List<object>();
            if (listEror.Count > 0)
            {
                return new JsonResult(listEror)
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
            var result = await business.AddorUpdate(new ArticleRequestAdd()
            {
                CategryIdLink = request.CategryIdLinkArticle,
                Content = request.ContentArticle,
                Id = request.Id,
                Keyword = request.KeywordArticle,
                LinkImage = request.LinkImageArticle,
                ShortDes = request.ShortDesArticle,
                Title = request.TitleArticle


            });
            var dataReponse = new
            {
                success = result,
            };
            return new JsonResult(dataReponse)
            {
                StatusCode = StatusCodes.Status200OK

            };
        }

        public virtual async Task<PartialViewResult> OnGetFormEdit(int id)
        {
            var resultView = new
            {

            };
            return Partial("editOrUpdateEmployee", resultView);
        }


    }
}
