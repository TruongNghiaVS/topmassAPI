using Topmass.Admin.Repository;
using Topmass.Core.Model.Admin;

namespace Topmass.Admin.Business
{
    public class LoginBusiness : BaseBusiness, IloginBusiness
    {
        public LoginBusiness(IAdminRepository _adminRepository) : base(_adminRepository)
        {

        }
        public async Task<Employer> Login(string userName, string password)
        {
            return await adminRepository.EmployeeeRepository.Login(userName, password);

        }
    }
}
