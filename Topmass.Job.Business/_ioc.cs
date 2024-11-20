using Microsoft.Extensions.DependencyInjection;

namespace Topmass.Job.Business
{
    public static class DependencyRegister
    {
        public static void ConfigJobBusiness(this IServiceCollection services)
        {
            services.AddSingleton<IJobBusiness, JobItemBusiness>();
            services.AddSingleton<IJobUtilitiesBusiness, JobUtilitiesBusiness>();

            services.AddSingleton<IJobUserBusiness, JobUserBusiness>();

            services.AddSingleton<IJobSearchBusiness, JobSearchBusiness>();


        }
    }
}
