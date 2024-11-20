namespace topmass.Model
{

    public class InputProjectUserInfoUpdateRequest : InputProjectUserRequestAdd
    {
        public int Id { get; set; }
    }
    public class InputProjectUserRequestAdd
    {

        public int Id { get; set; }


        public string Position { get; set; }

        public string? DataInput { get; set; }

        public string? FromMonth { get; set; }

        public string? FromYear { get; set; }

        public string? ToMonth { get; set; }

        public string? ToYear { get; set; }

        public bool? IsEnd { get; set; }

        public string? Introduction { get; set; }

        public string? LinkFile { get; set; }
        public string? ProjectName { get; set; }

        public string? CustomerName { get; set; }

        public int NumOfMember { get; set; }


        public string Technology { get; set; }

    }






}
