namespace Topmass.core.Business
{
    public class EducationDisplayItem

    {

        public int Id { get; set; }
        public string? SchoolName { get; set; }

        public string? Major { get; set; }

        public string Position { get; set; }

        public string? FromMonth { get; set; }

        public string? FromYear { get; set; }

        public string? ToMonth { get; set; }

        public string? ToYear { get; set; }

        public bool? IsEnd { get; set; }

        public string? Introduction { get; set; }

        public string? PositionText { get; set; }
        public string? LinkFile { get; set; }

        public int RelId { get; set; }

        public string? Rank { get; set; }

        public EducationDisplayItem()
        {
            SchoolName = "";
            Major = "";
            Position = "";
            FromMonth = "";
            FromYear = "";
            ToMonth = "";
            ToYear = "";
            IsEnd = false;
            Introduction = "";
            LinkFile = "";


        }
    }


    public class ExperienceDisplayItem


    {

        public int Id { get; set; }
        public string? CompanyName { get; set; }

        public string Position { get; set; }

        public string? FromMonth { get; set; }

        public string? FromYear { get; set; }

        public string? ToMonth { get; set; }

        public string? ToYear { get; set; }

        public bool? IsEnd { get; set; }

        public string? Introduction { get; set; }

        public string? LinkFile { get; set; }

        public int RelId { get; set; }

        public ExperienceDisplayItem()
        {
            CompanyName = "";

            Position = "";
            FromMonth = "";
            FromYear = "";
            ToMonth = "";
            ToYear = "";
            IsEnd = false;
            Introduction = "";
            LinkFile = "";


        }
    }
}
