using Microsoft.Extensions.Configuration;
using Topmass.Core.Model.Profile;

namespace Topmass.Core.Repository
{
    public partial class OtherProfileUserRepository : RepositoryBase<OtherProfileUser>, IOtherProfileUserRepository
    {
        public OtherProfileUserRepository(IConfiguration configuration) : base(configuration)
        {
            tableName = "OtherProfileUser";
        }


    }
}
