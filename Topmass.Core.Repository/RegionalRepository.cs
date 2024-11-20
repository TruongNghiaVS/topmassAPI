using Microsoft.Extensions.Configuration;
using Topmass.Core.Model.location;
using Topmass.Core.Repository.Model;

namespace Topmass.Core.Repository
{
    public class RegionalRepository : RepositoryBase<RegionalModel>, IRegionalRepository
    {

        public RegionalRepository(IConfiguration configuration) : base(configuration)
        {
            tableName = "regionals";
        }

        public async Task<bool> HanldeAdd(RegionalModelAdd itemModel)
        {
            return await AddOrUPdate(itemModel);

        }
        public async Task<RegionalReponse> GetRegional(RegionalRequest request)
        {
            var reponse = new RegionalReponse();
            var data = await this.ExecutePro<RegionalIndexModel>("sp_regionals_getall", request);
            reponse.Data = data;
            return reponse;
        }

        public async Task<List<RegionalModel>> GetData()
        {
            var reponse = new RegionalReponse();
            var sqlText = " select Code,Name, Slug, datatype, Level1, Level2 from regionals";
            var dataAll = await ExecuteSqlProcerduceToList<RegionalModel>(sqlText, null, System.Data.CommandType.Text);
            return dataAll;

        }


    }
}
