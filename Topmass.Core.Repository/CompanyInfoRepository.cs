using Microsoft.Extensions.Configuration;
using Topmass.Core.Model;


namespace Topmass.Core.Repository
{
    public class CompanyInfoRepository : RepositoryBase<CompanyInfoModel>, ICompanyInfoRepository
    {
        public CompanyInfoRepository(IConfiguration configuration) : base(configuration)
        {
            tableName = "CompanyInfo";
        }


    }
}
