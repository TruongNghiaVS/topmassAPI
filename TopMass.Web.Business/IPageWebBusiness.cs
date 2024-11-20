using TopMass.Web.Business.Model;

namespace TopMass.Web.Business
{
    public interface IPageWebBusiness
    {
        public Task<dynamic> AddCustomerRequest(ContactCustomerRequest article);




    }
}
