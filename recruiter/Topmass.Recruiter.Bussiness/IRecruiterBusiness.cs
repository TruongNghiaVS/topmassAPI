using Topmass.Recruiter.Bussiness.Model;
using TopMass.Core.Result;

namespace Topmass.Recruiter.Bussiness

{
    public partial interface IRecruiterBusiness
    {
        public Task<UserResgisterResult> RegisterUser(RecruiterRegisterRequest request);
        public Task<BaseResult> GetInfo(RecruiterInfoRequest requestInfo);

        public Task<BaseResult> UpdateInfoRecruiter(RecruiterInfoUpdate requestInfo);
        public Task<BaseResult> UpdateCompanyInfo(CompanyInfoRequestUpdate requestInfo);

        public Task<BaseResult> UpdateBusinessLicense(BusinessLicenseRequestUpdate requestInfo);

        public Task<BaseResult> ChangePassword(string password, int userId);
    }
}
