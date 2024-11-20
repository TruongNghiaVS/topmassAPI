using Microsoft.Extensions.DependencyInjection;
using Topmass.Recruiter.Repository;

namespace Topmass.Recruiter.Bussiness
{
    public static class DependencyRegister
    {

        public static void ConfigRecruiterBusiness(this IServiceCollection services)
        {
            services.ConfigRecruiterRep();
            services.AddSingleton<IAuthenBuisiness, AuthenBuisiness>();
            services.AddSingleton<IRecruiterBusiness, RecruiterBusiness>();
            services.AddSingleton<ISupportBusiness, SupportBusiness>();
            services.AddSingleton<IRewardBusiness, RewardBusiness>();
            services.AddSingleton<ICompanyBusiness, CompanyBusiness>();
            services.AddSingleton<IExchangeCVBusiness, ExchangeCVBusiness>();

        }
    }
}
