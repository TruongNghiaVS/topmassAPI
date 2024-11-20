using Topmass.Core.Repository;

namespace Topmass.Recruiter.Bussiness
{
    public partial class CompanyInfoBusiness : ICompanyInfoBusiness
    {
        private readonly ICompanyInfoRepository companyInfoRepository;

        public CompanyInfoBusiness(
             ICompanyInfoRepository _companyInfoRepository
             )
        {
            companyInfoRepository = _companyInfoRepository;
        }

        //public async Task<CompanyInfoResult> GetAll(CompanyInfoRequest request)
        //{

        //    var result = new CompanyInfoResult();





        //    return result;



        //}
    }
}
