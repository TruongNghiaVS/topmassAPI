namespace Topmass.Campagn.Business.Model
{
    public class CampagnSearchFilter
    {
        public DateTime? From { get; set; }

        public string Email { get; set; }

        public DateTime? To { get; set; }

        public int? Status { get; set; }
        public int HandleBy { get; set; }
    }


    public class SearchJobOfCampagnRequest
    {
        public DateTime? From { get; set; }

        public DateTime? To { get; set; }

        public int? Status { get; set; }

        public int UserId { get; set; }

        public int CampagnId { get; set; }

        public int HandleBy { get; set; }

        public SearchJobOfCampagnRequest()
        {
            Status = 0;
        }
    }



}
