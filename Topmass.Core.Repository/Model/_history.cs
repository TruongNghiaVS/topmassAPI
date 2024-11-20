namespace Topmass.Core.Repository.Model
{
    public class GetAllHistoryRequest

    {
        public int UserId { get; set; }

        public int Source { get; set; }

        public int Typedata { get; set; }

        public DateTime? From { get; set; }
        public DateTime? To { get; set; }

        public GetAllHistoryRequest()
        {
            UserId = -1; Source = 2; Typedata = 1;
        }
    }
}
