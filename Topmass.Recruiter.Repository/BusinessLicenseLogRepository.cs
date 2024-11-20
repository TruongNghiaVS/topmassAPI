

using Microsoft.Extensions.Configuration;
using Topmass.Core.Model;
using Topmass.Core.Repository;

namespace Topmass.Recruiter.Repository
{
    public class BusinessLicenseLogRepository : RepositoryBase<BusinessLicenseLogModel>, IBusinessLicenseLogRepository
    {
        public BusinessLicenseLogRepository(IConfiguration configuration) : base(configuration)
        {
            tableName = "BusinessLicenseLog";
        }


    }
}
