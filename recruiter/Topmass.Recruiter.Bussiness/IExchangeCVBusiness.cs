using Topmass.Recruiter.Bussiness.Model;
using TopMass.Core.Result;

namespace Topmass.Recruiter.Bussiness

{
    public partial interface IExchangeCVBusiness
    {
        public Task<BaseResult> AddExchange(ExchangeCVRequestAdd request);
        public Task<BaseResult> GetHistory(int Status, int userId);
        public Task<BaseResult> GetDetail(int id);
        public Task<bool> CancelExchange(int id);

    }
}
