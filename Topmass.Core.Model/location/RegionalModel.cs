

namespace Topmass.Core.Model.location
{
    public class RegionalModel : BaseModel
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Slug { get; set; }

        public string Level1 { get; set; }

        public string Level2 { get; set; }

        public RegionalModel()
        {


        }
    }


}
