using Microsoft.Extensions.DependencyInjection;
using Topmass.Campagn.Repository;

namespace Topmass.Campagn.Business
{
    public static class DependencyRegister
    {
        public static void ConfigCampagnBusiness(this IServiceCollection services)
        {
            services.ConfigRepCampagn();
            services.AddSingleton<ICampagnBusiness, CampagnBusiness>();
            //services.AddSingleton<IJobBusiness, JobItemBusiness>();
        }
    }
}
