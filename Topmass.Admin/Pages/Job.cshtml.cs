using Microsoft.AspNetCore.Mvc;
using Topmass.Admin;
using Topmass.Admin.Business;
using Topmass.Admin.Pages.Model;
using Topmass.Admin.Pages.Model.search;

namespace crmHuman.Pages
{
    //[Authorize]
    public class JobModel : BaseModel
    {
        private readonly ILogger<JobModel> _logger;
        public List<string> TableColumnTextAdmin { get; set; }
        public JobInputRequest RequestSearch { get; set; }
        public BaseList DataAll { get; set; }
        public string TitlePage { get; set; }
        public string NameController { get; set; }
        public string KeyPage { get; set; }
        public List<string> TableColumnText { get; set; }
        private IJobAdminBusiness business { get; set; }
        private readonly INTDBusiness bussiessNTD;

        public List<Object> DataCompnay { get; set; }
        public int TotalRecord
        {

            get
            {
                return DataAll.Total;

            }
        }
        public JobModel(ILogger<JobModel> logger,
            IJobAdminBusiness _business,
            INTDBusiness _nTDBusiness

            )
        {
            _logger = logger;
            TitlePage = "Danh sách tin đăng";
            KeyPage = "NTD";
            TableColumnText = new List<string>()
            {
                "STT","Mã","Tên tin","Trạng thái hiển thị","Ngày hết hạn", "Trạng thái tin",
                "Công ty", "Chiến dịch","Vị trí", "Cập nhật gần nhất","Thao tác"
            };
            NameController = "Job";
            business = _business;
            bussiessNTD = _nTDBusiness;
            DataAll = new BaseList();
            DataCompnay = new List<Object>();
        }
        public async Task<ActionResult> OnGet([FromQuery] JobInputRequest request)
        {

            return await GetAll(request);
        }
        public async Task<ActionResult> GetAll(JobInputRequest request2)
        {
            RequestSearch = request2;
            var dataAll = await business.GetAll(new SearchJobAdminRequest()
            {
                From = request2.From,
                Limit = request2.Limit,
                Page = request2.Page,
                To = request2.To,
                OrderBy = request2.Orderby,
                Token = request2.Token,
                CbDisplay = request2.CbDisplay,
                CbStatus = request2.CbStatus,
                CbCompany = request2.CbCompany,
                AuthenLevel = int.Parse(request2.AuthenLevel)
            });
            DataAll.Data = dataAll.Data;
            var dataCompa = await bussiessNTD.GetAllShortNTD();
            foreach (var item in dataCompa)
            {
                DataCompnay.Add(new ControlOptionDisplay()
                {
                    id = item.Id,
                    text = item.Text
                });
            }
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
