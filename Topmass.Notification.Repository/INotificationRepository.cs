
using Topmass.Core.Repository;
using Topmass.Core.Repository.Notification;
using Topmass.Notification.Repository.Model;

namespace Topmass.Notification.Repository
{
    public partial interface INotificationRepository : IBaseRepository<NotificationContentModel>
    {
        public Task<bool> PushNoti(NotificationRepAdd request);

    }
}
