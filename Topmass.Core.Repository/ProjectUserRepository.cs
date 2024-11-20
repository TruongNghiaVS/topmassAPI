using Microsoft.Extensions.Configuration;
using Topmass.Core.Model.Profile;

namespace Topmass.Core.Repository
{
    public partial class ProjectUserRepository : RepositoryBase<ProjectUser>, IProjectUserRepository
    {
        public ProjectUserRepository(IConfiguration configuration) : base(configuration)
        {
            tableName = "ProjectUser";
        }


    }
}
