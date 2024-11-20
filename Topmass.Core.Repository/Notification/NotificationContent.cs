using Topmass.Core.Model;

namespace Topmass.Core.Repository.Notification
{
    public class NotificationContentModel : BaseModel
    {
        public string Title { get; set; }

        public int UserId { get; set; }

        public int RelId { get; set; }
        public string UserName { get; set; }

        public string LableText { get; set; }

        public int TypeInfo { get; set; }

        public string Content { get; set; }

        public string LinkFile { get; set; }

        public int Source { get; set; }

        public NotificationContentModel()
        {
            Source = 2;
        }



    }
}
