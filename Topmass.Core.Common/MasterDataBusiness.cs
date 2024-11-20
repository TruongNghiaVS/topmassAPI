using Topmass.Core.Model;
using Topmass.Core.Repository;
using TopMass.Core.Result;

namespace Topmass.Core.Common
{
    public class MasterDataBusiness : IMasterDataBusiness
    {

        private List<MasterDataModel> _listData;
        private readonly IMasterDataRepository _masterDataRepository;
        public MasterDataBusiness(IMasterDataRepository masterDataRepository)
        {
            _masterDataRepository = masterDataRepository;
        }

        public async Task<bool> LoadData()
        {
            _listData = await _masterDataRepository.GetAllToList();

            if (_listData == null)
            {
                _listData = new List<MasterDataModel>();
            }
            return true;
        }

        public async Task<BaseResult> FillterData(int typeData = -1)
        {
            var reponse = new BaseResult();
            if (typeData == 0)
            {
                return reponse;
            }

            if (_listData == null)
            {
                await LoadData();
            }
            var dataFilter = _listData.Where(x => x.TypeData == typeData).ToList();
            reponse.Data = dataFilter;
            return reponse;

        }








    }
}
