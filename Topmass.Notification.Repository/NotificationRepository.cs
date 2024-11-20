using Microsoft.Extensions.Configuration;
using Topmass.Core.Repository.Notification;
using Topmass.Notification.Repository;
using Topmass.Notification.Repository.Model;

namespace Topmass.Core.Repository
{
    public partial class NotificationRepository : RepositoryBase<NotificationContentModel>, INotificationRepository
    {
        public NotificationRepository(IConfiguration configuration) : base(configuration)
        {
            tableName = "Notification";
        }

        public async Task<bool> PushNoti(NotificationRepAdd request)
        {
            return await this.AddOrUPdate(new NotificationContentModel()
            {
                Content = request.Content,
                CreateAt = DateTime.Now,
                CreatedBy = request.HandleBy,
                Deleted = false,
                Status = 0,
                LinkFile = request.LinkFile,
                LableText = request.LableText,
                Title = request.Title,
                UpdatedBy = request.HandleBy,
                UpdateAt = DateTime.Now,
                TypeInfo = request.TypeInfo,
                RelId = request.RelId,
                UserName = request.UserName,
                Source = request.Source
            });
        }



    }
}
