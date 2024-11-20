using Microsoft.Extensions.Configuration;
using Topmass.Core.Model;

namespace Topmass.Core.Repository
{
    public class MasterDataRepository : RepositoryBase<MasterDataModel>, IMasterDataRepository
    {
        public MasterDataRepository(IConfiguration configuration) : base(configuration)
        {
            tableName = "MasterData";
        }




    }
}
