namespace Topmass.core.Business
{
    public class EducationUserInfoReponse
    {

        public bool IsSave { get; set; }

        public EducationUserInfoReponse()
        {
            IsSave = true;
        }

    }

    public class EducationUserInfoUpdateReponse
    {


    }

    public class EducationUserInfoDeleteRequest
    {
        public int Id { get; set; }


    }

    public class EducationUserInfoUpdateRequest : EducationUserInfoRequest
    {

        public int Id { get; set; }
    }

    public class EducationUserInfoRequest
    {

        public int UserId { get; set; }
        public string? SchoolName { get; set; }

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

        public string? Rank { get; set; }
    }



    public class EducationGetAllResult
    {

        public List<EducationDisplayItem> Data { get; set; }


    }



}
