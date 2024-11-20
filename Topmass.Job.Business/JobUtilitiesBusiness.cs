
using Topmass.Core.Model.Campagn;
using Topmass.Core.Repository;

namespace Topmass.Job.Business
{
    public class JobUtilitiesBusiness : IJobUtilitiesBusiness
    {




        private readonly IJobsaveRepository _jobsaveRepository;
        private readonly IJobRepository jobRepository;
        public JobUtilitiesBusiness(


            IJobsaveRepository jobOverViewCounterRepository,
            IJobRepository _jobRepository
            )
        {

            _jobsaveRepository = jobOverViewCounterRepository;
            jobRepository = _jobRepository;
        }

        public async Task<bool> AddJobSave(int jobId, int userId)
        {
            var jobitem = new JobsaveModel()
            {
                CreateAt = DateTime.Now,
                CreatedBy = userId,
                Deleted = false,
                JobId = jobId,
                Status = 1,
                UserId = userId,
                UpdateAt = DateTime.Now,
                UpdatedBy = userId
            };

            var jobitemexit = await _jobsaveRepository.FindOneByStatementSql<JobsaveModel>("select * from jobSave  where JobId = @jobId  and UserId= @userId ", new
            {
                jobId,
                userId
            });

            if (jobitemexit != null)
            {
                jobitemexit.UpdateAt = DateTime.Now;
                await _jobsaveRepository.AddOrUPdate(jobitemexit);
                return true;

            }
            await _jobsaveRepository.AddOrUPdate(jobitem);
            return true; ;

        }
        public async Task<bool> AddJobSave(string jobSlug, int userId)
        {

            var jobInfo = await jobRepository.GetBySlug(jobSlug);

            var jobitem = new JobsaveModel()
            {
                CreateAt = DateTime.Now,
                CreatedBy = userId,
                Deleted = false,
                JobId = jobInfo.Id,
                Status = 1,
                UserId = userId,
                UpdateAt = DateTime.Now,
                UpdatedBy = userId
            };

            var jobitemexit = await _jobsaveRepository.FindOneByStatementSql<JobsaveModel>("select * from jobSave  where JobId = @jobId  and UserId= @userId and   ISNULL(Deleted,0) =0 ", new
            {
                JobId = jobInfo.Id,
                userId
            });

            if (jobitemexit != null)
            {
                jobitemexit.UpdateAt = DateTime.Now;
                await _jobsaveRepository.AddOrUPdate(jobitemexit);
                return true;

            }
            await _jobsaveRepository.AddOrUPdate(jobitem);
            return true; ;

        }


        public async Task<bool> RemoveJobSave(int id, int userId)
        {

            var jobitemexit = await _jobsaveRepository.FindOneByStatementSql<JobsaveModel>("select * from jobSave where JobId = @jobId and UserId = @userId and  Deleted = 0 ",
                 new
                 {
                     jobId = id,
                     userId = userId
                 });
            if (jobitemexit == null)
            {
                return true;
            }
            jobitemexit.Status = 2;
            jobitemexit.Deleted = true;
            jobitemexit.UpdateAt = DateTime.Now;
            await _jobsaveRepository.AddOrUPdate(jobitemexit);
            return true; ;

        }


    }
}
