using Topmass.Core.Model.CV;
using Topmass.Core.Repository.Model;

namespace Topmass.Core.Repository
{
    public partial interface ISearchCVRepository : IBaseRepository<SearchCVModel>
    {



        public Task<DigitalFileCVModelReponse> CheckReloadFile(int searchId);

    }
}
