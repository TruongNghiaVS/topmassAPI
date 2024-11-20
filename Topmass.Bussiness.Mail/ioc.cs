using Microsoft.Extensions.DependencyInjection;
using Topmass.Mail.Repository;
using Topmass.Recruiter.Repository;

namespace Topmass.Bussiness.Mail
{
    public static class DependencyRegister
    {
        public static void ConfigMailBusiness(this IServiceCollection services)
        {
            services.ConfigMailRep();
            services.ConfigRecruiterRep();
            services.AddSingleton<IMailBussiness, MailBussiness>();
            services.AddSingleton<IRecruitmentMailBussiness, RecruitmentMailBussiness>();
            services.AddSingleton<IMailJobBissness, MailJobBissness>();
        }
    }
}
