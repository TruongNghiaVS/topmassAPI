using Microsoft.Extensions.Configuration;
using Topmass.Core.Model.CV;

namespace Topmass.Core.Repository
{
    public partial class SearchCVRepository : RepositoryBase<SearchCVModel>, ISearchCVRepository
    {
        public SearchCVRepository(IConfiguration configuration) : base(configuration)
        {
            tableName = "SearchCV";
        }

    }
}
