namespace Topmass.Core.Model.Campagn
{
    public class JobItemModel : BaseModel
    {
        public string Name { get; set; }
        public int? Campagn { get; set; }
        public int RelId { get; set; }
        public int Package { get; set; }

        public string Slug { get; set; }

    }

}
