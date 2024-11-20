using Microsoft.Extensions.DependencyInjection;
using Topmass.CV.Repository;

namespace Topmass.CV.Business
{
    public static class DependencyRegister
    {
        public static void ConfigCVBusiness(this IServiceCollection services)
        {
            services.AddSingleton<ICVUtilities, CVUtilities>();
            services.AddSingleton<ICVBusiness, CVBusiness>();
            services.AddSingleton<ICVUserBusiness, CVUserBusiness>();
            services.AddSingleton<IProfileCVBusiness, ProfileCVBusiness>();
            services.AddSingleton<ISearchCVBusiness, SearchCVBusiness>();
            services.ConfigRepCV();

        }

    }
}
