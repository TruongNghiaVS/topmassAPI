using Microsoft.Extensions.Configuration;
using Topmass.Core.Model.Profile;

namespace Topmass.Core.Repository
{
    public partial class ExperienceRepository : RepositoryBase<ExperienceUser>, IExperienceUserRepository
    {
        public ExperienceRepository(IConfiguration configuration) : base(configuration)
        {
            tableName = "ExperienceUser";
        }


    }
}
