using Microsoft.Extensions.DependencyInjection;
using Topmass.Core.Repository;

namespace Topmass.Core.Common
{
    public static class DependencyRegister
    {

        public static void ConfigCoreCommon(this IServiceCollection services)
        {
            services.ConfigRep();
            services.AddSingleton<IMasterDataBusiness, MasterDataBusiness>();

        }
    }
}
