using Microsoft.Extensions.DependencyInjection;
using Topmass.Business.Regional;


namespace Topmass.core.Business
{
    public static class DependencyRegister
    {
        public static void ConfigLocationBusiness(this IServiceCollection services)
        {
            services.AddSingleton<IRegionalBusiness, RegionalBusiness>();
        }
    }
}
