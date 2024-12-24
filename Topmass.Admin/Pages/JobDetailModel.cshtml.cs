using Microsoft.AspNetCore.Mvc;
using Topmass.Admin;
using Topmass.Admin.Business;
using Topmass.Admin.Pages.Model;
using Topmass.Admin.Pages.Model.search;

namespace crmHuman.Pages
{
    //[Authorize]

    public class JobDetailModel : BaseModel
    {
        private readonly ILogger<JobDetailModel> _logger;
        public int IdRequest { get; set; }
        public List<string> TableColumnTextAdmin { get; set; }
        public NTDRequest RequestSearch { get; set; }
        public BaseList DataAll { get; set; }

        public dynamic DataLinhVuc { get; set; }
        public dynamic DataTypeOfWork { get; set; }

        public dynamic DataLevelMaster { get; set; }
        public dynamic DataExperMaster { get; set; }
        public dynamic DataLog { get; set; }

        public string TitlePage { get; set; }

        public string NameController { get; set; }
        public string KeyPage { get; set; }

        public List<string> TableColumnText { get; set; }
        private INTDBusiness business { get; set; }
        private readonly IJobAdminBusiness jobAdminBusiness;
        private IMasterBusiness MasterBusiness { get; set; }

        public int TotalRecord
        {

            get
            {
                return DataAll.Total;

            }
        }
        public JobDetailModel(ILogger<JobDetailModel> logger,
            INTDBusiness _business,
            IJobAdminBusiness _jobAdminBusiness,
        IMasterBusiness masterBusiness

            )
        {
            _logger = logger;
            TitlePage = "Thông tin tin đăng";
            KeyPage = "jobdetail";
            TableColumnText = new List<string>()
            {
                "STT","Mã","Tên người đại diện","Tên đăng nhập",
                "Tên công ty", "MST", "Số điện thoại", "Trạng thái",
                "Cấp độ xác thực","Cập nhật gần nhất","Thao tác"
            };
            NameController = "Jobdetail";
            business = _business;
            DataAll = new BaseList();
            DataLinhVuc = new List<object>();
            DataTypeOfWork = new List<object>();
            DataLevelMaster = new List<object>();
            DataExperMaster = new List<object>();
            MasterBusiness = masterBusiness;

            jobAdminBusiness = _jobAdminBusiness;
        }
        public async Task<ActionResult> OnGet(int id)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return Redirect("/Login");
            }
            IdRequest = id;
            return await GetDetail(id);
        }
        public dynamic ResultData;
        public async Task<ActionResult> GetDetail(int orderId)
        {
            var dataLinhvuc = await MasterBusiness.GetAllDataByType(2);
            DataLinhVuc = dataLinhvuc;
            var dataTypeOfWork = await MasterBusiness.GetAllDataByType(3);
            DataTypeOfWork = dataTypeOfWork;
            var dataLevelMaster = await MasterBusiness.GetAllDataByType(4);
            DataLevelMaster = dataLevelMaster;
            var dataExperMaster = await MasterBusiness.GetAllDataByType(5);
            DataExperMaster = dataExperMaster;
            //var data = await MasterBusiness.GetAllDataByType(2);
            //DataLinhVuc = dataLinhvuc;
            var reusltData1 = await jobAdminBusiness.GetAllLog(orderId);
            DataLog = reusltData1;
            if (orderId > 0)
            {
                var resultView = await jobAdminBusiness.GetDetail(orderId);
                ResultData = resultView;
            }
            return Page();
        }
        public async Task<ActionResult> GetAll(NTDRequest request2)
        {
            RequestSearch = request2;


            return Page();
        }

        public async Task<IActionResult> OnpostConfirmStatusJob(UpdateConfirmStatusJobRequest request)
        {
            var listEror = new List<object>();

            if (request == null)
            {
                listEror.Add("thiếu thông tin");
            }

            if (listEror.Count > 0)
            {
                return new JsonResult(listEror)
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
            await jobAdminBusiness.UpdateConfirmStatus(new UpdateJobAdmin()
            {
                Id = request.Id,
                NotedChange = request.NotedChange,
                StatusChange = request.StatusChange
            });
            var dataReponse = new
            {
                success = true,
            };
            return new JsonResult(dataReponse)
            {
                StatusCode = StatusCodes.Status200OK

            };
        }


        public async Task<IActionResult> OnpostUpdateDisplayJob(UpdateConfirmStatusJobRequest request)
        {
            var listEror = new List<object>();

            if (request == null)
            {
                listEror.Add("thiếu thông tin");
            }

            if (listEror.Count > 0)
            {
                return new JsonResult(listEror)
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
            await jobAdminBusiness.UpdateStatusDisplay(request.Id, request.StatusChange, request.NotedChange, "");
            var dataReponse = new
            {
                success = true,
            };
            return new JsonResult(dataReponse)
            {
                StatusCode = StatusCodes.Status200OK

            };
        }


        public async Task<IActionResult> OnpostUpdateInfoHuman(UpdateInfoHuman request)
        {
            var listEror = new List<object>();

            if (request == null)
            {
                listEror.Add("thiếu thông tin");
            }

            if (listEror.Count > 0)
            {
                return new JsonResult(listEror)
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
            await business.UpdateInfoHuman(request.StatusAccount, request.ConfirmAccout, request.Id);
            var dataReponse = new
            {
                success = true,
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
