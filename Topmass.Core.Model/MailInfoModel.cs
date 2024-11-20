namespace Topmass.Core.Model
{
    public class MailInfoModel : BaseModel
    {

        public string MailTo { get; set; }

        public string Subject { get; set; }

        public string Content { get; set; }

        public int TypeContent { get; set; }


    }
}
