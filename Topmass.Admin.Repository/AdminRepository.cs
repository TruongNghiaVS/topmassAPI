using Microsoft.Extensions.Configuration;
using Topmass.Campagn.Repository;
using Topmass.Core.Repository;

namespace Topmass.Admin.Repository
{
    public partial class AdminRepository : BaseDataAccess, IAdminRepository
    {
        public ICompanyInfoRepository CompanyInfoRepository { get; set; }
        public INTDRepository NTDRepository { get; set; }
        public IJobAdminRepository JobAdminRep { get; set; }
        public IEmployeeeRepository EmployeeeRepository { get; set; }
        public IForgetPasswordRepository ForgetPasswordRepository { get; set; }
        public IMasterDataRepository MasterDataRepository { get; set; }
        public AdminRepository(IConfiguration configuration,
            ICompanyInfoRepository _companyInfoRepository,
            INTDRepository _ntdRepository,
            IEmployeeeRepository employeeeRepository,
            IMasterDataRepository masterDataRepository,
            IForgetPasswordRepository forgetPasswordRepository,
            IJobAdminRepository jobAdminRepository
            ) : base(configuration)
        {
            NTDRepository = _ntdRepository;
            CompanyInfoRepository = _companyInfoRepository;
            EmployeeeRepository = employeeeRepository;
            MasterDataRepository = masterDataRepository;
            JobAdminRep = jobAdminRepository;
            ForgetPasswordRepository = forgetPasswordRepository;
        }

    }
}
