namespace Topmass.core.Business
{
    public class ExperienceUserInfoReponse
    {

        public bool IsSave { get; set; }

        public ExperienceUserInfoReponse()
        {
            IsSave = true;
        }

    }

    public class ExperienceUserInfoUpdateReponse
    {


    }

    public class ExperienceUserInfoDeleteRequest
    {
        public int Id { get; set; }


    }

    public class ExperienceUserInfoUpdateRequest : ExperienceUserInfoRequest
    {

        public int Id { get; set; }
    }

    public class ExperienceUserInfoRequest
    {

        public int UserId { get; set; }
        public string? CompanyName { get; set; }

        public string? Major { get; set; }

        public string Position { get; set; }

        public string? DataInput { get; set; }

        public string? FromMonth { get; set; }

        public string? FromYear { get; set; }

        public string? ToMonth { get; set; }

        public string? ToYear { get; set; }

        public bool? IsEnd { get; set; }

        public string? Introduction { get; set; }

        public string? LinkFile { get; set; }
    }



    public class ExperienceGetAllResult
    {
        public List<ExperienceDisplayItem> Data { get; set; }

    }






}
