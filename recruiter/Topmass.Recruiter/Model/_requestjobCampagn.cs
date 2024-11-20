using Topmass.Job.Business.Model;

namespace Topmass.Recruiter.Model
{

    public class JobSearchRequestFilter
    {
        public DateTime? From { get; set; }

        public DateTime? To { get; set; }

        public int? CampaigId { get; set; }
        public int Limt { get; set; }
        public int Page { get; set; }
        public int? Status { get; set; }

        public JobSearchRequestFilter()
        {
            Limt = 10;
            Page = 1;
            CampaigId = -1;
            Status = -1;
        }

    }

    public class JobItemRequestUpdateInfo
    {
        public int JobId { get; set; }
        public string? Name { get; set; }

        public string? Position { get; set; }
        public int? Profession { get; set; }
        public DateTime? Expired_date { get; set; }
        public int? Quantity { get; set; }
        public int? Type_of_work { get; set; }
        public int? Rank { get; set; }

        public int? Experience { get; set; }

        public bool? Aggrement { get; set; }

        public int? Salary_from { get; set; }

        public int? Salary_to { get; set; }

        public string? Type_money { get; set; }

        public int? Gender { get; set; }

        public string? Description { get; set; }

        public string? Requirement { get; set; }


        public string? Benefit { get; set; }

        public string? Skill { get; set; }

        public string? Username { get; set; }

        public string? Phone { get; set; }

        public List<EmailProper>? Emails { get; set; }

        public List<SkillProper>? Skills { get; set; }

        public List<TimeWorking>? Time_working { get; set; }

        public List<LocationsJob>? Locations { get; set; }

    }
    public class JobItemRequestAdd
    {
        public string? Name { get; set; }
        public int? Campaign { get; set; }

        public string? Position { get; set; }
        public int? Profession { get; set; }
        public DateTime? Expired_date { get; set; }
        public int? Quantity { get; set; }
        public int? Type_of_work { get; set; }
        public int? Rank { get; set; }

        public int? Experience { get; set; }

        public bool? Aggrement { get; set; }

        public int? Salary_from { get; set; }

        public int? Salary_to { get; set; }

        public string? Type_money { get; set; }

        public int? Gender { get; set; }

        public string? Description { get; set; }

        public string? Requirement { get; set; }


        public string? Benefit { get; set; }

        public string? Skill { get; set; }

        public string? Username { get; set; }

        public string? Phone { get; set; }

        public List<EmailProper>? Emails { get; set; }

        public List<TimeWorking>? Time_working { get; set; }

        public List<LocationsJob>? Locations { get; set; }

        public List<SkillProper> Skills { get; set; }


    }


    //public class EmailRequestProper
    //{

    //    public string Email { get; set; }
    //}

    //public class TimeWorkingRequest
    //{

    //    public string? Day_from { get; set; }

    //    public string? Day_to { get; set; }

    //    public string? Time_to { get; set; }

    //    public string? Time_from { get; set; }
    //    public TimeWorkingRequest()
    //    {
    //        Day_from = "";
    //        Day_to = "";
    //        Time_to = "";
    //        Time_from = "";
    //    }

    //}


    //public class LocationsRequest
    //{
    //    public string Location { get; set; }
    //    public List<DistrictRequest> Districts { get; set; }

    //}


    //public class DistrictRequest
    //{

    //    public string District { get; set; }

    //    public string Detail_location { get; set; }

    //    public DistrictRequest()
    //    {
    //        District = "";
    //        Detail_location = "";
    //    }
    //}


    public class JobItemRequestUpdate
    {
        public string Name { get; set; }
        public DateTime? From { get; set; }

        public DateTime? To { get; set; }
        public int? Status { get; set; }

        public int? IdUpdate { get; set; }



    }

    public class JobItemStatusRequestUpdate
    {

        public int? IdUpdate { get; set; }
        public int? Status { get; set; }



    }


    public class FilterJobRequest
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }

        public int? Status { get; set; }

    }


    public class JobItemInfoRequest
    {

        public int? JobId { get; set; }




    }

}
