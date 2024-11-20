using Topmass.Notification.Repository;
using Topmass.Notification.Repository.Model;

namespace Topmass.Notification.Business
{
    public class NotificationBussiness : INotificationBussiness
    {
        private INotificationRepository _notificationRepository;

        public NotificationBussiness(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<GetallNotificationReponse> GetAll(GetallNotificationPushRequest request)
        {
            var result = await _notificationRepository.ExecuteSqlProcerduceToList<NotificationItemDisplay>("sp_getAllNotification",
                new
                {
                    request.UserName,
                    request.Status
                });
            var reponse = new GetallNotificationReponse()
            {
                Data = result
            };
            return reponse;
        }
        public async Task<dynamic> GetDetailById(int Id)
        {
            return await _notificationRepository.GetById(Id);
        }

        public async Task<dynamic> UpdateStatus(int Id, int status)
        {
            var notificationUpdate = await _notificationRepository.GetById(Id);
            if (notificationUpdate != null)
            {
                notificationUpdate.Status = status;
                return await _notificationRepository.AddOrUPdate(notificationUpdate);
            }
            return true;
        }
        public async Task<bool> PushNotification(NotificationPushRequest requestAdd)
        {
            var lableText = "Thông báo";
            await _notificationRepository.PushNoti(new NotificationRepAdd
            {
                Content = requestAdd.Content,
                HandleBy = requestAdd.UserId,
                LableText = lableText,
                LinkFile = requestAdd.LinkFile,
                RelId = requestAdd.RelId,
                Title = requestAdd.Title,
                TypeInfo = requestAdd.TypeInfo,
                UserName = requestAdd.UserName,
                Source = requestAdd.Source
            });
            return true;
        }
    }
}
