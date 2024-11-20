


using Microsoft.Extensions.DependencyInjection;
using Topmass.Notification.Repository;

namespace Topmass.Notification.Business
{
    public static class DependencyRegister
    {
        public static void ConfigNotificationBusiness(this IServiceCollection services)
        {
            services.ConfigNotificationRep();

            services.AddSingleton<INotificationBussiness, NotificationBussiness>();
        }
    }
}
