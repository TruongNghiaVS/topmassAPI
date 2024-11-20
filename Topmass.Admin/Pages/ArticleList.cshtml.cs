using Microsoft.AspNetCore.Mvc;
using Topmass.Admin;
using Topmass.Admin.Business;
using Topmass.Admin.Pages.Model.search;

namespace crmHuman.Pages
{
    //[Authorize]

    public class ArticleListModel : BaseModel
    {
        private readonly ILogger<ArticleListModel> _logger;
        public List<string> TableColumnTextAdmin { get; set; }
        public SearchArticleRequest RequestSearch { get; set; }
        public List<object> DataAll { get; set; }
        public string TitlePage { get; set; }
        public string NameController { get; set; }
        public string KeyPage { get; set; }
        public List<string> TableColumnText { get; set; }
        private IAdminArticleBusiness business { get; set; }
      
        public ArticleListModel(ILogger<ArticleListModel> logger,
            IAdminArticleBusiness _business

            )
        {
            _logger = logger;
            TitlePage = "Danh sách bài viết";
            KeyPage = "article";
            TableColumnText = new List<string>()
            {
                "STT","Mã","Tiêu đề", "Mô tả ngắn",  "Hình ảnh",  "Từ khóa", "Cập nhật gần nhất","Thao tác"
            };
            NameController = "Article";
            business = _business;
            DataAll = new List<object>();
        }
        public async Task<ActionResult> OnGet([FromQuery] SearchArticleRequest request)
        {

            return await GetAll(request);
        }
        public async Task<ActionResult> GetAll(SearchArticleRequest request2)
        {
            RequestSearch = request2;
            var dataAll = await business.GetAll(new SearchArticleRequest()
            {
                From = request2.From,
                Limit = request2.Limit,
                Page = request2.Page,
                To = request2.To
            
            });

            foreach (var item in dataAll.Data.Data)
            {
                DataAll.Add(item);
            }
            return Page();
        }

        public virtual async Task<PartialViewResult> OnGetFormEdit(int id)
        {
            var resultView = new
            {
                Id = -1
            };
            return Partial("PartialView/Article/EditNTD", resultView);
        }

     


        public async Task<IActionResult> OnPostDelete
            (int Id = -1)
        {

            var listEror = new List<object>();
            if (Id < 0)
            {
                var itemError = new
                {
                    name = "id",
                    Content = "Thiếu thông tin cần xoá"
                };
                listEror.Add(itemError);

            }
            if (listEror.Count > 0)
            {
                return new JsonResult(listEror)
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
            var result = await business.Delete(Id);
            var dataReponse = new
            {
                success = result,

            };
            return new JsonResult(dataReponse)
            {
                StatusCode = StatusCodes.Status200OK

            };
        }
    }
}
