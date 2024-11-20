namespace Topmass.Notification.Repository.Model
{
    public class NotificationRepAdd
    {
        public int HandleBy { get; set; }
        public string Title { get; set; }
        public int RelId { get; set; }
        public string UserName { get; set; }
        public string LableText { get; set; }
        public int TypeInfo { get; set; }
        public string Content { get; set; }
        public string LinkFile { get; set; }

        public int Source { get; set; }
        public NotificationRepAdd()
        {
            Source = 2;
        }


    }

    public enum NotificationType
    {
        SYSTEM = 1
    }


}
