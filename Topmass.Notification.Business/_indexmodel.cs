namespace Topmass.Notification.Business
{
    public class NotificationItemDisplay
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public int RelId { get; set; }
        public string UserName { get; set; }

        public string LableText { get; set; }

        public int TypeInfo { get; set; }

        public string Content { get; set; }

        public string LinkFile { get; set; }

        public DateTime CreateAt { get; set; }

        public int Status { get; set; }
    }
}
