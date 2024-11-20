using Topmass.Core.Model.Campagn;
using Topmass.Core.Model.CV;
using Topmass.Core.Model.JobAply;
using Topmass.Core.Repository;
using Topmass.Core.Repository.Notification;
using Topmass.CV.Repository.Model;
using Topmass.Notification.Repository;

namespace Topmass.CV.Repository
{
    public class CVRepository : ICVRepository
    {

        private readonly IJobApplyRepository _jobApplyRepository;
        private readonly IResumeRepository _resumeRepository;
        private readonly IResumeUIRepository _resumeUIRepository;
        private readonly IJobInfoRepository _jobInfoRepository;
        private readonly ICandidateRepository _candidateRepository;
        private readonly INotificationRepository _notificationRepository;


        public CVRepository(IResumeRepository resumeRepository,
            IResumeUIRepository resumeUIRepository,
            IJobApplyRepository jobApplyRepository,
            IJobInfoRepository jobInfoRepository,
            INotificationRepository notificationRepository,
            ICandidateRepository candidateRepository


            )
        {
            _jobApplyRepository = jobApplyRepository;
            _resumeRepository = resumeRepository;
            _resumeUIRepository = resumeUIRepository;
            _jobInfoRepository = jobInfoRepository;
            _notificationRepository = notificationRepository;
            _candidateRepository = candidateRepository;
        }

        public async Task<CVResumeResponse> CreateCV(CVResumeRequest request)
        {
            var respone = new CVResumeResponse()
            { };
            var resumeInsert = new Resume()
            {
                CreateAt = DateTime.Now,
                CreatedBy = request.HandleBy,
                LinkFile = request.LinkFile,
                TemplateId = 1,
                TypeData = request.TypeData,
                Status = 1,
                UserId = request.HandleBy,
                DataInput = request.DataInput,
                Email = "",
                Deleted = false
            };
            await _resumeRepository.AddOrUPdate(resumeInsert);
            respone.Success = true;
            return respone;
        }



        public async Task<ApplyJobWithCreateCVReponse> ApplyJobWithCreateCV(ApplyJobWithCreateCV request)
        {
            var reponse = new ApplyJobWithCreateCVReponse();
            var result = await _jobApplyRepository.ExecuteStatementSql("sp_applyJobWithCreateCV", request);

            reponse.Success = true;

            return reponse;
        }
        public async Task<GetAllCVReponse> GetAllCVOfCan(CVResumeRequest request)
        {
            var respone = new GetAllCVReponse()
            {

            };
            if (request.UserId < 1)
            {
                return respone;
            }
            var dataResult = await _resumeRepository
                    .ExecuteSqlProcerduceToList<ResumeDisplayItem>("sp_getAllCVCandidate",
                    new
                    {
                        request.UserId,
                        request.TypeData

                    });
            respone.Data = dataResult;
            return respone;
        }
        public async Task<GetAllCVByJobReponse> GetAllCVByJob(GetAllCVByJobRequest request)
        {
            var respone = new GetAllCVByJobReponse()
            {

            };
            if (request.JobId < 1)
            {
                return respone;
            }

            var sql = "sp_getAllCVByJob";

            if (request.TypeData != 3)
            {
                sql = "sp_getAllCVByJob";
            }
            else
            {
                sql = "sp_getAllSearchCVByJob";
            }

            var dataResult = await _resumeRepository
                .ExecuteSqlProcerduceToList<JobApplyDisplayItem>(sql,
                  new
                  {
                      request.JobId,
                      request.UserId
                  });
            respone.Data = dataResult;
            return respone;
        }

        public async Task<CVapplyJobReponse> ApplyJob(CVapplyJobRequest request)
        {

            var response = new CVapplyJobReponse();
            var resumeInsert = new JobApply()
            {
                Status = 21,
                FullName = request.FullName,
                Email = request.Email,
                Phone = request.Phone,
                CreateAt = DateTime.Now,
                CreatedBy = request.HandleBy,
                CVId = request.CVId,
                JobId = request.JobId,
                Deleted = false
            };
            var applyId = await _jobApplyRepository.AddAndGetId(resumeInsert);

            var jobInfo = await _jobInfoRepository.FindOneByStatementSql<JobInfoModel>("select * from jobInfo where JobId = @jobId",
            new
            {
                jobId = request.JobId

            });
            if (jobInfo == null)
            {
                return response;
            }
            var positonJob = jobInfo.Position;
            var humanUser = await _candidateRepository.FindOneByStatementSql<GetIdentifyReponse>(
                "select top 1 id, UserName from Recruiter where id = @createdBy", new
                {
                    createdBy = jobInfo.CreatedBy
                }
              );
            var itemNotification = new NotificationContentModel
            {
                Title = " Có CV mới  ",
                Content = "Ứng viên  đã  nộp hồ sơ   cho vị trí " + positonJob,
                LableText = "Hệ thống",
                RelId = applyId,
                LinkFile = "",
                TypeInfo = 2,
                UserName = humanUser.UserName,
                Status = 0,
                UserId = request.HandleBy
            };
            await _notificationRepository.AddOrUPdate(itemNotification);
            return response;
        }



        public async Task<GetAllCVByCampaignReponse> GetAllCVApply(GetAllCVByCampaignRequest request)
        {
            var respone = new GetAllCVByCampaignReponse()
            {

            };
            if (request.UserId < 1)
            {
                return respone;
            }
            var sqlText = "sp_getAllCVByCampangn";
            if (request.Source > 1)
            {
                sqlText = "sp_getAllCVBySearchCV";
            }
            var requestfilter = new
            {
                request.JobId,
                request.TypeData,
                request.Status,
                request.CampagnId,
                request.UserId
            };

            var dataResult = await _resumeRepository
                   .ExecuteSqlProcerduceToList<JobApplyDisplayItem>(sqlText,
                  requestfilter);
            respone.Data = dataResult;
            return respone;
        }


    }
}
