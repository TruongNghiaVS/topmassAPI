using Topmass.Core.Model;
using Topmass.Core.Repository;
using Topmass.Web.Repository.Model;

namespace Topmass.Web.Repository
{

    public partial interface IArticleRepository : IBaseRepository<ArticleModel>
    {
        public Task<GetAllByCategoryReponse> GetAllCategory();
        public Task<GetAllArticleReponse> GetAll(ArticleFilter request);
        public Task<GetAllArticleReponse> GetAllShort(ArticleFilter request);

        public Task<GetAllByCategoryReponse> GetAllBlogsCategory();


    }

    public partial interface ICategoryAritcleRepository : IBaseRepository<CategoryAritcle>
    {


    }

}
