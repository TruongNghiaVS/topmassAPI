namespace Topmass.Web.Repository.Model
{

    public class BaseReponse
    {
        //public bool Success { get; set; }
        public IEnumerable<object> Data { get; set; }

    }
    public class GetAllByCategoryReponse : BaseReponse
    {


    }

    public partial class GetAllArticleReponse : BaseReponse
    {

    }

    public partial class GetAllByCategoryIdReponse : BaseReponse
    {

    }

}
