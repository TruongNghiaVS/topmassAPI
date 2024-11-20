namespace Topmass.Notification.Business
{
    public class NotificationPushRequest
    {
        public string Title { get; set; }
        public int RelId { get; set; }
        public string UserName { get; set; }
        public int UserId { get; set; }
        public string LableText { get; set; }
        public int TypeInfo { get; set; }
        public string Content { get; set; }
        public string LinkFile { get; set; }
        public int Source { get; set; }
        public NotificationPushRequest()
        {
            Source = 0;
        }

    }


    public class NotificationPushJobRequest
    {
        public string Title { get; set; }
        public int RelId { get; set; }
        public int UserId { get; set; }
        public int JobId { get; set; }
        public string LableText { get; set; }
        public int TypeInfo { get; set; }
        public string Content { get; set; }
        public string LinkFile { get; set; }
        public int Source { get; set; }
        public NotificationPushJobRequest()
        {
            Source = 0;
        }

    }

    public class GetallNotificationReponse
    {

        public List<NotificationItemDisplay> Data { get; set; }

        public GetallNotificationReponse()
        {
            Data = new List<NotificationItemDisplay>();
        }
    }

    public class GetallNotificationPushRequest
    {
        public int RelId { get; set; }

        public string UserName { get; set; }

        public int? Status { get; set; }

        public int? Source { get; set; }
        public GetallNotificationPushRequest()
        {
            Source = 2;
        }

    }
}
