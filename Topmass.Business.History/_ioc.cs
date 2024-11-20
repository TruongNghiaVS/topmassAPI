using Microsoft.Extensions.DependencyInjection;

namespace Topmass.Business.History
{
    public static class DependencyRegister
    {
        public static void ConfigHistoryBusiness(this IServiceCollection services)
        {
            services.AddSingleton<IHistoryBussiness, HistoryBussiness>();


        }
    }
}
