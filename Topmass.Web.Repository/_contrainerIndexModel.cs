namespace Topmass.Web.Repository
{
    public class BaseIndexModel
    {
        public int Status { get; set; }

        public int Id { get; set; }
        public DateTime CreateAt { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdateAt { get; set; }
        public int UpdatedBy { get; set; }

    }

    public class ArticleIndexModel : BaseIndexModel
    {
        public string? Title { get; set; }

        protected string? CoverImage { get; set; }

        public string FullSlug
        {
            get
            {
                if (string.IsNullOrEmpty(CategorySlug))
                {
                    return Slug;
                }
                return CategorySlug + "/" + Slug;
            }
        }

        protected string CategorySlug { get; set; }


        public string CoverFullLink
        {
            get
            {

                if (string.IsNullOrEmpty(CoverImage))
                {
                    return "";
                }
                return "https://www.cdn.topmass.vn/static/blog/" + CoverImage;
            }
        }

        public string? KeyWord { get; set; }

        public string? Content { get; set; }

        public string? ShortDes { get; set; }

        public string? CategoryName { get; set; }

        public string Linked { get; set; }

        public string? Slug { get; set; }


    }

    public class ArticleShortInfoIndexModel : BaseIndexModel
    {
        public string? Title { get; set; }

        protected string? CoverImage { get; set; }

        public string CoverFullLink
        {
            get
            {

                if (string.IsNullOrEmpty(CoverImage))
                {
                    return "";
                }
                return "https://www.cdn.topmass.vn/static/blog/" + CoverImage;
            }
        }

        public string? KeyWord { get; set; }

        public string? Content { get; set; }

        public string? ShortDes { get; set; }

        public string? CategoryName { get; set; }

        public string Linked { get; set; }

        public string? Slug { get; set; }


    }

    public class CategoryAritcleIndexModel
    {
        public string Title { get; set; }
        public string? Slug { get; set; }

        public int Id { get; set; }

    }

    public class CategoryAritcleIndex2Model
    {
        public string Title { get; set; }
        public string? Slug { get; set; }

        public int Id { get; set; }

        public List<ArticleShortInfoIndexModel> DataBlog { get; set; }

        public CategoryAritcleIndex2Model()
        {
            DataBlog = new List<ArticleShortInfoIndexModel>();
        }

    }


}
