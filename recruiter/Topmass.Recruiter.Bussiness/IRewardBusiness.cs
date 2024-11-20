using TopMass.Core.Result;

namespace Topmass.Recruiter.Bussiness

{
    public partial interface IRewardBusiness
    {
        public Task<BaseResult> ExchangePointToOpenCV(int
            searchId, int point, int userId, int? campaignId = -1);


    }
}
