using System.Data;
using System.Text.Json;
using Topmass.Core.Model;
using Topmass.Core.Model.Campagn;
using Topmass.Core.Model.location;
using Topmass.Core.Repository;
using Topmass.Core.Repository.IndexModel;
using Topmass.Job.Business.Model;
using TopMass.Core.Result;

namespace Topmass.Job.Business
{
    public class JobItemBusiness : IJobBusiness
    {

        private readonly IRegionalRepository _regionalRepository;
        private readonly IJobRepository _jobRepository;
        private readonly IJobInfoRepository _jobInfoRepository;
        private readonly IJobLogViewRepository _jobLogViewRepository;
        private readonly IJobOverViewCounterRepository _jobOverViewCounterRepository;
        private readonly IJobDisplayItemRepository _jobDisplayItemRepository;
        private readonly IMasterDataRepository _masterDataRepository;
        private readonly ICompanyInfoRepository _companyInfoRepository;
        private readonly ICampagnRepository _campagnRepository;
        private List<MasterDataModel> _masterData { get; set; }
        private List<RegionalModel> _dataRegionals { get; set; }
        public JobItemBusiness(

            IJobRepository jobRepository,
            IJobLogViewRepository jobLogViewRepository,
            IJobOverViewCounterRepository jobOverViewCounterRepository,
            IJobInfoRepository jobInfoRepository,
            IJobDisplayItemRepository jobDisplayItemRepository,
            IRegionalRepository regionalRepository,
            IMasterDataRepository masterDataRepository,
            ICompanyInfoRepository companyInfoRepository,
            ICampagnRepository campagnRepository
            )
        {
            _regionalRepository = regionalRepository;
            _jobRepository = jobRepository;
            _jobLogViewRepository = jobLogViewRepository;
            _jobOverViewCounterRepository = jobOverViewCounterRepository;
            _jobInfoRepository = jobInfoRepository;
            _jobDisplayItemRepository = jobDisplayItemRepository;
            _masterDataRepository = masterDataRepository;
            _companyInfoRepository = companyInfoRepository;
            _campagnRepository = campagnRepository;
        }
        public async Task<JobItemReponse> AddJob(JobItemBusinessAdd itemAdd)
        {
            var reponse = new JobItemReponse();
            var campagnId = itemAdd.Campaign;
            if (!campagnId.HasValue)
            {
                reponse.Message = "Không có thông tin chiến dịch";
                return reponse;
            }
            if (!itemAdd.Expired_date.HasValue)
            {
                reponse.Message = "Không có thông tin thời hạn đăng";
                return reponse;
            }
            if (itemAdd.Expired_date.Value.AddDays(1).Date > DateTime.Now.AddDays(1).Date.AddMonths(1))
            {
                reponse.Message = "Thời hạn tin đăng, nhỏ hơn 1 tháng kể từ thời điểm tạo tin";
                return reponse;
            }
            var companyInfo = await _jobDisplayItemRepository.FindOneByStatementSql<JobIdCount>("select * from Recruiter d inner join CompanyInfo e on  d.id = e.RelId where d.id = @relid  ",
             new { relid = itemAdd.HandleBy });
            if (companyInfo == null || companyInfo.Id < 1)
            {
                reponse.Message = "Hoàn tất cập nhật thông tin công ty, trước khi tạo tin đăng";
                return reponse;
            }
            //var jobApprove = await _jobDisplayItemRepository.ExecuteSqlProcerduceToList<JobIdCount>("getAllCountJob",
            // new { userId = itemAdd.HandleBy });
            //if (jobApprove != null && jobApprove.Count >= 5)
            //{
            //    reponse.Message = "Tài khoản của bạn chỉ được hiển thị 5 tin đăng, vui lòng nâng cấp để đăng nhiều tin hơn";
            //    return reponse;
            //}

            var jobCount = await _jobDisplayItemRepository.FindOneByStatementSql<JobIdCount>
                ("select id as Id from jobItems where Campagn = @campagnId and status != 5  ",
               new { campagnId = campagnId });
            if (jobCount != null && jobCount.Id > 0)
            {
                reponse.Message = "Mỗi chiến dịch chỉ có một tin đăng, Vui lòng tạo chiến dịch khác, quay lại sau";
                return reponse;
            }
            var JobSlug = await _jobRepository.CreateSlugJob(itemAdd.HandleBy, itemAdd.Name);
            var jobItemBySlug = await _jobDisplayItemRepository.FindOneByStatementSql<JobIdCount>
                ("SELECT id  FROM jobItems WHERE Slug = @slug  ",
            new { slug = JobSlug }
            );
            if (jobItemBySlug != null && jobItemBySlug.Id > 0)
            {
                reponse.Message = "Vui lòng chọn tiêu đề khác, tiêu đề hiện tại đã tồn tại";
                return reponse;
            }
            var jobitem = new JobItemModel()
            {
                Campagn = campagnId,
                CreateAt = DateTime.Now,
                CreatedBy = itemAdd.HandleBy,
                Name = itemAdd.Name,
                RelId = itemAdd.HandleBy,
                Deleted = false,
                Package = 0,
                UpdateAt = DateTime.Now,
                Status = 0,
                Slug = JobSlug,
                UpdatedBy = itemAdd.HandleBy
            };
            var idjob = await _jobRepository.AddAndGetId(jobitem);
            if (idjob < 0)
            {
                return reponse;
            }
            await AddJobInfo(itemAdd, idjob);
            await UpdateJobDiplayAdd(itemAdd, idjob, JobSlug);
            return reponse;
        }
        public async Task<JobItemReponse> UpdateJob(JobItemBusinessUpdate itemAdd)
        {
            var reponse = new JobItemReponse();
            var jobUpdate = await _jobRepository.GetById(itemAdd.JobId);
            if (jobUpdate == null)
            {
                reponse.Message = "Không có thông tin trong hệ thống";
            }
            if (!itemAdd.Expired_date.HasValue)
            {
                reponse.Message = "Không có thông tin thời hạn đăng";
                return reponse;
            }
            if (itemAdd.Expired_date.Value.AddDays(1).Date > DateTime.Now.AddDays(1).Date.AddMonths(1))
            {
                reponse.Message = "Thời hạn tin đăng, nhỏ hơn 1 tháng kể từ thời điểm tạo tin";
                return reponse;
            }
            var JobSlug = await _jobRepository.CreateSlugJob(itemAdd.HandleBy, itemAdd.Name);
            var jobItemBySlug = await _jobDisplayItemRepository.FindOneByStatementSql<JobIdCount>
                ("SELECT id  FROM jobItems WHERE Slug = @slug  and  id != @JobId  ",
            new { slug = JobSlug, JobId = itemAdd.JobId }
            );
            if (jobItemBySlug != null && jobItemBySlug.Id > 0)
            {
                reponse.Message = "Vui lòng chọn tiêu đề khác, tiêu đề hiện tại đã tồn tại";
                return reponse;
            }
            jobUpdate.Name = itemAdd.Name;
            jobUpdate.Slug = JobSlug;
            jobUpdate.UpdateAt = DateTime.Now;
            jobUpdate.UpdatedBy = itemAdd.HandleBy;
            await _jobRepository.AddOrUPdate(jobUpdate);
            var jobInfo = await _jobInfoRepository.FindOneByStatementSql<JobInfoModel>("select * from jobInfo where jobId =@jobId ",
                new
                {
                    itemAdd.JobId
                }
                );
            if (jobInfo == null)
            {
                jobInfo = new JobInfoModel()
                {
                    JobId = jobUpdate.Id
                };
            }
            jobInfo.JobId = itemAdd.JobId;
            jobInfo.Name = itemAdd.Name;
            jobInfo.Aggrement = itemAdd.Aggrement;
            jobInfo.Benefit = itemAdd.Benefit;
            jobInfo.CreateAt = DateTime.Now;
            jobInfo.CreatedBy = itemAdd.HandleBy;
            jobInfo.Gender = itemAdd.Gender;
            jobInfo.Deleted = false;
            jobInfo.Description = itemAdd.Description;
            jobInfo.Experience = itemAdd.Experience;
            jobInfo.Expired_date = itemAdd.Expired_date;
            jobInfo.Position = itemAdd.Position;
            jobInfo.Profession = itemAdd.Profession;
            jobInfo.Quantity = itemAdd.Quantity;
            jobInfo.Rank = itemAdd.Rank;
            jobInfo.Requirement = itemAdd.Requirement;
            jobInfo.Salary_from = itemAdd.Salary_from;
            jobInfo.Salary_to = itemAdd.Salary_to;
            jobInfo.Type_money = itemAdd.Type_money;
            jobInfo.Type_of_work = itemAdd.Type_of_work;
            jobInfo.Skill = JsonSerializer.Serialize(itemAdd.Skills);
            jobInfo.UpdateAt = DateTime.Now;
            jobInfo.fullName = itemAdd.Username;
            jobInfo.Phone = itemAdd.Phone;
            jobInfo.Emails = JsonSerializer.Serialize(itemAdd.Emails);
            jobInfo.UpdatedBy = itemAdd.HandleBy;
            jobInfo.Locations = JsonSerializer.Serialize(itemAdd.Locations);
            jobInfo.Time_workings = JsonSerializer.Serialize(itemAdd.Time_working);
            jobInfo.Time_WorkingText = itemAdd.Time_WorkingText;
            await _jobInfoRepository.AddOrUPdate(jobInfo);
            await UpdateJobDiplay(itemAdd, JobSlug);
            return reponse;
        }
        public async Task<JobItemReponse> UpdateJobDiplayAdd(JobItemBusinessAdd itemAdd, int idjob, string JobSlug)
        {
            if (_masterData == null)
            {
                _masterData = await _masterDataRepository.GetAllToList();
            }
            if (_dataRegionals == null)
            {
                _dataRegionals = await _regionalRepository.ExecuteSqlProcerduceToList<RegionalModel>("select id, slug, code, Name from regionals", new { }, System.Data.CommandType.Text);
            }
            var reponse = new JobItemReponse();

            var jobInfo = new JobDisplayItemModel()
            {
                JobId = idjob
            };
            jobInfo.CreateAt = DateTime.Now;
            jobInfo.CreatedBy = itemAdd.HandleBy;
            jobInfo.UpdateAt = DateTime.Now;
            jobInfo.UpdatedBy = itemAdd.HandleBy;
            jobInfo.RecurId = itemAdd.HandleBy;
            jobInfo.JobName = itemAdd.Name;
            jobInfo.CompanyName = "";
            if (!itemAdd.Salary_from.HasValue)
            {
                jobInfo.SalaryFrom = 0;
            }
            if (!itemAdd.Salary_to.HasValue)
            {
                jobInfo.SalaryTo = 0;
            }
            jobInfo.SalaryFrom = itemAdd.Salary_from.Value;
            jobInfo.SalaryTo = itemAdd.Salary_to.Value;
            if (itemAdd.Aggrement == true)
            {
                jobInfo.RangeSalary = "Thoả thuận";
            }
            else
            {
                var tempSalaryText = "";
                if (itemAdd.Salary_from > 0)
                {
                    tempSalaryText += "Từ " + itemAdd.Salary_from;
                }
                if (itemAdd.Salary_to > 0)
                {

                    if (string.IsNullOrEmpty(tempSalaryText))
                    {
                        tempSalaryText += "Đến " + itemAdd.Salary_from;
                    }
                    else
                    {
                        tempSalaryText += " _ " + itemAdd.Salary_from;
                    }


                }
                jobInfo.RangeSalary = tempSalaryText;
            }
            jobInfo.Deleted = false;
            jobInfo.Hashtags = "";
            jobInfo.Slug = JobSlug;
            string professionTemp = "";
            string typeOfWorkText = "";
            if (itemAdd.Profession > 0)
            {
                professionTemp = _masterData.Where(x => x.Id == itemAdd.Profession.Value).FirstOrDefault().Text;
            }
            var listStringLocationSearch = new List<string>();
            var listStringLocation = new List<string>();
            var isNational = false;

            foreach (var item in itemAdd.Locations)
            {
                if (item.Location == "-1")
                {
                    isNational = true;
                    break;
                }
                var provice = _dataRegionals.Where(x => x.Code == item.Location)
                                            .FirstOrDefault();
                if (provice == null)
                {
                    continue;
                }
                listStringLocationSearch.Add(provice.Slug);
                listStringLocation.Add(provice.Name);

            }
            if (isNational == false)
            {
                var textLocationSearch = string.Join<string>(";", listStringLocationSearch);
                jobInfo.LocationSearch = textLocationSearch;
                jobInfo.LocationText = string.Join<string>(";", listStringLocation);

            }
            else
            {
                jobInfo.LocationSearch = "-1";
                jobInfo.LocationText = "-1";

            }

            jobInfo.ProfessionText = professionTemp;
            if (itemAdd.Type_of_work > 0)
            {
                typeOfWorkText = _masterData.Where(x => x.Id == itemAdd.Type_of_work.Value).FirstOrDefault().Text;
            }
            jobInfo.TypeOfWork = typeOfWorkText;
            string typeRankText = "";
            if (!itemAdd.Rank.HasValue)
            {
                itemAdd.Rank = -1;
            }
            if (itemAdd.Rank > 0)
            {
                typeRankText = _masterData.Where(x => x.Id == itemAdd.Rank.Value).FirstOrDefault().Text;


            }
            jobInfo.Rank = typeRankText;
            jobInfo.RankSearch = itemAdd.Rank.Value;
            jobInfo.DateExpried = itemAdd.Expired_date;
            string exprenceText = "";

            if (itemAdd.Profession > 0)
            {
                professionTemp = _masterData.Where(x => x.Id == itemAdd.Profession.Value).FirstOrDefault().Text;
            }

            if (itemAdd.Experience > 0)
            {
                exprenceText = _masterData.Where(x => x.Id == itemAdd.Experience.Value).FirstOrDefault().Text;
            }
            jobInfo.ExperienceText = exprenceText;
            jobInfo.ProfessionText = professionTemp;

            await _jobDisplayItemRepository.AddOrUPdate(jobInfo);
            return reponse;
        }
        public async Task<JobItemReponse> UpdateJobDiplay(JobItemBusinessUpdate itemAdd, string JobSlug)
        {
            if (_masterData == null)
            {
                _masterData = await _masterDataRepository.GetAllToList();
            }
            if (_dataRegionals == null)
            {
                _dataRegionals = await _regionalRepository.ExecuteSqlProcerduceToList<RegionalModel>("select id, slug, code, Name from regionals", new { }, System.Data.CommandType.Text);
            }
            var reponse = new JobItemReponse();

            var jobUpdate = await _jobRepository.GetById(itemAdd.JobId);

            jobUpdate.Name = itemAdd.Name;
            jobUpdate.UpdateAt = DateTime.Now;
            jobUpdate.UpdatedBy = itemAdd.HandleBy;
            await _jobRepository.AddOrUPdate(jobUpdate);
            var jobInfo = await _jobInfoRepository.FindOneByStatementSql<JobDisplayItemModel>("select * from jobItemDisplay where JobId =@jobId ",
                new
                {
                    itemAdd.JobId
                }
                );
            if (jobInfo == null)
            {
                jobInfo = new JobDisplayItemModel()
                {
                    JobId = jobUpdate.Id
                };
                jobInfo.JobId = itemAdd.JobId;
                jobInfo.CreateAt = DateTime.Now;
                jobInfo.CreatedBy = itemAdd.HandleBy;
                //reponse.AddError(nameof(itemAdd.JobId), "Cập nhật thông tin thất bại");
                //return reponse;
            }
            else
            {
                jobInfo.UpdateAt = DateTime.Now;
                jobInfo.UpdatedBy = itemAdd.HandleBy;
            }

            jobInfo.RecurId = itemAdd.HandleBy;
            jobInfo.JobName = itemAdd.Name;
            jobInfo.CompanyName = "";

            if (!itemAdd.Salary_from.HasValue)
            {
                jobInfo.SalaryFrom = 0;
            }
            if (!itemAdd.Salary_to.HasValue)
            {
                jobInfo.SalaryTo = 0;
            }
            jobInfo.SalaryFrom = itemAdd.Salary_from.Value;
            jobInfo.SalaryTo = itemAdd.Salary_to.Value;


            if (itemAdd.Aggrement == true)
            {
                jobInfo.RangeSalary = "Thoả thuận";
            }
            else
            {
                var tempSalaryText = "";
                if (itemAdd.Salary_from > 0)
                {
                    tempSalaryText += "Từ " + itemAdd.Salary_from;
                }
                if (itemAdd.Salary_to > 0)
                {

                    if (string.IsNullOrEmpty(tempSalaryText))
                    {
                        tempSalaryText += "Đến " + itemAdd.Salary_from;
                    }
                    else
                    {
                        tempSalaryText += " _ " + itemAdd.Salary_from;
                    }


                }
                jobInfo.RangeSalary = tempSalaryText;
            }
            jobInfo.Deleted = false;

            jobInfo.Slug = JobSlug;

            string professionTemp = "";
            string typeOfWorkText = "";
            string exprenceText = "";

            if (itemAdd.Profession > 0)
            {
                professionTemp = _masterData.Where(x => x.Id == itemAdd.Profession.Value).FirstOrDefault().Text;
            }

            if (itemAdd.Experience > 0)
            {
                exprenceText = _masterData.Where(x => x.Id == itemAdd.Experience.Value).FirstOrDefault().Text;
            }


            var listStringLocationSearch = new List<string>();
            var listStringLocation = new List<string>();
            var isNational = false;
            foreach (var item in itemAdd.Locations)
            {
                if (item.Location == "-1")
                {
                    isNational = true;
                }
                var provice = _dataRegionals.Where(x => x.Code == item.Location)
                                            .FirstOrDefault();
                if (provice == null)
                {
                    continue;
                }
                listStringLocationSearch.Add(provice.Slug);
                listStringLocation.Add(provice.Name);

            }

            if (isNational == false)
            {
                var textLocationSearch = string.Join<string>(";", listStringLocationSearch);
                jobInfo.LocationSearch = textLocationSearch;
                jobInfo.LocationText = string.Join<string>(";", listStringLocation);

            }
            else
            {
                jobInfo.LocationSearch = "-1";
                jobInfo.LocationText = "-1";

            }

            jobInfo.ProfessionText = professionTemp;
            jobInfo.TypeOfWork = typeOfWorkText;

            string experienceTemp = "";

            if (itemAdd.Type_of_work > 0)
            {
                jobInfo.TypeOfWork = _masterData.Where(x => x.Id == itemAdd.Type_of_work.Value).FirstOrDefault().Text;
            }
            if (itemAdd.Experience > 0)
            {
                experienceTemp = _masterData.Where(x => x.Id == itemAdd.Experience.Value).FirstOrDefault().Text;
            }
            jobInfo.ExperienceText = experienceTemp;
            string typeRankText = "";
            if (!itemAdd.Rank.HasValue)
            {
                itemAdd.Rank = -1;
            }
            if (itemAdd.Rank > 0)
            {
                typeRankText = _masterData.Where(x => x.Id == itemAdd.Rank.Value).FirstOrDefault().Text;
            }
            jobInfo.Rank = typeRankText;
            jobInfo.RankSearch = itemAdd.Rank.Value;
            jobInfo.DateExpried = itemAdd.Expired_date;
            await _jobDisplayItemRepository.AddOrUPdate(jobInfo);
            return reponse;
        }
        private async Task<bool> AddJobInfo(JobItemBusinessAdd itemAdd, int jobId)
        {
            var reponse = new JobItemReponse();

            var campagnId = itemAdd.Campaign;


            if (jobId < 1)
            {
                return false;
            }


            if (!campagnId.HasValue)
            {
                return false;
            }

            var jobitem = new JobInfoModel()
            {
                JobId = jobId,
                Name = itemAdd.Name,
                Aggrement = itemAdd.Aggrement,
                Benefit = itemAdd.Benefit,
                CapagnId = itemAdd.Campaign,
                CreateAt = DateTime.Now,
                CreatedBy = itemAdd.HandleBy,
                Gender = itemAdd.Gender,
                Deleted = false,
                Description = itemAdd.Description,
                Experience = itemAdd.Experience,
                Expired_date = itemAdd.Expired_date,
                Position = itemAdd.Position,
                Profession = itemAdd.Profession,
                Quantity = itemAdd.Quantity,
                Rank = itemAdd.Rank,
                Requirement = itemAdd.Requirement,
                Salary_from = itemAdd.Salary_from,
                Salary_to = itemAdd.Salary_to,
                Type_money = itemAdd.Type_money,
                Type_of_work = itemAdd.Type_of_work,
                Skill = JsonSerializer.Serialize(itemAdd.Skills),
                Status = 0,
                UpdateAt = DateTime.Now,
                fullName = itemAdd.Username,
                Phone = itemAdd.Phone,
                Emails = JsonSerializer.Serialize(itemAdd.Emails),
                UpdatedBy = itemAdd.HandleBy,
                Locations = JsonSerializer.Serialize(itemAdd.Locations),
                Time_workings = JsonSerializer.Serialize(itemAdd.Time_working),
                Time_WorkingText = itemAdd.Time_WorkingText


            };

            var ressultAddInfo = await _jobInfoRepository.AddOrUPdate(jobitem);
            return ressultAddInfo;
        }
        public async Task<BaseResult> ChangeStatus(JobItemStatusUpdate itemAdd)
        {
            var reponse = new BaseResult();
            if (itemAdd.IdUpdate < 1)
            {
                reponse.AddError(nameof(itemAdd.IdUpdate), "Không có thông tin đối tượng");
            }



            if (itemAdd.Status.HasValue && itemAdd.Status.Value == 1)
            {
                var jobApprove = await _jobRepository
                                        .ExecuteSqlProcerduceToList<JobIdCount>("getAllActiveCamapang",
                                         new { createby = itemAdd.HandleBy });
                if (jobApprove != null && jobApprove.Count >= 5)
                {
                    reponse.Message = "Tài khoản của quý khách chỉ hiển thị cùng lúc 5 tin tuyển dụng, vui lòng kiểm tra lại";
                    return reponse;
                }
            }

            var jobInfo = await _jobRepository.GetById(itemAdd.IdUpdate.Value);
            if (jobInfo == null)
            {
                reponse.Message = "Không tồn tại tin đăng";
                return reponse;
            }

            jobInfo.Status = itemAdd.Status.Value;
            jobInfo.UpdateAt = DateTime.Now;
            jobInfo.UpdatedBy = itemAdd.HandleBy;

            await _jobRepository.AddOrUPdate(jobInfo);

            await _jobRepository.ExecuteSqlProcedure("updateChangeStatusCampaignDetail", new
            {
                jobId = itemAdd.IdUpdate,

                status = itemAdd.Status.Value
            });


            return reponse;

        }
        public async Task<BaseResult> GetallJob(JobSearchRequest itemAdd)
        {
            var reponse = new BaseResult();
            var searchRequst = new SearchRepJobRequest()
            {
                CampagnId = itemAdd.CampagnId.HasValue == true ? itemAdd.CampagnId.Value : -1,
                Limit = itemAdd.Limit,
                Page = itemAdd.Page,
                UserId = itemAdd.Userid.Value

            };

            if (itemAdd.Status.HasValue)
            {
                searchRequst.Status = itemAdd.Status;
            }
            var dataJOb = await _jobRepository.SearchAll(searchRequst);

            reponse.Data = dataJOb;
            return reponse;
        }


