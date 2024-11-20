namespace Topmass.Core.Repository.Model
{
    public class RegionalIndexModel
    {

        public int CountJob { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
    }


    public class RegionalIndexDataModel
    {

        public string? Code { get; set; }
        public string? Name { get; set; }
        public string Slug { get; set; }

        public int Datatype { get; set; }

        public string Level1 { get; set; }
        public string Level2 { get; set; }
    }
}
