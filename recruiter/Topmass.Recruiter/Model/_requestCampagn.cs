namespace Topmass.Recruiter.Model
{
    public class CampagnSearchRequestFilter
    {
        public DateTime? From { get; set; }

        public DateTime? To { get; set; }

        public int? Status { get; set; }
        public int? Page { get; set; }

        public int? Limit { get; set; }

        public CampagnSearchRequestFilter()
        {
            Limit = 1;
            Page = 10;
        }

    }
    public class CampagnItemRequestAdd
    {
        public string Name { get; set; }
        public DateTime? From { get; set; }

        public DateTime? To { get; set; }




    }


    public class CampagnItemRequestUpdate
    {
        public string Name { get; set; }
        public DateTime? From { get; set; }

        public DateTime? To { get; set; }



        public int? Status { get; set; }

        public int? IdUpdate { get; set; }


    }

    public class CampagnItemStatusRequestUpdate
    {

        public int? IdUpdate { get; set; }
        public int? Status { get; set; }



    }


    public class FilterCampagnRequest
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }

        public int? Status { get; set; }

    }


    public class GetAllJobOfCampagnRequest
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public int? Status { get; set; }

        public int ModeDisplay { get; set; }

    }
}