        public async Task<BaseResult> GetAllViewerOfJob
            (GetAllVierOfJobRequest itemAdd)
        {
            var reponse = new BaseResult();
            var dataViews = await _jobLogViewRepository.GetAll(new SearchRepJobLogView()
            {
                CampagnId = itemAdd.CampagnId,
                JobId = itemAdd.JobId,

                ViewMode = itemAdd.ViewMode,
                Limit = itemAdd.Limit,
                Status = itemAdd.Status,
                Page = itemAdd.Page,
                UserId = itemAdd.HandleId
            });
            reponse.Data = dataViews;
            return reponse;
        }
        public async Task<BaseResult> GetInfo(JobInfoRequest jobInfo)
        {
            var reponse = new BaseResult();
            if (jobInfo.JobId < 1)
            {
                reponse.AddError(nameof(jobInfo.JobId), "Không có thông tin đối tượng");
                return reponse;
            }
            var itemJob = await _jobRepository.GetById(jobInfo.JobId);
            reponse.Data = itemJob;
            return reponse;


        }
        public async Task<GetInfoForEditReponse> GetInfoForEdit(JobInfoRequest jobInfo)
        {
            var reponse = new GetInfoForEditReponse();

            var itemJob = await _jobRepository.GetById(jobInfo.JobId);

            var ItemjobInfo = await _jobInfoRepository.FindOneByStatementSql<JobInfoModel>("select d.*, e.RuleStatus from jobInfo d inner join   jobItems e  on d.JobId = e.Id where d.JobId = @jobId ",
               new
               {
                   jobInfo.JobId
               }
               );
            if (ItemjobInfo == null)
            {
                ItemjobInfo = new JobInfoModel()
                {
                    JobId = jobInfo.JobId
                };


            }

            var locations = new List<LocationsJob>();
            var emails = new List<EmailProper>();
            var timeWorks = new List<TimeWorking>();

            var skills = new List<SkillProper>();

            if (!string.IsNullOrEmpty(ItemjobInfo.Locations))
            {
                locations = JsonSerializer.Deserialize<List<LocationsJob>>(ItemjobInfo.Locations);

            }

            if (!string.IsNullOrEmpty(ItemjobInfo.Time_workings))
            {
                timeWorks = JsonSerializer.Deserialize<List<TimeWorking>>(ItemjobInfo.Time_workings);

            }

            if (!string.IsNullOrEmpty(ItemjobInfo.Emails))
            {
                emails = JsonSerializer.Deserialize<List<EmailProper>>(ItemjobInfo.Emails);
            }


            if (!string.IsNullOrEmpty(ItemjobInfo.Skill))
            {
                skills = JsonSerializer.Deserialize<List<SkillProper>>(ItemjobInfo.Skill);
            }
            var dataReponse = new GetInfoForEditReponse()
            {

                Name = ItemjobInfo.Name,
                Aggrement = ItemjobInfo.Aggrement,
                Benefit = ItemjobInfo.Benefit,
                CreateAt = DateTime.Now,
                CreatedBy = ItemjobInfo.CreatedBy,
                Gender = ItemjobInfo.Gender,

                Description = ItemjobInfo.Description,
                Experience = ItemjobInfo.Experience,
                Expired_date = ItemjobInfo.Expired_date,
                Position = ItemjobInfo.Position,
                Profession = ItemjobInfo.Profession,
                Quantity = ItemjobInfo.Quantity,
                Rank = ItemjobInfo.Rank,
                Requirement = ItemjobInfo.Requirement,
                Salary_from = ItemjobInfo.Salary_from,
                Salary_to = ItemjobInfo.Salary_to,
                Type_money = ItemjobInfo.Type_money,
                Type_of_work = ItemjobInfo.Type_of_work,
                RuleStatus = ItemjobInfo.RuleStatus,
                Time_WorkingText = ItemjobInfo.Time_WorkingText,

                Status = itemJob.Status,
                UpdateAt = DateTime.Now,
                Username = ItemjobInfo.fullName,
                Phone = ItemjobInfo.Phone,
                Campaign = itemJob.Campagn,
                Id = itemJob.Id,


            };

            dataReponse.TimeWorks = timeWorks;
            dataReponse.Emails = emails;
            dataReponse.Locations = locations;
            dataReponse.Skills = skills;

            return dataReponse;


        }
        public async Task<DataResult> UpdateJob(JobItemUpdate item)
        {
            var reponse = new JobItemReponse();
            if (item.JobId < 1)
            {

                reponse.Message = "Không có thông tin đối tượng";

                return reponse;
            }

            var jobUpdate = await _jobRepository.GetById(item.JobId);

            if (jobUpdate == null)
            {
                reponse.Message = "Không có thông tin job";
                return reponse;
            }

            jobUpdate.Name = item.Name;

            await _jobRepository.AddOrUPdate(jobUpdate);
            return reponse;
        }
        public async Task<BaseResult> AddViewJob(ViewJobUserAddRequest item)
        {
            var reponse = new BaseResult();
            var jobItem = await _jobRepository.GetBySlug(item.jobslug);
            if (jobItem == null)
            {
                reponse.AddError("không có thông tin đối tượng");
                return reponse;
            }
            await _jobLogViewRepository.ExecuteSqlProcedure("sp_addViewUserJob", new
            {
                JobId = jobItem.Id,
                userId = item.Userid
            });
            return reponse;
        }
        public async Task<ReportStaticInfoOverviewItem> GetOverviewJob(GetOverViewByJobId request)
        {

            var result = await _jobOverViewCounterRepository.GetAll(new JobOverViewCounterRequest()
            {
                From = request.From,
                To = request.To,
                JobId = request.JobId,
            });
            var allJobApplyshort = await _jobOverViewCounterRepository.ExecuteSqlProcerduceToList<JobAppplyViewStatus>("sp_getAllShortJobApply",
            new
            {
                request.JobId
            });

            if (allJobApplyshort == null)
            {
                allJobApplyshort = new List<JobAppplyViewStatus>();
            }

            var rateInfo = 0;
            var totalCV = allJobApplyshort.Count;

            if (totalCV > 0)
            {

                var countApplyjobNotInComfor1 = allJobApplyshort.Where(x => x.Status == 17).Count();
                var countApplyjobNotInComfor2 = allJobApplyshort.Where(x => x.Status == 19).Count();

                var countPhuHop = countApplyjobNotInComfor1 + countApplyjobNotInComfor2;

                if (countPhuHop > 0)
                {
                    rateInfo = (int)Math.Round((double)(100 * countPhuHop) / totalCV);

                }
            }
            var reponse = new ReportStaticInfoOverviewItem()
            {
                From = result.From,
                Data = result.Data,
                TotalApply = result.TotalApply,
                TotalViewer = result.TotalViewer,
                To = result.To,
                JobId = request.JobId,
                JobName = result.JobName,
                StatusText = result.StatusText,
                StatusCode = result.Status,
                Rate = rateInfo
            };
            return reponse;
        }
        public async Task<JobRelattionReponse> GetRelationJob(JobRelattionRequest request)

