namespace Topmass.Web.Repository.Model
{
    public class ArticleFilter
    {
        public int? CategoryId { get; set; }

        public int? Status { get; set; }
        public string? SlugCategory { get; set; }

        public string? Slug { get; set; }

        public int? Limit { get; set; }

        public ArticleFilter()
        {
            Limit = 30;
        }
    }

}
