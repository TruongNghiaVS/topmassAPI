

using TopMass.Core.Result;

namespace Topmass.Web.Business
{
    public interface ICustomerContactBusiness
    {
        public Task<BaseResult> AddRequest(string name,
            string email,
            string phone,
            string title,
            string content,
            int typeData = 0
            );


    }
}
