using Topmass.Core.Model.Page;

namespace Topmass.Core.Repository
{
    public partial interface IPageContentRepository : IBaseRepository<PageContentModel>
    {
        public Task<PageContentModel> GetContentBySlug(string slug, int source = 1);
        public Task<PageContentModel> GetSeoBySlug(string slug, int source = 1);
    }
}
