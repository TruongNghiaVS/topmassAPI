

using Topmass.core.Business.Model;

namespace Topmass.core.Business
{
    public interface IMetaDataBussiness
    {
        public Task<MetaDataReponse> GetInfo(MetaDataRequest request);
    }
}
