using Topmass.Core.Model;

namespace Topmass.Admin.Repository
{

    public class NTDRequest : BaseRequest
    {
        public NTDRequest()
        {

        }
    }

    public class ArticleAdminRequest : BaseRequest
    {
        public int CategoryId { get; set; }

        public int Page { get; set; }
        public int Limit { get; set; }

        public ArticleAdminRequest()
        {
            Page = 1;
            Limit = 100;
        }
    }


    public class ArticleAdminReponse
    {

        public List<ArtileIndexModel> Data { get; set; }
        public int Total { get; set; }
        public int Page { get; set; }

        public int Limit { get; set; }
        public ArticleAdminReponse()
        {

        }
    }

    public class ArticleDetailReponse
    {

        public ArticleModel Data { get; set; }

        public dynamic DataExtra { get; set; }

        public ArticleDetailReponse()
        {
            Data = new ArticleModel()
            {
                Id = -1
            };
        }
    }


    public class NTDBasicInfoReponse
    {

        public string FullName { get; set; }
        public int Gender { get; set; }
        public string Phone { get; set; }

        public NTDBasicInfoReponse()
        {

        }
    }
}
