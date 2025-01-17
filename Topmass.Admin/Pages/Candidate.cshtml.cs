using Microsoft.AspNetCore.Mvc;
using Topmass.Admin;
using Topmass.Admin.Business;
using Topmass.Admin.Pages.Model;
using Topmass.Admin.Pages.Model.search;

namespace crmHuman.Pages
{
    //[Authorize]
    public class CandidateModel : BaseModel
    {
        private readonly ILogger<CandidateModel> _logger;
        public List<string> TableColumnTextAdmin { get; set; }
        public CandidateInputRequest RequestSearch { get; set; }
        public BaseList DataAll { get; set; }
        public string TitlePage { get; set; }
        public string NameController { get; set; }
        public string KeyPage { get; set; }
        public List<string> TableColumnText { get; set; }
        private ICandidateAdminBusiness business { get; set; }
        private readonly INTDBusiness bussiessNTD;
        public List<Object> DataCompnay { get; set; }
        public int TotalRecord
        {

            get
            {
                return DataAll.Total;

            }
        }
        public CandidateModel(ILogger<CandidateModel> logger,
            ICandidateAdminBusiness _business,
            INTDBusiness _nTDBusiness

            )
        {
            _logger = logger;
            TitlePage = "Danh sách ứng viên";
            KeyPage = "Candidate";
            TableColumnText = new List<string>()
            {
                "STT","Mã","Họ", "Tên" ,"Email", "Số điện thoại" ,"Trạng thái mail","Ngày đăng ký", "Truy cập gần nhất", "Cập nhật gần nhất","Thao tác"
            };
            NameController = "Candidate";
            business = _business;
            bussiessNTD = _nTDBusiness;
            DataAll = new BaseList();
            DataCompnay = new List<Object>();
        }

        public async Task<ActionResult> OnGet([FromQuery] CandidateInputRequest request)
        {

            return await GetAll(request);
        }
        public async Task<ActionResult> GetAll(CandidateInputRequest request2)
        {
            RequestSearch = request2;
            var dataAll = await business.GetAll(new SearchCandidateAdminRequest()
            {
                From = request2.From,
                Limit = request2.Limit,
                Page = request2.Page,
                To = request2.To,
                OrderBy = request2.Orderby,
                Token = request2.Token,
                CbStatus = request2.CbStatus,
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
