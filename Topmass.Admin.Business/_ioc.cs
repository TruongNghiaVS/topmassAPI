using Microsoft.Extensions.DependencyInjection;
using Topmass.Admin.Repository;
using Topmass.Bussiness.Mail;
using Topmass.core.Business;


namespace Topmass.Admin.Business
{
    public static class DependencyRegister
    {
        public static void ConfigAdminBusiness(this IServiceCollection services)
        {
            services.ConfigRepAdmin();
            services.AddSingleton<IloginBusiness, LoginBusiness>();
            services.AddSingleton<INTDBusiness, NTDBusiness>();
            services.AddSingleton<IAdminArticleBusiness, AdminArticleBusiness>();
            services.AddSingleton<IMasterBusiness, MasterDataBusiness>();
            services.AddSingleton<IJobAdminBusiness, JobAdminBusiness>();
            services.AddSingleton<ICandidateAdminBusiness, CandidateAdminBusiness>();
            services.ConfigLocationBusiness();
            services.ConfigMailBusiness();
        }
    }
}
