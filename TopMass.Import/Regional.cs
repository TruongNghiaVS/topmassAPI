namespace TopMass.Import
{
    public class Regional
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string Level1 { get; set; }

        public string Level2 { get; set; }

        public string Slug { get; set; }
        public int? Datatype { get; set; }
        public Regional()
        {

            Slug = "";

        }
    }
}
