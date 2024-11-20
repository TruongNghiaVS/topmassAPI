using TopMass.Web.Business.Model;

namespace TopMass.Web.Business
{
    public interface IPageBusiness
    {
        public Task<PageContentResult> GetContentBySlug(string pageSlug, int source = 1);
        public Task<PageSeoResult> GetSeoBySlug(string pageSlug, int source = 1);

    }
}
