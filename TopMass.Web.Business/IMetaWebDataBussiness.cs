using Topmass.Web.Business;
using Topmass.Web.Repository.Model;
using TopMass.Web.Business.Model;

namespace TopMass.Web.Business
{
    public interface IMetaWebDataBussiness
    {

         Task<MetaDataReponse> GetInfo(MetaDataRequest request);

    }
}
