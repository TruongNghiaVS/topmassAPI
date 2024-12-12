namespace Topmass.Admin.Pages.Model
{

    public class LoginRequest
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public LoginRequest()
        {

        }
    }


    public class ArticleRequestUpdate
    {

        public int? Id { get; set; }

        public string TitleArticle { get; set; }

        public string ShortDesArticle { get; set; }

        public string KeywordArticle { get; set; }

        public string LinkImageArticle { get; set; }

        public string CategryIdLinkArticle { get; set; }

        public string ContentArticle { get; set; }
        public string SlugArticle { get; set; }
    }

}
