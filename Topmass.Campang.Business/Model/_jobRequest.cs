

//namespace Topmass.Campagn.Business.Model
//{
//    public class JobItemAdd
//    {
//        public string Name { get; set; }
//        public int? Campagn { get; set; }

//        public DateTime? From { get; set; }

//        public DateTime? To { get; set; }
//        public int Package { get; set; }
//        public int HandleBy { get; set; }

//        public int? Status { get; set; }

//        public JobItemAdd()
//        {
//            Campagn = -1;

//            Package = -1;
//            Status = 0;
//        }

//    }


//    public class JobItemUpdate
//    {
//        public string Name { get; set; }
//        public DateTime? From { get; set; }

//        public DateTime? To { get; set; }
//        public int Package { get; set; }
//        public int HandleBy { get; set; }
//        public int JobId { get; set; }



//    }

//    public class JobItemStatusUpdate
//    {

//        public int? IdUpdate { get; set; }
//        public int? Status { get; set; }

//        public int HandleBy { get; set; }



//    }


//    public class JobSearchRequest
//    {
//        public int? Userid { get; set; }

//        public int Limit { get; set; }

//        public int Page { get; set; }

//        public string? Email { get; set; }

//        public DateTime? From { get; set; }

//        public DateTime? To { get; set; }


//        public int? CampagnId { get; set; }
//        public int? Status { get; set; }

//        public JobSearchRequest()
//        {
//            Limit = 10;
//            Page = 1;
//        }

//    }

//    public class JobInfoRequest
//    {
//        public int CampagnId { get; set; }

//        public int JobId { get; set; }

//        public int HandleBy { get; set; }

//        public string Slug { get; set; }
//    }

//    public class ViewJobUserAddRequest
//    {
//        public int JobId { get; set; }

//        public int Userid { get; set; }


//    }
//}
