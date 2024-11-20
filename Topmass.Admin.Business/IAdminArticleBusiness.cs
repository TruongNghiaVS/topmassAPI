using TopMass.Core.Result;

namespace Topmass.Admin.Business
{
    public interface IAdminArticleBusiness : IBaseBusiness
    {
        public Task<BaseResult> GetAll(SearchArticleRequest request);

        public Task<BaseResult> Detail(int id);

        public Task<bool> Delete(int id);

        public Task<BaseResult> AddorUpdate(ArticleRequestAdd request);
    }
}
