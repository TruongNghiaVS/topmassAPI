using Microsoft.Extensions.Configuration;
using Topmass.Core.Model.JobAply;

namespace Topmass.Core.Repository
{
    public partial class JobApplyStatusRepository : RepositoryBase<JobApplyStatus>, IJobApplyStatusRepository
    {
        public JobApplyStatusRepository(IConfiguration configuration) : base(configuration)
        {
            tableName = "JobApplyStatus";
        }






    }
}
