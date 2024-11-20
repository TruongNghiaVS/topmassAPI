using Microsoft.Extensions.DependencyInjection;
using Topmass.Core.Repository;
using Topmass.Notification.Repository;

namespace Topmass.CV.Repository
{
    public static class DependencyRegister
    {
        public static void ConfigRepCV(this IServiceCollection services)
        {
            services.ConfigRep();
            services.AddSingleton<ICVRepository, CVRepository>();
            services.AddSingleton<INotificationRepository, NotificationRepository>();


        }

    }
}
