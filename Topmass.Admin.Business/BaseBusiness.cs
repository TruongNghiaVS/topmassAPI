using Topmass.Admin.Repository;

namespace Topmass.Admin.Business
{
    public class BaseBusiness : IBaseBusiness
    {

        protected readonly IAdminRepository adminRepository;


        public BaseBusiness(IAdminRepository _adminRepository)
        {
            adminRepository = _adminRepository;
        }

    }
}
