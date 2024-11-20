using Microsoft.Extensions.Configuration;
using Topmass.Core.Model.Profile;

namespace Topmass.Core.Repository
{
    public partial class CertifyUserRepository : RepositoryBase<CertifyUser>, ICertifyUserRepository
    {
        public CertifyUserRepository(IConfiguration configuration) : base(configuration)
        {
            tableName = "CertifyUser";
        }


    }
}
