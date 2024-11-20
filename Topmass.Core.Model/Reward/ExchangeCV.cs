namespace Topmass.Core.Model.Reward
{
    public class ExchangeCV : BaseModel
    {
        public string Title { get; set; }
        public int Point { get; set; }
        public string Position { get; set; }
        public string Rank { get; set; }
        public string Experience { get; set; }
        public DateTime BusinessTime { get; set; }
        public ExchangeCV()
        {
            Position = "";
            Rank = "";
            Experience = "";
        }
    }
}
