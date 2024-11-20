using Microsoft.Extensions.Configuration;
using Topmass.Core.Model.Profile;

namespace Topmass.Core.Repository
{
    public partial class ProfileCVUserRepository : RepositoryBase<ProfileCVUser>, IProfileCVUserRepository
    {


        public ProfileCVUserRepository(IConfiguration configuration, IJobRepository jobRepository) : base(configuration)
        {
            tableName = "ProfileCV";

        }


    }
}
