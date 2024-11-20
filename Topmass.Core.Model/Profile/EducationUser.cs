namespace Topmass.Core.Model.Profile
{
    public class EducationUser : BaseModel
    {
        public string? SchoolName { get; set; }

        public string? Major { get; set; }

        public string Position { get; set; }


        public string Rank { get; set; }


        public string? FromMonth { get; set; }

        public string? FromYear { get; set; }

        public string? ToMonth { get; set; }

        public string? ToYear { get; set; }

        public bool? IsEnd { get; set; }

        public string? Introduction { get; set; }

        public string? LinkFile { get; set; }

        public int RelId { get; set; }

    }

}
