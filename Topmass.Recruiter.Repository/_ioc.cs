using Microsoft.Extensions.DependencyInjection;
using Topmass.Core.Repository;


namespace Topmass.Recruiter.Repository
{
    public static class DependencyRegister
    {

        public static void ConfigRecruiterRep(this IServiceCollection services)
        {
            services.ConfigRep();
            services.AddSingleton<IRecruiterRepository, RecruiterRepository>();
            services.AddSingleton<IActiveCodeRecruiterRepository, ActiveCodeRecruiterRepository>();
            services.AddSingleton<IRecruiterInfoRepository, RecruiterInfoRepository>();
            services.AddSingleton<IBusinessLicenseRepository, BusinessLicenseRepository>();
            services.AddSingleton<IBusinessLicenseLogRepository, BusinessLicenseLogRepository>();
            services.AddSingleton<IRewardTransactionRepository, RewardTransactionRepository>();
            services.AddSingleton<IExchangeCVRepository, ExchangeCVRepository>();
            services.AddSingleton<IExchangeCVDetailRepository, ExchangeCVDetailRepository>();

        }
    }
}
