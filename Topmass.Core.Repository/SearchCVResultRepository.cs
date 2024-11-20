using Microsoft.Extensions.Configuration;
using Topmass.Core.Model.CV;

namespace Topmass.Core.Repository
{
    public partial class SearchCVResultRepository : RepositoryBase<SearchCVResultModel>, ISearchCVResultRepository
    {
        public SearchCVResultRepository(IConfiguration configuration) : base(configuration)
        {
            tableName = "SearchResult";
        }

    }
}
