using Microsoft.Extensions.Configuration;
using Topmass.Core.Model.CV;

namespace Topmass.Core.Repository
{
    public partial class SearchCVExtraRepository : RepositoryBase<SearchCVExtraModel>,
        ISearchCVExtraRepository
    {
        public SearchCVExtraRepository(IConfiguration configuration) : base(configuration)
        {
            tableName = "SearchCVExtra";
        }
    }
}
