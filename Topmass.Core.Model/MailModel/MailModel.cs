namespace Topmass.Core.Model.MailModel
{
    public class MailModel : BaseModel
    {
        public string? EmailTo { get; set; }
        public string? Content { get; set; }
        public string? TitleMail { get; set; }
        public DateTime? TimeBusiness { get; set; }
        public MailModel()
        {
        }
    }
}
