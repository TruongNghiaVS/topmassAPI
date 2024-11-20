namespace TopMass.Web.Business.Model
{

    public class PageContentResult : PageSeoResult
    {
        public string? Content { get; set; }

    }

    public class PageSeoResult
    {
        public string? TitlePage { get; set; }

        public string? Description { get; set; }
        public string? KeyWord { get; set; }

        public string Image { get; set; }
        public string PageSlug { get; set; }
    }

    public class CustomerContactReponse
    {


    }
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
}
