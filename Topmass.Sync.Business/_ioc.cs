using Microsoft.Extensions.DependencyInjection;
using Topmass.core.Business;
using Topmass.Sync.Business;
using Topmass.Sync.Repository;

namespace Topmass.Sync.Busines
{
    public static class DependencyRegister
    {

        public static void ConfigSyncBusiness(this IServiceCollection services)
        {

            services.ConfigBusiness();
            services.ConfigSyncRepository();
            services.ConfigLocationBusiness();
            services.AddSingleton<ISyncBusiness, SyncBusiness>();

        }
    }
}
