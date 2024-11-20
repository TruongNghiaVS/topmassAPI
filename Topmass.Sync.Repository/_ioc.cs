using Microsoft.Extensions.DependencyInjection;
using Topmass.Core.Repository;

namespace Topmass.Sync.Repository
{
    public static class DependencyRegister
    {

        public static void ConfigSyncRepository(this IServiceCollection services)
        {
            services.ConfigRep();

            services.AddSingleton<ISyncDaRepository, SyncDaRepository>();


        }
    }
}
