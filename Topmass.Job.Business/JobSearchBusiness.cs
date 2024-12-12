using System.Text;
using System.Text.RegularExpressions;
using Topmass.Core.Repository;
using Topmass.Job.Business.Model;

namespace Topmass.Job.Business
{
    public class JobSearchBusiness : IJobSearchBusiness
    {


        private readonly IJobRepository _jobRepository;

        private readonly IJobInfoRepository _jobInfoRepository;

        private readonly IJobLogViewRepository _jobLogViewRepository;

        private readonly IJobOverViewCounterRepository _jobOverViewCounterRepository;
        private readonly IJobDisplayItemRepository _jobDisplayItemRepository;

        private readonly IMasterDataRepository _masterDataRepository;
        public JobSearchBusiness(

            IJobRepository jobRepository,
            IJobLogViewRepository jobLogViewRepository,
            IJobOverViewCounterRepository jobOverViewCounterRepository,
            IJobInfoRepository jobInfoRepository,
            IJobDisplayItemRepository jobDisplayItemRepository
            )
        {

            _jobRepository = jobRepository;

            _jobLogViewRepository = jobLogViewRepository;
            _jobOverViewCounterRepository = jobOverViewCounterRepository;
            _jobInfoRepository = jobInfoRepository;
            _jobDisplayItemRepository = jobDisplayItemRepository;
        }


        private string ToUrlSlug(string value)
        {

            //First to lower case
            value = value.ToLowerInvariant();

            //Remove all accents
            var bytes = Encoding.GetEncoding("Cyrillic").GetBytes(value);
            value = Encoding.ASCII.GetString(bytes);

            //Replace spaces
            value = Regex.Replace(value, @"\s", "-", RegexOptions.Compiled);

            //Remove invalid chars
            value = Regex.Replace(value, @"[^a-z0-9\s-_]", "", RegexOptions.Compiled);

            //Trim dashes from end
            value = value.Trim('-', '_');

            //Replace double occurences of - or _
            value = Regex.Replace(value, @"([-_]){2,}", "$1", RegexOptions.Compiled);

            return value;
        }

        private int ConverToNumber(string valueConvert)
        {
            if (string.IsNullOrEmpty(valueConvert))
            {
                return -1;
            }
            try
            {
                return int.Parse(valueConvert);
            }
            catch (Exception)
            {

                return -1;
            }
        }

