namespace Topmass.Core.Model
{
    public class ArticleModel : BaseModel
    {
        public string Title { get; set; }

        public string CoverImage { get; set; }

        public string KeyWord { get; set; }

        public string Content { get; set; }

        public string ShortDes { get; set; }

        public string linked { get; set; }

        public string Slug { get; set; }
    }

    public class CategoryAritcle : BaseModel
    {

        public string Title { get; set; }
        public string? CoverImage { get; set; }
        public string? KeyWord { get; set; }
        public string? ShortDes { get; set; }
        public string? Slug { get; set; }



    }
}
