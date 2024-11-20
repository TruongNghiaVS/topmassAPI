using Microsoft.Extensions.Configuration;
using Topmass.Core.Model.Profile;

namespace Topmass.Core.Repository
{
    public partial class JobSettingUserRepository : RepositoryBase<JobSettingUser>, IJobSettingUserRepository
    {
        public JobSettingUserRepository(IConfiguration configuration) : base(configuration)
        {
            tableName = "UserJobSetting";
        }




    }
}