        public async Task<GetAllBestJobOptimizationReponse>
            GetAllBestJobOptimization
            (GetAllBestJobOptimizationRequest itemAdd)
        {
            var reponse = new GetAllBestJobOptimizationReponse();
            var sqlText = "sp_job_getJobOptimization";
            var salaryfrom = -1;
            var salaryto = -1;
            var experienceId = -1;
            var professionId = -1;


            if (itemAdd.TypeSearch == "1")
            {
                if (!string.IsNullOrEmpty(itemAdd.ValueType))
                {
                    var arrayNumber = itemAdd.ValueType.Split('-');
                    salaryfrom = ConverToNumber(arrayNumber[0]);
                    salaryto = ConverToNumber(arrayNumber[0]);

                }
            }
            if (itemAdd.TypeSearch == "2")
            {
                experienceId = ConverToNumber(itemAdd.ValueType);
            }

            if (itemAdd.TypeSearch == "3")
            {
                professionId = ConverToNumber(itemAdd.ValueType);
            }
            if (itemAdd.TypeSearch == "0")
            {
                sqlText = "sp_job_getJobOptimizationByPlace";
                var request2 = new
                {
                    itemAdd.Limit,
                    itemAdd.From,
                    itemAdd.To,
                    searchLocation = itemAdd.ValueType,
                    itemAdd.Page,
                    itemAdd.OrderBy
                };
                var dataJOb1 = await _jobRepository
                    .ExecuteSqlProcerduceToList<BestJobOptimizationDisplayItemData>(sqlText,
                    request2);
                reponse.Page = itemAdd.Page;
                reponse.Limit = itemAdd.Limit;
                var itemsearch = dataJOb1.FirstOrDefault();
                if (itemsearch != null)
                {
                    reponse.TotalRecord = itemsearch.TotalRecord;
                }
                reponse.Data = dataJOb1;
                return reponse;
            }
            var request = new
            {
                itemAdd.Limit,
                itemAdd.From,
                itemAdd.To,
                itemAdd.Page,
                salaryfrom,
                salaryto,
                professionId,
                experienceId,
                itemAdd.OrderBy
            };
            var dataJOb = await _jobRepository
                .ExecuteSqlProcerduceToList<BestJobOptimizationDisplayItemData>(sqlText,
                request);
            reponse.Page = itemAdd.Page;
            reponse.Limit = itemAdd.Limit;
            var itemsearch1 = dataJOb.FirstOrDefault();
            if (itemsearch1 != null)
            {
                reponse.TotalRecord = itemsearch1.TotalRecord;
            }
            reponse.Data = dataJOb;

            if (itemAdd.UserId > 0)
            {
                var allJobIdSave = new List<JobCountGroupById>();
                var allJobApply = new List<JobCountGroupById>();
                allJobIdSave = await _jobRepository.ExecuteSqlProcerduceToList<JobCountGroupById>
                (
              "select DISTINCT JobId from jobSave where  UserId = @UserId and  Deleted = 0 ", new { UserId = itemAdd.UserId.Value },
              commandType: System.Data.CommandType.Text

              );
                allJobApply = await _jobRepository.ExecuteSqlProcerduceToList<JobCountGroupById>
                (
                    "select DISTINCT  JobId  from jobApply  where  CreatedBy = @userId ", new { UserId = itemAdd.UserId.Value },
                    commandType: System.Data.CommandType.Text
                    );

                var listNew = new List<BestJobOptimizationDisplayItemData>();
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

        public async Task<GetAllBestJobOptimizationReponse> GetSuitableJob(GetSuitableJobRequest request)
        {

            var reponse = new GetAllBestJobOptimizationReponse();
            var dataJOb = await _jobRepository
                .ExecuteSqlProcerduceToList<BestJobOptimizationDisplayItemData>("sp_job_getSuitableJob",
                new
                {
                    request.LocationSearch,
                    request.Limit,
                    request.Page

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

                var listNew = new List<BestJobOptimizationDisplayItemData>();
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

            reponse.Limit = request.Limit;
            reponse.Page = request.Page;
            var itemfirst = dataJOb.FirstOrDefault();
            if (itemfirst != null)
            {
                reponse.TotalRecord = itemfirst.TotalRecord;
            }

            return reponse;
        }



        public async Task<GetAllBestJobOptimizationReponse> SearchJob(SearchJobRequest request)
        {
            var reponse = new GetAllBestJobOptimizationReponse();
            var dataJob = await _jobRepository
                .ExecuteSqlProcerduceToList<BestJobOptimizationDisplayItemData>("sp_search_job",
                new
                {
                    request.Location,
                    request.TypeOfWork,
                    request.Limit,
                    request.Page,
                    request.KeyWord,
                    request.Field,
                    request.RankLevel,
                    request.ModeOrderBy,
                    request.Gender,
                    request.SalaryFrom,
                    request.SalaryTo,
                    request.ExperienceId
                });
            var itemfirst = dataJob.FirstOrDefault();
            if (itemfirst != null)
            {
                reponse.TotalRecord = itemfirst.TotalRecord;
                reponse.Limit = request.Limit;
                reponse.Page = request.Page;

            }
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

                var listNew = new List<BestJobOptimizationDisplayItemData>();
                foreach (var item in dataJob)
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
                reponse.Data = dataJob;
            }
            return reponse;
        }



        public async Task<GetAllBestJobOptimizationReponse> GetAttractiveJobs(GetAttractiveJobs request)
        {
            var reponse = new GetAllBestJobOptimizationReponse();
            var dataJOb = await _jobRepository
                .ExecuteSqlProcerduceToList<BestJobOptimizationDisplayItemData>("sp_job_getGetAttractiveJobs",
                new
                {
                    request.LocationSearch,
                    request.Limit,
                    request.Page,
                    request.UserId

                });

            if (request.UserId > 0)
            {
                var allJobIdSave = new List<JobCountGroupById>();
                var allJobApply = new List<JobCountGroupById>();
                allJobIdSave = await _jobRepository.ExecuteSqlProcerduceToList<JobCountGroupById>
                (
              "select DISTINCT JobId from jobSave where  UserId = @UserId  and  Deleted = 0 ", new { UserId = request.UserId },
              commandType: System.Data.CommandType.Text

              );
                allJobApply = await _jobRepository.ExecuteSqlProcerduceToList<JobCountGroupById>
                (
                    "select DISTINCT  JobId  from jobApply  where  CreatedBy = @userId ", new { UserId = request.UserId },
                    commandType: System.Data.CommandType.Text
                    );

                var listNew = new List<BestJobOptimizationDisplayItemData>();
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

            reponse.Limit = request.Limit;
            reponse.Page = request.Page;
            var itemfirst = dataJOb.FirstOrDefault();
            if (itemfirst != null)
            {
                reponse.TotalRecord = itemfirst.TotalRecord;
            }
            return reponse;
        }



    }
}
