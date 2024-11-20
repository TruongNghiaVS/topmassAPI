using Microsoft.Extensions.Configuration;
using Topmass.Core.Model;
using Topmass.Core.Repository;

namespace Topmass.Web.Repository
{
    public class CategoryAritcleRepository : RepositoryBase<CategoryAritcle>, ICategoryAritcleRepository
    {
        public CategoryAritcleRepository(IConfiguration configuration)
            : base(configuration)
        {
            tableName = "CategoryArticle";
        }


    }
}
