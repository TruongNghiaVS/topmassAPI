using Microsoft.AspNetCore.Mvc;
using Topmass.Admin;
using Topmass.Admin.Business;
using Topmass.Admin.Pages.Model;
using Topmass.Admin.Pages.Model.search;

namespace crmHuman.Pages
{
    //[Authorize]

    public class NTDDetailModel : BaseModel
    {
        private readonly ILogger<NTDModel> _logger;
        public int IdRequest { get; set; }
        public List<string> TableColumnTextAdmin { get; set; }
        public NTDRequest RequestSearch { get; set; }
        public BaseList DataAll { get; set; }

        public dynamic DataLinhVuc { get; set; }

        public dynamic DataLogAccount { get; set; }
        public dynamic DataLog { get; set; }
        public string TitlePage { get; set; }

        public string NameController { get; set; }
        public string KeyPage { get; set; }
        public List<string> TableColumnText { get; set; }
        private INTDBusiness business { get; set; }
        private IMasterBusiness MasterBusiness { get; set; }
        public int TotalRecord
        {
            get
            {
                return DataAll.Total;

            }
        }
        public NTDDetailModel(ILogger<NTDModel> logger,
            INTDBusiness _business,
            IMasterBusiness masterBusiness

            )
        {
            _logger = logger;
            TitlePage = "Thông tin nhà tuyển dụng";
            KeyPage = "NTD";
            TableColumnText = new List<string>()
            {
                "STT","Mã","Tên người đại diện","Tên đăng nhập",
                "Tên công ty", "MST", "Số điện thoại", "Trạng thái",
                "Cấp độ xác thực","Cập nhật gần nhất","Thao tác"
            };
            NameController = "NTDDetail";
            business = _business;
            DataAll = new BaseList();
            DataLinhVuc = new List<object>();
            MasterBusiness = masterBusiness;
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
            var reusltData1 = await business.GetAllLog(orderId);
            DataLog = reusltData1;
            var reusltData2 = await business.GetALlLogAccount(orderId);
            DataLogAccount = reusltData2;
            if (orderId > 0)
            {
                var resultView = await business.GetDetail(orderId);
                ResultData = resultView;
            }
            return Page();
        }
        public async Task<ActionResult> GetAll(NTDRequest request2)
        {
            RequestSearch = request2;
            return Page();
        }

        public async Task<IActionResult> OnpostUpdateBusinessDocument(UpdatedocumentChangeRequest request)
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
            await business.AddOrUpdateDocumnet(new UpdateDocumnetRequest()
            {
                Id = request.Id,
                LinkFile = request.Linkfile,
                NotedChange = request.NotedChange,
                ReasonReject = request.ReasonReject.HasValue ? request.ReasonReject.Value : -1,
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
            await business.UpdateInfoHuman(request.StatusAccount,
                request.ConfirmAccout, request.Id,
                request.ReasonLock.HasValue ? request.ReasonLock.Value : -1,
                request.Noted);
            var dataReponse = new
            {
                success = true,
            };
            return new JsonResult(dataReponse)
            {
                StatusCode = StatusCodes.Status200OK
            };
        }

        public async Task<IActionResult> OnpostUpdatePersonalPerson(UpdatePersonalPerson request)
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
            await business.UpdatePersonalPerson(request.fullName, request.Gender, request.Phone, request.Id);
            var dataReponse = new
            {
                success = true,
            };
            return new JsonResult(dataReponse)
            {
                StatusCode = StatusCodes.Status200OK

            };
        }

        public async Task<IActionResult> OnpostResetPasswordAccount(int id)
        {
            var listEror = new List<object>();

            if (id < 1)
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
            await business.ResetPasswordAccount(id);
            return new JsonResult(true)
            {
                StatusCode = StatusCodes.Status200OK

            };
        }
        public async Task<IActionResult> OnpostSendMailActiveAccount(int id)
        {
            var listEror = new List<object>();

            if (id < 1)
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
            await business.ResetPasswordAccount(id);
            return new JsonResult(true)
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


        public async Task<IActionResult> OnpostUpdateCompanyInfo(UpdateCompanyInfoChangeRequest request)
        {

            await business.UpdateCompanyInfo(new UpdateCompanyRequestInfo()
            {
                CompanyName = request.CompanyName,
                DescriptionCompany = request.DescriptionCompany,
                Id = request.Id,
                Address = request.Address
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
    }
}
