using Microsoft.Extensions.Configuration;
using Topmass.Core.Model.JobAply;

namespace Topmass.Core.Repository
{
    public partial class JobApplyRepository : RepositoryBase<JobApply>, IJobApplyRepository
    {
        public JobApplyRepository(IConfiguration configuration) : base(configuration)
        {
            tableName = "jobApply";
        }


        public async Task<bool> AddCounterApply(int jobId, int UserId)
        {
            var result = await ExecuteSqlProcedure("sp_AddCounterApply", new
            {
                JobId = jobId,
                userId = UserId
            });
            return result;
        }

    }
}
