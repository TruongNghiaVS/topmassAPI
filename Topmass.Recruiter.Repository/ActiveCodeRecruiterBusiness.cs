

using Microsoft.Extensions.Configuration;
using Topmass.Core.Model;
using Topmass.Core.Repository;

namespace Topmass.Recruiter.Repository
{
    public class ActiveCodeRecruiterRepository : RepositoryBase<ActiveCodeRecruiter>, IActiveCodeRecruiterRepository
    {
        public ActiveCodeRecruiterRepository(IConfiguration configuration) : base(configuration)
        {
            tableName = "ActiveCodeRecruiter";
        }

    }
}
