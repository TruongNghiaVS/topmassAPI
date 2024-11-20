namespace Topmass.Core.Model.Reward
{
    public class ExchangeCVDetail : BaseModel
    {
        public int RelId { get; set; }
        public int UserId { get; set; }
        public string Linkfile { get; set; }
        public int Point { get; set; }
        public string Noted { get; set; }
        public DateTime BusinessTime { get; set; }

        public ExchangeCVDetail()
        {
            BusinessTime = DateTime.Now;

        }
    }
}
