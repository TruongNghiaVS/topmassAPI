using Topmass.Bussiness.Mail;
using Topmass.Core.Repository;
using Topmass.CV.Business.Model;
using Topmass.CV.Repository;
using Topmass.CV.Repository.Model;

namespace Topmass.CV.Business
{
    public class CVBusiness : ICVBusiness
    {

        private readonly ICVRepository _cVRepository;

        private readonly IResumeRepository _resumeRepository;
        private readonly IJobRepository _jobRepository;
        private readonly IMailJobBissness _mailJobBissness;

        public CVBusiness(ICVRepository cVRepository,
             IResumeRepository resumeRepository,
             IJobRepository jobRepository,
             IMailJobBissness mailJobBissness


            )
        {
            _cVRepository = cVRepository;
            _resumeRepository = resumeRepository;
            _jobRepository = jobRepository;
            _mailJobBissness = mailJobBissness;


        }
        public async Task<ApplyJobResponeAdd> ApplyJob(ApplyJobRequestAdd request)
        {
            var result = new ApplyJobResponeAdd();

            if (request.CVId < 1)
            {
                result.AddError(nameof(request.CVId), "Thiếu thông tin CV");
            }
            if (!result.Success)
            {
                return result;
            }

            var itemJob = await _jobRepository.GetBySlug(request.JobSlug);
            var resumeInsert = new CVapplyJobRequest()
            {
                CVId = request.CVId,
                HandleBy = request.HandleBy,
                JobId = itemJob.Id,
                Email = request.Email,
                Phone = request.Phone,
                FullName = request.FullName
            };
            result.Data = await _cVRepository.ApplyJob(resumeInsert);

            await _mailJobBissness.NotficationRecruiterWhenHasApply(new NotficationRecruiterWhenHasApplyRequest()
            {
                JobId = itemJob.Id,
                UserId = request.HandleBy
            });

            return result;
        }
        public async Task<ApplyJobWithCreateResponeAdd> ApplyJobWithCV(ApplyJobWithCreateCVAdd request)
        {

            var result = new ApplyJobWithCreateResponeAdd();
            if (string.IsNullOrWhiteSpace(request.LinkFile))
            {
                result.AddError(nameof(request.LinkFile), "Thiếu thông tin File");
            }
            if (!result.Success)
            {
                return result;
            }

            var jobinfo = await _jobRepository.GetBySlug(request.jobSlug);

            if (jobinfo == null)
            {
                result.AddError(nameof(request.jobSlug), "thiếu thông tin job");
                return result;
            }
            var resumeInsert = new ApplyJobWithCreateCV()
            {
                Email = request.Email,
                FullName = request.FullName,
                LinkFile = request.LinkFile,
                JobId = jobinfo.Id,
                Phone = request.Phone,
                TemplateID = request.TemplateID,
                TypeData = request.TypeData,
                UserId = request.UserId,


            };
            result.Data = await _cVRepository.ApplyJobWithCreateCV(resumeInsert);
            return result;
        }
        public async Task<SearchCVReponse> SearchCV(SearchCVRequestInfo request)
        {
            var result = new SearchCVReponse();
            var dataSearch = await _jobRepository
                .ExecuteSqlProcerduceToList<ItemSearchCVDisplay>("sp_cv_search",
                new
                {
                    request.KeyWord,
                    request.LocationCode,
                    EducationalLevel = request.EducationalLevelArray,
                    request.FromYear,
                    request.ToYear,
                    request.SchoolSearch,
                    request.Gender,
                    request.CvKey,
                    request.Limit,
                    request.Page
                }
                );
            result.Data = dataSearch;
            var countTotal = dataSearch.FirstOrDefault() != null ?
                            dataSearch.FirstOrDefault().TotalRecord : 0;
            result.Limit = request.Limit;
            result.Page = request.Page;
            result.Total = countTotal;
            return result;

        }
        public async Task<CVReponseAdd> CreateCV(CVRequestAdd request)
        {
            var respone = new CVReponseAdd();
            var resumeInsert = new CVResumeRequest()
            {
                DataInput = request.DataInput,
                HandleBy = request.HandleBy,
                LinkFile = request.LinkFile,
                TemplateID = request.TemplateID,
                TypeData = request.TypeData,
                UserId = request.UserId
            };
            await _cVRepository.CreateCV(resumeInsert);
            respone.Success = true;
            return respone;
        }


        public async Task<GetAllOfHumanRequestReponse> GetAllCVApply(GetAllOfHumanRequest request)
        {
            var respone = new GetAllOfHumanRequestReponse()
            {

            };
            if (request.UserId < 1)
            {
                return respone;
            }

            var requestFilter = new GetAllCVByCampaignRequest()
            {
                JobId = request.JobId,
                CampagnId = request.CampagnId,
                TypeData = request.TypeData,
                UserId = request.UserId,
                Status = request.Status,
                Key = request.Key,
                Source = request.Source.HasValue ? request.Source.Value : 1

            };
            var dataResult = await _cVRepository
                   .GetAllCVApply(requestFilter);
            respone.Data = dataResult.Data;
            return respone;
        }

        public async Task<GetAllCVOfJobReponse> GetAllCVOfJob(GetAllCVOfJobRequest request)
        {
            var respone = new GetAllCVOfJobReponse() { };
            if (request.UserId < 1)
            {
                return respone;
            }
            var requestFilter = new GetAllCVByJobRequest()
            {
                JobId = request.JobId,
                TypeData = request.TypeData,
                UserId = request.UserId

            };
            var dataResult = await _cVRepository
                   .GetAllCVByJob(requestFilter);
            respone.Data = dataResult.Data;
            return respone;
        }

        public async Task<GetInfoCVReponse> GetInfo(GetInfoCVRequest request)
        {
            var respone = new GetInfoCVReponse()
            { };
            if (request.CVId < 1)
            {
                return respone;
            }
            var dataResult = await _resumeRepository
                            .ExecuteSqlProcerduceToList<ResumeItemDisplay>("sp_cv_getInfo", new
                            {
                                Id = request.CVId
                            });
            if (dataResult == null || dataResult.Count < 1)
            {
                respone.Data = new ResumeItemDisplay();
            }
            respone.Data = dataResult.First();
            return respone;
        }

        public async Task<SearchCVReponse> GetDetailSearch(string searchId)
        {

            return new SearchCVReponse();
        }

    }
}
