using Topmass.Recruiter.Bussiness.Model;
using TopMass.Core.Result;

namespace Topmass.Recruiter.Bussiness

{
    public partial interface ISupportBusiness
    {
        public Task<BaseResult> AddTicket(TicketItemAdd request);


    }
}
