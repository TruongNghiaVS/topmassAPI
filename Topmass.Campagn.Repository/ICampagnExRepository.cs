using Topmass.Campagn.Repository.Model;

namespace Topmass.Campagn.Repository
{
    public partial interface ICampagnExRepository
    {
        public Task<CampangnSearchReponse> GetAll(SearchCampagnRequest request);


        public Task<CampangnSearchJobReponse> GetAllJob(SearchJobByCampagn request);

    }
}
