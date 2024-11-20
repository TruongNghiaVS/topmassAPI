using Microsoft.AspNetCore.Mvc;
using Topmass.Admin;
using Topmass.Admin.Business;
using Topmass.Admin.Pages.Model.search;

namespace crmHuman.Pages
{
    //[Authorize]

    public class NTDModel : BaseModel
    {
        private readonly ILogger<NTDModel> _logger;

        public List<string> TableColumnTextAdmin { get; set; }
        public NTDRequest RequestSearch { get; set; }
        public BaseList DataAll { get; set; }

        public string TitlePage { get; set; }

        public string NameController { get; set; }
        public string KeyPage { get; set; }

        public List<string> TableColumnText { get; set; }
        private INTDBusiness business { get; set; }

        public int TotalRecord
        {

            get
            {
                return DataAll.Total;

            }
        }
        public NTDModel(ILogger<NTDModel> logger,
            INTDBusiness _business

            )
        {
            _logger = logger;
            TitlePage = "Danh sách nhà tuyển dụng";
            KeyPage = "NTD";
            TableColumnText = new List<string>()
            {
                "STT","Mã","Tên người đại diện","Tên đăng nhập",
                "Tên công ty", "MST", "Số điện thoại",  "Trạng thái",
                "Xác thực","Chứng từ", "Cập nhật gần nhất","Thao tác"
            };

            NameController = "NTD";
            business = _business;
            DataAll = new BaseList();
        }
        public async Task<ActionResult> OnGet([FromQuery] NTDRequest request)
        {

            return await GetAll(request);
        }
        public async Task<ActionResult> GetAll(NTDRequest request2)
        {
            RequestSearch = request2;
            var dataAll = await business.GetAllNTD(new SearchNTDRequest()
            {
                From = request2.From,
                Limit = request2.Limit,
                Page = request2.Page,
                To = request2.To,
                Token = request2.Token,
                Status = request2.CbStatus,
                OrderBy = request2.Orderby,
                CbDocumnetStatus = request2.CbDocumnetStatus,
                AuthenLevel = int.Parse(request2.AuthenLevel)
            });
            DataAll.Data = dataAll.Data;
            return Page();
        }

        public virtual async Task<PartialViewResult> OnGetFormEdit(int id)
        {
            var resultView = new
            {
                Id = -1
            };
            return Partial("PartialView/EditNTD", resultView);
        }


    }
}
