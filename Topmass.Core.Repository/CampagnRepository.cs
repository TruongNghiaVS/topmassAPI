using Microsoft.Extensions.Configuration;
using Topmass.Core.Model.Campagn;

namespace Topmass.Core.Repository
{
    public partial class CampagnRepository : RepositoryBase<CampagnModel>, ICampagnRepository
    {
        public CampagnRepository(IConfiguration configuration) : base(configuration)
        {
            tableName = "Campaign";
        }




    }
}
