using Microsoft.Extensions.Configuration;
using Topmass.Core.Model.Profile;

namespace Topmass.Core.Repository
{
    public partial class EducationUserRepository : RepositoryBase<EducationUser>, IEducationUserRepository
    {
        public EducationUserRepository(IConfiguration configuration) : base(configuration)
        {
            tableName = "educationUser";
        }


    }
}
