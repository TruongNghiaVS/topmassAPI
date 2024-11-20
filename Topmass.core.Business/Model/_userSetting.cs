namespace Topmass.core.Business
{



    public class JobSettingRequest
    {

        public int? Gender { get; set; }
        public string? Position { get; set; }
        public string Field { get; set; }

        public string Skill { get; set; }
        public string Experience { get; set; }
        public string? Salary { get; set; }
        public string? LocationAddress { get; set; }

        public int RelId { get; set; }

        public int UserId { get; set; }
    }



    public class JobSettingReponse
    {
        public bool Data { get; set; }
    }


    public class GetJobSettingReponse
    {

        public dynamic Data { get; set; }
    }




}