        {
            var reponse = new JobRelattionReponse();
            var dataJOb = await _jobRepository
                .ExecuteSqlProcerduceToList<JobRelationItemDisplay>("sp_job_getRelation",

                new
                {
                    request.Slug

                });

            if (request.UserId > 0)
            {
                var allJobIdSave = new List<JobCountGroupById>();
                var allJobApply = new List<JobCountGroupById>();
                allJobIdSave = await _jobRepository.ExecuteSqlProcerduceToList<JobCountGroupById>
                (
              "select DISTINCT JobId from jobSave where  UserId = @UserId and  Deleted = 0 ", new { UserId = request.UserId },
              commandType: System.Data.CommandType.Text

              );
                allJobApply = await _jobRepository.ExecuteSqlProcerduceToList<JobCountGroupById>
                (
                    "select DISTINCT  JobId  from jobApply  where  CreatedBy = @userId ", new { UserId = request.UserId },
                    commandType: System.Data.CommandType.Text
                    );

                var listNew = new List<JobRelationItemDisplay>();
                foreach (var item in dataJOb)
                {
                    var itemSave = allJobIdSave.Any(x => x.JobId == item.JobId);
                    var itemApply = allJobApply.Any(x => x.JobId == item.JobId);
                    item.IsLike = itemSave;
                    item.IsSave = itemSave;
                    item.IsApply = itemApply;
                    listNew.Add(item);
                }
                reponse.Data = listNew;
            }
            else
            {
                reponse.Data = dataJOb;
            }
            return reponse;



        }
        public async Task<JobRecommendedReponse> GetRecomendJob(JobRecommendedRequest request)

