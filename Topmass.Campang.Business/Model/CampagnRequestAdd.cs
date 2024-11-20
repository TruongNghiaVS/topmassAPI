

namespace Topmass.Campagn.Business.Model
{
    public class CampagnItemAdd
    {
        public string Name { get; set; }
        public DateTime? From { get; set; }

        public DateTime? To { get; set; }

        public int HandleBy { get; set; }

        public string Email { get; set; }

    }


    public class CampagnItemUpdate
    {
        public string Name { get; set; }
        public DateTime? From { get; set; }

        public DateTime? To { get; set; }

        public int HandleBy { get; set; }

        public string Email { get; set; }

        public int? Status { get; set; }

        public int? IdUpdate { get; set; }


    }

    public class CampagnItemStatusUpdate
    {

        public int? IdUpdate { get; set; }
        public int? Status { get; set; }



    }


    public class FilterCampagnRequest
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public string Email { get; set; }
        public int? Status { get; set; }

    }
}
