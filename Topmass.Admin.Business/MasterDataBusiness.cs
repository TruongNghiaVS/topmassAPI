using Topmass.Admin.Repository;
using Topmass.Core.Model.Admin;

namespace Topmass.Admin.Business
{
    public class MasterDataBusiness : BaseBusiness, IMasterBusiness
    {

        public MasterDataBusiness(IAdminRepository _adminRepository)
                                : base(_adminRepository)
        {

        }

        public async Task<dynamic> GetAllDataByType(int typeData)
        {
            var dataall = await adminRepository.MasterDataRepository.GetAllToList();
            var result = dataall.Where(x => x.TypeData == typeData).ToList();
            return result;

        }

        public async Task<Employer> Login(string userName, string password)
        {
            return await adminRepository.EmployeeeRepository.Login(userName, password);

        }
    }
}
