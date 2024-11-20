namespace topmass.Model
{

    public class InputExperienceUserInfoUpdateRequest : InputExperienceUserRequestAdd
    {
        public int Id { get; set; }
    }
    public class InputExperienceUserRequestAdd
    {
        public string? CompanyName { get; set; }


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





}
