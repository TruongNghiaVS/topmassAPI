using Microsoft.Extensions.Configuration;
using Topmass.Core.Model;

namespace Topmass.Core.Repository
{
    public partial class CompanyFollowModelRepository : RepositoryBase<CompanyFollowModel>, ICompanyFollowModelRepository
    {
        public CompanyFollowModelRepository(IConfiguration configuration) : base(configuration)
        {
            tableName = "CompanyFollow";
        }

    }
}
