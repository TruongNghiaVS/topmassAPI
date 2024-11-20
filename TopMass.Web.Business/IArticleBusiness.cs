using Topmass.Web.Business;
using Topmass.Web.Repository.Model;
using TopMass.Web.Business.Model;

namespace TopMass.Web.Business
{
    public interface IArticleBusiness
    {
        public Task<ArticleResult> AddOrUpdate(ArticleRequestAdd article);
        public Task<ArticleResult> Delete(DeleteRequest article);
        public Task<GetAllArticleReponse> GetAll(ArticleReqest request);

        public Task<GetAllArticleReponse> GetAllShort(ArticleReqest request);
        public Task<GetAllByCategoryReponse> GetAllCatagroy();

        public Task<GetAllByCategoryReponse> GetAllBlogsCategory();


    }
}
