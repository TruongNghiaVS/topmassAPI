using Microsoft.Extensions.Configuration;
using Topmass.Core.Model.Campagn;

namespace Topmass.Core.Repository
{
    public partial class JobInfoRepository : RepositoryBase<JobInfoModel>, IJobInfoRepository
    {
        public JobInfoRepository(IConfiguration configuration) : base(configuration)
        {
            tableName = "jobInfo";
        }


    }
}
