using Microsoft.Extensions.DependencyInjection;
using Topmass.Campagn.Repository;
using Topmass.Core.Repository;

namespace Topmass.Admin.Repository
{
    public static class DependencyRegister
    {
        public static void ConfigRepAdmin(this IServiceCollection services)
        {
            services.ConfigRep();
            services.AddSingleton<IAdminRepository, AdminRepository>();
            services.AddSingleton<IEmployeeeRepository, EmployerRepository>();
            services.AddSingleton<IArticleAdminRepository, ArticleAdminRepository>();
            services.AddSingleton<INTDRepository, NTDRepository>();
            services.AddSingleton<IMasterDataRepository, MasterDataRepository>();
            services.AddSingleton<IForgetPasswordRepository, ForgetPasswordRepository>();
            services.AddSingleton<IJobAdminRepository, JobAdminRepository>();
            

        }
    }
}
