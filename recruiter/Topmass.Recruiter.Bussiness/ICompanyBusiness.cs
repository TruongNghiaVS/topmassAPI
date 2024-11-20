using TopMass.Core.Result;

namespace Topmass.Recruiter.Bussiness

{
    public partial interface ICompanyBusiness
    {

        public Task<BaseResult> GetAllPartner();


    }
}
