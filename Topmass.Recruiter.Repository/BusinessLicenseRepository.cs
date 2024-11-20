

using Microsoft.Extensions.Configuration;
using Topmass.Core.Model;
using Topmass.Core.Repository;

namespace Topmass.Recruiter.Repository
{
    public class BusinessLicenseRepository : RepositoryBase<BusinessLicenseModel>, IBusinessLicenseRepository
    {
        public BusinessLicenseRepository(IConfiguration configuration) : base(configuration)
        {
            tableName = "BusinessLicense";
        }


    }
}
