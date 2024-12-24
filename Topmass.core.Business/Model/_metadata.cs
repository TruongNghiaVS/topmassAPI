namespace Topmass.core.Business.Model
{
    public class MetaDataRequest
    {
        public string KeyScreen { get; set; }
        public MetaDataRequest()
        {
            KeyScreen = "Home";
        }
    }

    public class MetaDataReponse
    {
        public string MetaTitle { get; set; }
        public string MetaDes { get; set; }
        public string MetaImage { get; set; }
        public string MetaAuthor { get; set; }
        public string MetaKeyWord { get; set; }

    }

    public class SlugWithRouterReponse
    {
        public string Slug { get; set; }
    }
}