        {
            var reponse = new JobRecommendedReponse();
            var listData = new List<JobrecommendedItemDisplay>()
            {

                new JobrecommendedItemDisplay()
                {

                    CompanyName = "Công ty cổ phần tập đoàn VietStar",
                    FieldArray = "IT, Marketting",
                    JobId = 12,
                    IsLike =true,

                    SalaryFrom = 10,
                    SalaryTo =15,

                    LastUpdate = DateTime.Now,
                    PositionText = "Nhân viên tư vấn Telesale"

                },

                  new JobrecommendedItemDisplay()
                {

                    CompanyName = "Công ty cổ phần tập đoàn VietStar",
                     FieldArray = "IT, Marketting",

                    JobId = 12,

                    SalaryFrom = 10,
                    IsLike =false,
                    SalaryTo =15,

                    LastUpdate = DateTime.Now,
                    PositionText = "Nhân viên tư vấn Telesale"

                },
                    new JobrecommendedItemDisplay()
                {

                    CompanyName = "Công ty cổ phần tập đoàn VietStar",
                     FieldArray = "IT, Marketting",

                    JobId = 12,
                    SalaryFrom = 10,
                    SalaryTo =15,
                     LastUpdate = DateTime.Now,
                    PositionText = "Nhân viên tư vấn Telesale"

                }
            };
            listData = new List<JobrecommendedItemDisplay>();

            reponse.Data = listData;

            return reponse;
        }

