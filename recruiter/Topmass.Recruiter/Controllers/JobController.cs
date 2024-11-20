using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Topmass.Campagn.Business;
using Topmass.Job.Business;
using Topmass.Job.Business.Model;
using Topmass.Recruiter.Model;
using TopMass.Core.Result;

namespace Topmass.Recruiter.Controllers
{
    [ApiController]
    [Authorize]
    public class JobController : BaseController
    {
        private readonly ILogger<JobController> _logger;
        private readonly ICampagnBusiness _bussiness;
        private readonly IJobBusiness _jobBusiness;
        public JobController(ILogger<JobController> logger,
            ICampagnBusiness articleBusiness,
            IJobBusiness jobBusiness
            ) : base(logger)
        {
            _logger = logger;
            _bussiness = articleBusiness;
            _jobBusiness = jobBusiness;
        }

        [HttpPost]
        public async Task<ActionResult> Add(JobItemRequestAdd request)
        {
            var resultUser = await GetCurrentUser();
            var reponse = new BaseResult();

            if (string.IsNullOrEmpty(request.Name))
            {
                reponse.AddError(nameof(request.Name), "Thiếu thông tên");
            }
            if (!reponse.Success)
            {
                return StatusCode(reponse.StatusCode, reponse);
            }
            var requestAdd = new JobItemBusinessAdd()
            {
                HandleBy = int.Parse(resultUser.Id),
                Aggrement = request.Aggrement,
                Benefit = request.Benefit,
                Campaign = request.Campaign,
                Description = request.Description,
                Experience = request.Experience,
                Gender = request.Gender,
                Name = request.Name,
                Phone = request.Phone,
                Quantity = request.Quantity,
                Position = request.Position,
                Rank = request.Rank,
                Requirement = request.Requirement,
                Salary_from = request.Salary_from,
                Salary_to = request.Salary_to,
                Profession = request.Profession,
                Type_money = request.Type_money,
                Username = request.Username,
                Type_of_work = request.Type_of_work,
                Expired_date = request.Expired_date,
                Emails = request.Emails,
                Skills = request.Skills,
                Locations = request.Locations,
                Time_working = request.Time_working

            };
            var result = await _jobBusiness.AddJob(requestAdd);
            return StatusCode(result.StatusCode, result);
        }


        [HttpPost]
        public async Task<ActionResult> UpdateJob(JobItemRequestUpdateInfo request)
        {
            var resultUser = await GetCurrentUser();
            var reponse = new BaseResult();


            if (request.JobId < 1)
            {
                reponse.AddError(nameof(request.JobId), "Thiếu thông tin định danh");
            }
            if (string.IsNullOrEmpty(request.Name))
            {
                reponse.AddError(nameof(request.Name), "Thiếu thông tên");
            }
            if (!reponse.Success)
            {
                return StatusCode(reponse.StatusCode, reponse);
            }


            var requestAdd = new JobItemBusinessUpdate()
            {
                JobId = request.JobId,
                HandleBy = int.Parse(resultUser.Id),
                Aggrement = request.Aggrement,
                Benefit = request.Benefit,
                Description = request.Description,
                Experience = request.Experience,
                Gender = request.Gender,
                Name = request.Name,
                Phone = request.Phone,
                Quantity = request.Quantity,
                Position = request.Position,
                Rank = request.Rank,
                Requirement = request.Requirement,
                Salary_from = request.Salary_from,
                Salary_to = request.Salary_to,
                Profession = request.Profession,
                Type_money = request.Type_money,
                Username = request.Username,


                Skills = request.Skills,
                Type_of_work = request.Type_of_work,
                Expired_date = request.Expired_date,
                Emails = request.Emails,
                Locations = request.Locations,
                Time_working = request.Time_working

            };
            var result = await _jobBusiness.UpdateJob(requestAdd);
            return StatusCode(result.StatusCode, result);
        }
        [HttpPost]
        public async Task<ActionResult> ChangeStatus(JobItemStatusRequestUpdate request)
        {
            var resultUser = await GetCurrentUser();
            var reponse = new BaseResult();

            if (request.IdUpdate < 1)
            {
                reponse.AddError(nameof(request.IdUpdate), "Thiếu thông đối tượng ");
            }
            if (request.Status < 0)
            {
                reponse.AddError(nameof(request.Status), "Thiếu thông tin trạng thái");
            }
            if (!reponse.Success)
            {
                return StatusCode(reponse.StatusCode, reponse);
            }
            var requestAdd = new JobItemStatusUpdate()
            {
                IdUpdate = request.IdUpdate,
                Status = request.Status
            };
            var result = await _jobBusiness.ChangeStatus(requestAdd);
            return StatusCode(result.StatusCode, result);
        }
        //[HttpPost]
        //public async Task<ActionResult> Update(JobItemRequestUpdate request)
        //{
        //    var resultUser = await GetCurrentUser();
        //    var reponse = new BaseResult();

