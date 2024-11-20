using Topmass.Core.Model.location;
using Topmass.Core.Repository;
using TopMass.Core.Result;


namespace Topmass.Business.Regional
{
    public partial class RegionalBusiness : IRegionalBusiness
    {
        private readonly IRegionalRepository _repository;

        private List<RegionalModel> dataRegionals;

        public RegionalBusiness(IRegionalRepository userRepository)
        {
            _repository = userRepository;
            dataRegionals = new List<RegionalModel>();
        }



        public async Task<RegionalBsReponse> GetRegional(RegionalBsRequest request)
        {
            var result = new RegionalBsReponse();
            var requestFileter = request as RegionalRequest;
            var reponse = await _repository.GetRegional(requestFileter);
            result.Data = reponse.Data;
            result.Total = reponse.Total;
            return result;
        }


        public async Task<BaseResult> LoadAllData()
        {
            var reponse = new BaseResult();
            var data = await _repository.ExecuteSqlProcerduceToList<RegionalModel>(
                "select * from regionals ", new { }, System.Data.CommandType.Text
                );
            var dataGlobal = GlobalRegional.GetRegional();
            dataGlobal.DataGlobal = data;
            return reponse;
        }

        public async Task<BaseResult> GetAllProvices()
        {
            var reponse = new BaseResult();
            var request = new RegionalRequest()
            {
                Type = 1
            };
            var data = await _repository.GetRegional(request);
            reponse.Data = data;
            return reponse;
        }

        public async Task<BaseResult> GetAllDistrict(GetAllDistrictRequest request)
        {
            var reponse = new BaseResult();
            var requestGet = new RegionalRequest()
            {
                Type = 2,
                Level1 = request.Code
            };
            var data = await _repository.GetRegional(requestGet);
            reponse.Data = data;
            return reponse;
        }

        public async Task<RegionalModel> GetInfoByCode(int code)
        {
            if (dataRegionals.Count < 1)
            {
                dataRegionals = await _repository.GetData();
            }
            var itemInfo = dataRegionals.Where(x => int.Parse(x.Code) == code).FirstOrDefault();
            return itemInfo;


        }

    }
}
