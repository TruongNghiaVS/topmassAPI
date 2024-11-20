using Topmass.Core.Model;

namespace Topmass.Admin.Repository
{
    public interface IArticleAdminRepository
    {
        public Task<ArticleAdminReponse> GetAll(ArticleAdminRequest request);

        public Task<ArticleDetailReponse> GetDetail(int id);
     
        Task<bool> DeleteArticle(int id);

        Task<bool> AddorUpdateArticle(ArticleModel itemInsert);
    }
}
