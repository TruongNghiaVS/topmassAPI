using Topmass.Core.Repository;
using TopMass.Web.Business;
using TopMass.Web.Business.Model;


namespace Topmass.Web.Business
{
    public partial class PageBusiness : IPageBusiness
    {
        private readonly IPageContentRepository _repository;
        public PageBusiness(IPageContentRepository _pagecontentReposiotry)
        {

            _repository = _pagecontentReposiotry;
        }
        public async Task<PageContentResult> GetContentBySlug(string pageSlug, int source = 1)
        {
            var content = await _repository.GetContentBySlug(pageSlug, source);
            if (content == null || content.Id < 1)
            {
                return null;
            }
            var PageContentResult = new PageContentResult()
            {
                Content = content.Content,
                Description = content.Description,
                Image = content.Image,
                KeyWord = content.KeyWord,
                PageSlug = pageSlug,
                TitlePage = content.TitlePage
            };
            return PageContentResult;
        }
        public async Task<PageSeoResult> GetSeoBySlug(string pageSlug, int source = 1)
        {
            var content = await _repository.GetSeoBySlug(pageSlug, source);
            if (content == null || content.Id < 1)
            {
                return null;
            }
            var PageContentResult = new PageSeoResult()
            {

                Description = content.Description,
                Image = content.Image,
                KeyWord = content.KeyWord,
                PageSlug = pageSlug,
                TitlePage = content.TitlePage
            };
            return PageContentResult;
        }





    }
}
