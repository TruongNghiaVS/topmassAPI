using Topmass.core.Business.Model;

namespace Topmass.core.Business
{
    public partial interface ICandidateBusiness
    {
        public Task<UserResgisterResult> RegisterUser(CandidateRegisterRequest request);

        public Task<BaseResult> GetInfo(CandidateInfoRequest requestInfo);

        public Task<BaseResult> UpdateInfoCandidate(CandidateInfoUpdate requestInfo);


        public Task<BaseResult> ChangePassword(string password, int userId);
    }
}
