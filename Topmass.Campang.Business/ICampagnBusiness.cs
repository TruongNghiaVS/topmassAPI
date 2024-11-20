using Topmass.Campagn.Business.Model;
using TopMass.Core.Result;

namespace Topmass.Campagn.Business
{
    public interface ICampagnBusiness
    {

        public Task<BaseResult> AddCampagn(CampagnItemAdd itemAdd);
        public Task<BaseResult> UpdateCampagn(CampagnItemUpdate itemAdd);

        public Task<BaseResult> ChangeStatusActive(CampagnItemStatusUpdate itemAdd);

        public Task<BaseResult> GetAll(CampagnSearchFilter itemAdd);



        public Task<BaseResult> GetAllJobOfCampagn(SearchJobOfCampagnRequest itemAdd);

    }
}