        public async Task<dynamic> GetDetailMetadata(string jobSlug)
        {
            var jobInfo = await _jobRepository.GetBySlug(jobSlug);

            if (jobInfo == null)
            {
                return new
                {
                    title = "",
                    KeyWord = "Topmass",
                    Author = "Topmass",
                    ShortDes = "",
                    linkImage = "https://topmass.vn/imgs/logo-new.svg"
                };
            }
            return new
            {
                title = jobInfo.Name,
                KeyWord = "Topmass",
                Author = "Topmass",
                ShortDes = jobInfo.Name,
                linkImage = "https://topmass.vn/imgs/logo-new.svg"
            };

        }
        public async Task<JobDetailResult> GetInfoJOb(CandidateJobInfoRequest request)
        {

            var result = new JobDetailResult()
            {
                JobId = -1,
                CompanyData = new CompanyInfoDisplay(),
                DataJob = new JobInfoDisplay()
                {
                    Content = "",
                    Hashtags = "",
                    Slug = "",
                    ExperienceText = "",
                    JobName = "",
                    LocationText = "",
                    RangeSalary = "",
                    CommonData = new JobCommonData
                    {
                        PublishDate = DateTime.Now,
                        ExperienceText = "",
                        FieldText = "",
                        FormOfWork = "",
                        GenderText = "",
                        LevelText = "",
                        NumOfRecruits = 1,
                        ProfessionText = ""
                    }
                }
            };
            var jobInfo = await _jobRepository.GetBySlug(request.Slug);

            if (jobInfo == null)
            {
                return result;
            }

            var jobDetail = await _jobInfoRepository.FindOneByStatementSql<JobInfoModel>("select top 1 * from jobInfo d where d.JobId = @jobid", new
            {
                jobid = jobInfo.Id

            });

            if (jobDetail == null)
            {
                return result;
            }

            var jobDisplayItem = await _jobDisplayItemRepository.FindOneByStatementSql<JobDisplayItemModel>("select top 1 * from jobItemDisplay d where d.JobId = @jobid", new
            {
                jobid = jobInfo.Id

            });
            if (jobDisplayItem == null)
            {
                return result;
            }

            var campagn = await _campagnRepository.GetById(jobInfo.Campagn.Value);

            if (campagn == null)
            {
                return result;
            }

            var companyInfo = await _companyInfoRepository.FindOneByStatementSql<CompanyInfoModel>("select * from CompanyInfo  d where d.RelId = @companyid",
            new
            {
                companyid = campagn.RelId
            });
            if (companyInfo == null)
            {
                return result;
            }
            var avatarLogo = "";
            if (string.IsNullOrEmpty(companyInfo.LogoLink))
            {
                avatarLogo = "";
            }
            else
            {
                avatarLogo = "https://www.cdn.topmass.vn/static/" + companyInfo.LogoLink;
            }


            var companyData = new CompanyInfoDisplay()
            {
                AddressInfo = companyInfo.AddressInfo,
                AvatarLink = avatarLogo,
                CompanyId = companyInfo.RelId.Value,
                CompanyName = companyInfo.FullName,
                Capacity = companyInfo.Capacity,
                Slug = companyInfo.Slug
            };

            var genderText = "Không yêu cầu";
            if (jobDetail.Gender.HasValue)
            {

                if (jobDetail.Gender.Value == 1)
                {
                    genderText = "Nam";
                }
                else if (jobDetail.Gender.Value == 2)
                {
                    genderText = "Nữ";
                }

            }
            var commonData = new JobCommonData
            {
                PublishDate = jobDetail.Expired_date,
                ExperienceText = jobDisplayItem.ExperienceText,
                FieldText = jobDisplayItem.ProfessionText,
                FormOfWork = jobDisplayItem.TypeOfWork,
                GenderText = genderText,
                LevelText = jobDisplayItem.Rank,
                NumOfRecruits = jobDetail.Quantity.HasValue ? jobDetail.Quantity.Value : 1,
                ProfessionText = jobDisplayItem.ProfessionText

            };


            var locationsArray = new List<LocationsJob>();

            if (!string.IsNullOrEmpty(jobDetail.Locations))
            {
                locationsArray = JsonSerializer.Deserialize<List<LocationsJob>>(jobDetail.Locations);
            }


            var DataJob = new JobInfoDisplay()
            {
                Content = jobDetail.Description,
                Hashtags = "",
                Slug = jobInfo.Slug,
                ExperienceText = jobDisplayItem.ExperienceText,
                JobName = jobInfo.Name,
                Requirement = jobDetail.Requirement,
                Benefit = jobDetail.Benefit,
                Description = jobDetail.Description,
                Skill = jobDetail.Skill,
                LocationText = jobDisplayItem.LocationText,
                SalaryFrom = jobDetail.Salary_from.HasValue ? jobDetail.Salary_from.Value : 0,
                SalaryTo = jobDetail.Salary_to.HasValue ? jobDetail.Salary_to.Value : 0,
                CurrencyCode = jobDetail.Type_money,
                RangeSalary = jobDisplayItem.RangeSalary,
                CommonData = commonData,
                Expired_date = jobDetail.Expired_date,
                Time_workings = jobDetail.Time_workings,
                Time_WorkingText = jobDetail.Time_WorkingText,
                Locations = jobDetail.Locations,
                LocationsInfoMation = locationsArray,
                Aggrement = jobDetail.Aggrement
            };

            var resultData = new JobDetailResult()
            {
                JobId = jobInfo.Id,
                CompanyData = companyData,
                DataJob = DataJob
            };

            var isApply = false;
            var isLike = false;

            if (request.UserId < 1)
            {

            }
            else
            {
                var allJobIdSave = await _jobInfoRepository.ExecuteSqlProcerduceToList<JobIdCount>
                (
                    "select DISTINCT JobId from jobSave where JobId = @jobId and   UserId = @UserId and Deleted = 0  ",
                    new { request.UserId, jobId = jobInfo.Id },
                    commandType: CommandType.Text
                );

                var allJobApply = await _jobInfoRepository.ExecuteSqlProcerduceToList<JobIdCount>
                 (
                     "select DISTINCT  JobId  from jobApply  where  JobId = @jobId  and   CreatedBy = @userId and Deleted = 0 ",
                      new { request.UserId, jobId = jobInfo.Id },
                     commandType: CommandType.Text
                 );



                if (allJobApply.Count > 0)
                {
                    isApply = true;
                }

                if (allJobIdSave.Count > 0)
                {
                    isLike = true;
                }

            }

            resultData.JobExtra = new
            {
                isAply = isApply,
                isSave = isLike
            }; ;

            return resultData;
        }
    }
}
