
using Microsoft.Extensions.DependencyInjection;

namespace Topmass.Mail.Repository
{
    public static class DependencyRegister
    {
        public static void ConfigMailRep(this IServiceCollection services)
        {
            services.AddSingleton<IMailRepository, MailRepository>();
        }
    }
}
