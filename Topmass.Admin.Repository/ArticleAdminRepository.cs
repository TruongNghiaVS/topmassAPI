using Microsoft.Extensions.Configuration;
using Topmass.Admin.Repository;
using Topmass.Core.Model;
using Topmass.Core.Repository;

namespace Topmass.Campagn.Repository
{
    public partial class ArticleAdminRepository : RepositoryBase<ArticleModel>, IArticleAdminRepository
    {
        public ArticleAdminRepository(IConfiguration configuration) : base(configuration)
        {

            tableName = "Articles";
        }

        public async Task<ArticleAdminReponse> GetAll(ArticleAdminRequest request)

        {
            var reponse = new ArticleAdminReponse();
            reponse.Page = request.Page;
            reponse.Limit = request.Limit;
            var dataResult = await ExecutePro<ArtileIndexModel>("sp_adminArticleGetAll", request);
            if (dataResult == null)
            {
                reponse.Data = new List<ArtileIndexModel>();
            }
            else
            {
                reponse.Data = dataResult.ToList();
            }
            return reponse;

        }

        public async Task<ArticleDetailReponse> GetDetail(int id)

        {
            var reponse = new ArticleDetailReponse();

            var dataResult = await GetById(id);
            if (dataResult == null)
            {
                reponse.Data = new ArticleModel() { Id = -1 };
            }
            else
            {
                reponse.Data = dataResult;
            }
            return reponse;

        }

        public async Task<ArticleModel> CheckSlug(string slug)

        {

            var dataResult = await FindOneByStatementSql<ArticleModel>("select top 1 * from Articles where slug = @slug",
                new
                {
                    slug = slug
                });
            if (dataResult == null)
            {
                return null;
            }
            else
            {
                return dataResult;
            }

        }
        public async Task<bool> DeleteArticle(int id)
        {

            return await DeleteReal(id, 1);

        }

        public async Task<bool> AddorUpdateArticle(ArticleModel itemInsert)
        {

            return await AddOrUPdate(itemInsert);
        }
    }

}
