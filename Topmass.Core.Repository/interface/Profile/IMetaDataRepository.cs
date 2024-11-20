using Topmass.Core.Model.Profile;

namespace Topmass.Core.Repository
{
    public partial interface IMetaDataRepository : IBaseRepository<MetaDataPage>
    {
        public Task<MetaDataPage> GetInfo(string keyScreen, int typeData = 0);
    }
}