        //    if (request.IdUpdate < 1)
        //    {
        //        reponse.AddError(nameof(request.IdUpdate), "Thiếu thông đối tượng ");
        //    }
        //    if (string.IsNullOrEmpty(request.Name))
        //    {
        //        reponse.AddError(nameof(request.Name), "Thiếu thông tên");
        //    }
        //    if (!reponse.Success)
        //    {
        //        return StatusCode(reponse.StatusCode, reponse);
        //    }
        //    var requestAdd = new JobItemUpdate()
        //    {
        //        From = DateTime.Now,
        //        To = DateTime.Now,
        //        JobId = request.IdUpdate.Value,
        //        Name = request.Name,

        //        HandleBy = int.Parse(resultUser.Id)
        //    };
        //    var result = await _jobBusiness.UpdateJob(requestAdd);
        //    return StatusCode(result.StatusCode, result);
        //}

        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery] JobSearchRequestFilter request)
        {
            var resultUser = await GetCurrentUser();
            var reponse = new BaseResult();
            var requestAdd = new JobSearchRequest()
            {
                From = request.From,
                To = request.To,
                Status = request.Status,
                Email = resultUser.UserName,
                Limit = 10,
                Page = 1,
                CampagnId = request.CampaigId,
                Userid = int.Parse(resultUser.Id)
            };
            var result = await _jobBusiness.GetallJob(requestAdd);
            return StatusCode(result.StatusCode, result);
        }


        [HttpGet]
        public async Task<ActionResult> GetInfo([FromQuery] JobItemInfoRequest request)
        {
            var resultUser = await GetCurrentUser();
            var reponse = new BaseResult();
            if (request.JobId < 1)
            {
                reponse.AddError(nameof(request.JobId), "Thiếu thông đối tượng ");
                return StatusCode(reponse.StatusCode, reponse);
            }
            var requestAdd = new JobInfoRequest()
            {
                JobId = request.JobId.HasValue ? request.JobId.Value : -1,
                HandleBy = int.Parse(resultUser.Id)
            };
            var result = await _jobBusiness.GetInfo(requestAdd);
            reponse.Data = result;
            return StatusCode(reponse.StatusCode, reponse);
        }


        [HttpGet]
        public async Task<ActionResult> GetInfoForEdit([FromQuery] JobItemInfoRequest request)
        {
            var resultUser = await GetCurrentUser();
            var reponse = new BaseResult();
            if (request.JobId < 1)
            {
                reponse.AddError(nameof(request.JobId), "Thiếu thông đối tượng ");
                return StatusCode(reponse.StatusCode, reponse);
            }
            var requestAdd = new JobInfoRequest()
            {
                JobId = request.JobId.HasValue ? request.JobId.Value : -1,
                HandleBy = int.Parse(resultUser.Id)
            };
            var result = await _jobBusiness.GetInfoForEdit(requestAdd);
            reponse.Data = result;
            return StatusCode(reponse.StatusCode, reponse);
        }
        [HttpGet]
        public async Task<ActionResult> GetAllViewerOfJob([FromQuery] InputGetAllCVApplyOfJob request)
        {
            var resultUser = await GetCurrentUser();
            var reponse = new BaseResult();
            var requestAdd = new GetAllVierOfJobRequest()
            {
                HandleId = int.Parse(resultUser.Id),
                Page = 1,
                Limit = 10,
                CampagnId = -1,
                JobId = request.JobId
            };
            var result = await _jobBusiness.GetAllViewerOfJob(requestAdd);

            return StatusCode(result.StatusCode, result);
        }


        [HttpGet]
        public async Task<ActionResult> GetOverViewInfoMation([FromQuery] InputGetOverViewInfoMation request)
        {
            var resultUser = await GetCurrentUser();
            var reponse = new BaseResult();
            if (request.JobId < 1)
            {
                reponse.AddError(nameof(request.JobId), "Thiếu thông tin Id");
                return StatusCode(reponse.StatusCode, reponse);
            }
            var requestAdd = new GetOverViewByJobId()
            {
                JobId = request.JobId,
                From = request.From,
                To = request.To

            };
            var result = await _jobBusiness.GetOverviewJob(requestAdd);

            var dataReponse = new
            {
                result.JobId,
                result.JobName,
                result.Rate,
                result.StatusCode,
                result.StatusText,
                result.ServiceName,
                result.TotalApply,
                result.TotalViewer
            };

            return StatusCode(reponse.StatusCode, dataReponse);
        }

        [HttpGet]
        public async Task<ActionResult> GetDataChartCountViewByJob([FromQuery] InputGetOverViewInfoMation request)
        {
            var resultUser = await GetCurrentUser();
            var reponse = new BaseResult();
            if (request.JobId < 1)
            {
                reponse.AddError(nameof(request.JobId), "Thiếu thông tin Id");
                return StatusCode(reponse.StatusCode, reponse);
            }
            var requestAdd = new GetOverViewByJobId()
            {
                JobId = request.JobId,
                From = request.From,
                To = request.To

            };
            var result = await _jobBusiness.GetOverviewJob(requestAdd);
            var reponseData = new
            {
                result.JobId,
                DataDraw = result.Data
            };
            return StatusCode(reponse.StatusCode, reponseData);
        }



    }
}
