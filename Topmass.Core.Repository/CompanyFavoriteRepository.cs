using Microsoft.Extensions.Configuration;
using Topmass.Core.Model;

namespace Topmass.Core.Repository
{
    public partial class CompanyFavoriteRepository : RepositoryBase<CompanyFavoriteModel>, ICompanyFavoriteModelRepository
    {
        public CompanyFavoriteRepository(IConfiguration configuration) : base(configuration)
        {
            tableName = "CompanyFavorite";
        }

    }
}
