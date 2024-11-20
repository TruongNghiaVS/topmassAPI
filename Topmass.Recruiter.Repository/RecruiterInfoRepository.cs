using Microsoft.Extensions.Configuration;
using Topmass.Core.Model;
using Topmass.Core.Repository;

namespace Topmass.Recruiter.Repository
{
    public class RecruiterInfoRepository : RepositoryBase<RecruiterInfoModel>, IRecruiterInfoRepository
    {
        public RecruiterInfoRepository(IConfiguration configuration) : base(configuration)
        {
            tableName = "RecruiterInfo";
        }


    }
}
