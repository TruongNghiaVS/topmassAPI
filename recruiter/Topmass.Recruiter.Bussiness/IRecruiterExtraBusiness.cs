using Topmass.Recruiter.Bussiness.Model;
using TopMass.Core.Result;

namespace Topmass.Recruiter.Bussiness

{
    public partial interface IRecruiterExtraBusiness
    {
        public Task<UserResgisterResult> RegisterUser(RecruiterRegisterRequest request);
        public Task<RecruiterInfoResult> GetInfo(RecruiterInfoRequest requestInfo);

        public Task<BaseResult> UpdateInfoRecruiter(RecruiterInfoUpdate requestInfo);


        public Task<BaseResult> ChangePassword(string password, int userId);
    }
}
