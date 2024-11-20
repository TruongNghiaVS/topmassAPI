using Microsoft.Extensions.DependencyInjection;
using Topmass.Core.Repository;

namespace Topmass.Bussiness.Company
{
    public static class DependencyRegister
    {
        public static void ConfigCompanyBusiness(this IServiceCollection services)
        {
            services.ConfigRep();
            services.AddSingleton<ICompanyBusiness, CompanyBusiness>();

        }
    }

}
