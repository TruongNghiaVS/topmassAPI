using Microsoft.Extensions.DependencyInjection;
using Topmass.Core.Repository;

namespace Topmass.Notification.Repository
{
    public static class DependencyRegister
    {

        public static void ConfigNotificationRep(this IServiceCollection services)
        {

            services.AddSingleton<INotificationRepository, NotificationRepository>();

        }
    }
}
