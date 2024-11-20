using Topmass.Web.Business;
using TopMass.Web.Business.Model;

namespace TopMass.Web.Business
{
    public interface ICategoryArticleBusiness
    {
        public Task<CategoryResult> AddOrUpdate(CategoryRequestAdd article);
        public Task<DeleteResult> Delete(DeleteRequest article);



    }
}
