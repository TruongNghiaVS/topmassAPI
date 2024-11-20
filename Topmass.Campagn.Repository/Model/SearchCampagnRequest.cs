namespace Topmass.Campagn.Repository.Model
{
    public class SearchCampagnRequest
    {
        public DateTime? From { get; set; }

        public DateTime? To { get; set; }

        public int? Status { get; set; }

        public string Email { get; set; }

        public SearchCampagnRequest()
        {
            Status = 0;
        }
    }


    public class SearchJobByCampagn
    {
        public DateTime? From { get; set; }

        public DateTime? To { get; set; }

        public int? Status { get; set; }

        public string Email { get; set; }

        public int CampagnId { get; set; }


        public SearchJobByCampagn()
        {
            Status = 0;
        }
    }
}
