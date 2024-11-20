using Microsoft.Extensions.Configuration;
using Topmass.Core.Model.Page;

namespace Topmass.Core.Repository
{
    public partial class PageContentRepository : RepositoryBase<PageContentModel>, IPageContentRepository
    {
        public PageContentRepository(IConfiguration configuration) : base(configuration)
        {

            tableName = "PageContent";
        }
        public async Task<PageContentModel> GetContentBySlug(string slug, int source = 1)
        {
            var resultItem = await this.FindOneByStatementSql<PageContentModel>("select top 1 * from " + tableName + " where slug = @slug and TypeData =1 and source = @source ",
                new
                {
                    slug = slug,
                    source = source
                });
            return resultItem;
        }

        public async Task<PageContentModel> GetSeoBySlug(string slug, int source = 1)
        {
            var resultItem = await this.FindOneByStatementSql<PageContentModel>("select top 1 * from " + tableName + " where slug = @slug and TypeData =2 and source = @source",
                new
                {
                    slug = slug,
                    source = source
                });
            return resultItem;
        }
    }
}
