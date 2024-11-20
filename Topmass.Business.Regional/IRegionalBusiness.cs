


using Topmass.Core.Model.location;
using TopMass.Core.Result;

namespace Topmass.Business.Regional
{
    public interface IRegionalBusiness
    {

        public Task<RegionalBsReponse> GetRegional(RegionalBsRequest request);
        public Task<BaseResult> GetAllProvices();

        public Task<BaseResult> LoadAllData();
        public Task<BaseResult> GetAllDistrict(GetAllDistrictRequest request);

        public Task<RegionalModel> GetInfoByCode(int code);
    }
}
