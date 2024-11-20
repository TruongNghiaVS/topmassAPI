namespace Topmass.Bussiness.Mail
{
    public class MailItem
    {
        public string? MailTo { get; set; }
        public int? TypeTemplate { get; set; }
        public DataMailInfo Data { get; set; }
        public MailItem()
        {
            MailTo = "";
            Data = new DataMailInfo();
        }


    }


    public class DataMailInfo
    {
        public string? Content { get; set; }
        public string? Subject { get; set; }

        public DataMailInfo()
        {
            Content = "";
            Subject = "";
        }

    }
}
