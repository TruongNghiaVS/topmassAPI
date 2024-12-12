using Newtonsoft.Json;
using Topmass.Bussiness.Mail;
using Topmass.Core.Model.CV;
using Topmass.Core.Repository;
using Topmass.CV.Business.Model;
using Topmass.CV.Repository;
using Topmass.CV.Repository.Model;
using Topmass.Utility;

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
                FullName = request.FullName,
                Introduction = request.Introduction,
            };
            result.Data = await _cVRepository.ApplyJob(resumeInsert);
            await _mailJobBissness.NotficationRecruiterWhenHasApply(new NotficationRecruiterWhenHasApplyRequest()
            {
                JobId = itemJob.Id,
                NameInput = request.FullName,
                Introduction = request.Introduction,
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
            await _mailJobBissness.NotficationRecruiterWhenHasApply(new NotficationRecruiterWhenHasApplyRequest()
            {
                JobId = jobinfo.Id,
                Introduction = request.Introduction,
                NameInput = request.FullName,
                UserId = request.UserId
            });
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
                Status = request.StatusCode.HasValue == true ? request.StatusCode.Value : -1,
                KeyWord = request.KeyWord,
                ViewMode = request.ViewMode,
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
        public async Task<CheckGenFileDigitalReponse> CheckGenFileDigital(int userId)
        {
            var result = new CheckGenFileDigitalReponse();
            var resumeFile = await _resumeRepository.FindOneByStatementSql<Resume>
                ("select top 1 * from resumes where UserId  = @userid and TypeData = 5", new
                {
                    userid = userId
                });
            if (resumeFile == null)
            {
                result.IsCreateNewFile = true;
                result.LinkFile = "";
                return result;
            }
            else
            {

                if (resumeFile.IsReload.HasValue)
                {
                    if (resumeFile.IsReload == 0)
                    {
                        result.IsCreateNewFile = false;
                        result.LinkFile = resumeFile.LinkFile;
                        return result;
                    }
                }
                result.IsCreateNewFile = true;
                result.LinkFile = "";
                return result;
            }
        }
        public async Task<CVReponseDigitalAdd> AddOrUpdateCVDigital(CVRequestDigitalAdd request)
        {
            var reponse = new CVReponseDigitalAdd();
            var linkfile = "";
            if (request.FileCV != null)
            {
                var fileCV = request.FileCV;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(@"https://www.cdn.topmass.vn");
                    var fileNameCV = "CV-" + Utilities.SlugifySlug2(request.FullName) + "-" + "Topmass";
                    byte[] fileByteArray;    //1st change here
                    using (var item = new MemoryStream())
                    {
                        request.FileCV.CopyTo(item);
                        fileByteArray = item.ToArray(); //2nd change here
                    }
                    var formContent = new MultipartFormDataContent
                    {
                        { new StreamContent(fileCV.OpenReadStream()),"File",fileCV.FileName },
                        {new StringContent(fileNameCV),"FileName" }
                    };
                    var result = await client.PostAsync("/FileMedia/UploadFile", formContent);
                    if (result.IsSuccessStatusCode)
                    {
                        var contents = await result.Content.ReadAsStringAsync();
                        var result2 = JsonConvert.DeserializeObject<FileReponse>(contents);
                        linkfile = result2.Data.FullLink;
                    }
                }
            }
            var resumeInsert = new CVResumeRequest()
            {
                DataInput = "",
                HandleBy = request.HandleBy,
                LinkFile = linkfile,
                TemplateID = request.TemplateID,
                TypeData = 5,
                UserId = request.UserId
            };
            reponse.Success = true;
            await _cVRepository.AddOrUpdateCVDigital(resumeInsert);
            reponse.Data = new
            {
                linkFile = linkfile
            };
            return reponse;
        }

        public async Task<CVReponseDigitalAdd> AddCVToStore(CVRequesAddToStore request)
        {
            return new CVReponseDigitalAdd();
        }
        public async Task<GetAllCVOfJobReponse> GetAllCVApplyNew(FilterGetAllCVApply request)
        {
            var respone = new GetAllCVOfJobReponse() { };
            if (request.UserId < 1)
            {
                return respone;
            }
            var requestFilter = new InputGetAllCVApplyFilter()
            {
                UserId = request.UserId,
                CampaignId = request.CampaignId,
                KeyWord = request.KeyWord,
                Source = request.Source,
                Limit = request.Limit,
                Page = request.Page,
                StatusCode = request.StatusCode

            };
            var dataResult = await _cVRepository
                   .GetAllCVApplyNew(requestFilter);
            respone.Data = dataResult.Data;
            return respone;
        }
    }
}
