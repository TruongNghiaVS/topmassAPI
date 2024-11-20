namespace Topmass.Notification.Business
{
    public interface INotificationBussiness
    {
        public Task<bool> PushNotification(NotificationPushRequest requestAdd);
        public Task<GetallNotificationReponse> GetAll(GetallNotificationPushRequest request);
        public Task<dynamic> GetDetailById(int Id);
        public Task<dynamic> UpdateStatus(int Id, int status);

    }
}
