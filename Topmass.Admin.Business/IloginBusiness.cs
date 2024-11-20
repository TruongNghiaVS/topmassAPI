using Topmass.Core.Model.Admin;

namespace Topmass.Admin.Business
{
    public interface IloginBusiness
    {
        public Task<Employer> Login(string userName, string password);

    }
}
