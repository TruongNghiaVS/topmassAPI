using Topmass.Core.Repository;
using Topmass.Recruiter.Bussiness.Model;
using TopMass.Core.Result;

namespace Topmass.Recruiter.Bussiness
{
    public partial class CompanyBusiness : ICompanyBusiness
    {


        private readonly ICompanyInfoRepository _companyInfoRepository;

        public CompanyBusiness(
        ICompanyInfoRepository companyInfoRepository

            )
        {

            _companyInfoRepository = companyInfoRepository;
        }

        public async Task<BaseResult> GetAllPartner()
        {
            var reponse = new BaseResult();

            var allData = await _companyInfoRepository.ExecuteSqlProcerduceToList
                <CompanyItemDisplay>("sql_getAllPartner",
                new
                {

                }
            );
            reponse.Data = allData;
            return reponse;

        }

    }
}
