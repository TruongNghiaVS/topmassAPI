using Topmass.Core.Model.location;
using Topmass.Core.Repository.Model;

namespace Topmass.Core.Repository
{
    public partial interface IRegionalRepository : IBaseRepository<RegionalModel>
    {
        public Task<bool> HanldeAdd(RegionalModelAdd itemModel);

        public Task<RegionalReponse> GetRegional(RegionalRequest request);

        public Task<List<RegionalModel>> GetData();
    }
}
