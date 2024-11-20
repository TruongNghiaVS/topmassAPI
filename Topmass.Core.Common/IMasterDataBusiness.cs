using TopMass.Core.Result;

namespace Topmass.Core.Common
{
    public interface IMasterDataBusiness
    {
        public Task<bool> LoadData();
        Task<BaseResult> FillterData(int typeData = -1);
    }
}
