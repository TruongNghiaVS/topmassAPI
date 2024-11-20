using Topmass.Campagn.Business.Model;
using Topmass.Campagn.Repository;
using Topmass.Core.Model.Campagn;
using Topmass.Core.Repository;
using TopMass.Core.Result;

namespace Topmass.Campagn.Business
{
    public class CampagnBusiness : ICampagnBusiness
    {
        private readonly ICampagnRepository _repository;
        private readonly ICampagnExRepository _campagnExRepository;

        public CampagnBusiness(ICampagnRepository repository,
            ICampagnExRepository campagnExRepository
            )
        {
            _repository = repository;
            _campagnExRepository = campagnExRepository;
        }

        public async Task<BaseResult> GetAllJobOfCampagn(SearchJobOfCampagnRequest request)
        {
            var reponse = new BaseResult();
            var result = await _campagnExRepository.GetAllJob
            (new Repository.Model.SearchJobByCampagn()
            {
                CampagnId = request.CampagnId,

                From = request.From,
                To = request.To
            });
            reponse.Data = result;
            return reponse;
        }
        public async Task<BaseResult> GetAll(CampagnSearchFilter request)
        {
            var reponse = new BaseResult();

            var result = await _campagnExRepository.GetAll(new Repository.Model.SearchCampagnRequest()
            {
                From = request.From,
                Status = request.Status,
                Email = request.Email,
                To = request.To
            });
            reponse.Data = result;


            return reponse;
        }
        public async Task<BaseResult> AddCampagn(CampagnItemAdd itemAdd)
        {
            var result = new BaseResult();
            if (string.IsNullOrEmpty(itemAdd.Name))
            {
                result.AddError(nameof(itemAdd.Name), "Thiếu tiêu đề");
                return result;
            }
            var campagnInsert = new CampagnModel
            {
                RelId = itemAdd.HandleBy,
                Email = itemAdd.Email,
                Name = itemAdd.Name,
                CreatedBy = itemAdd.HandleBy,
                UpdatedBy = itemAdd.HandleBy
            };

            //if (itemAdd.From.HasValue)
            //{
            //    campagnInsert.From = itemAdd.From.Value;
            //}
            //if (itemAdd.To.HasValue)
            //{
            //    campagnInsert.To = itemAdd.To.Value;
            //}
            campagnInsert.Package = "0";
            await _repository.AddOrUPdate(campagnInsert);
            return result;
        }

        public async Task<BaseResult> ChangeStatusActive(CampagnItemStatusUpdate itemAdd)
        {
            var result = new BaseResult();
            if (!itemAdd.IdUpdate.HasValue)
            {
                result.AddError(nameof(itemAdd.IdUpdate), "Thiếu thông tin đối tượng");
                return result;
            }
            if (!itemAdd.Status.HasValue)
            {
                result.AddError(nameof(itemAdd.Status), "Thiếu thông tin trạng thái");
                return result;
            }
            var campagnInsert = await _repository.GetById(itemAdd.IdUpdate.Value);
            campagnInsert.Status = itemAdd.Status.Value;
            await _repository.AddOrUPdate(campagnInsert);
            return result;
        }

        public async Task<BaseResult> UpdateCampagn(CampagnItemUpdate itemAdd)
        {
            var result = new BaseResult();
            if (itemAdd.IdUpdate < 1)
            {
                result.AddError(nameof(itemAdd.IdUpdate), "Thiếu tiêu đề");
                return result;
            }
            if (string.IsNullOrEmpty(itemAdd.Name))
            {
                result.AddError(nameof(itemAdd.Name), "Thiếu tiêu đề");
                return result;
            }
            var campagnInsert = await _repository.GetById(itemAdd.IdUpdate.Value);
            //if (itemAdd.From.HasValue)
            //{
            //    campagnInsert.From = itemAdd.From.Value;
            //}
            //if (itemAdd.To.HasValue)
            //{
            //    campagnInsert.To = itemAdd.To.Value;
            //}


            campagnInsert.Name = itemAdd.Name;
            campagnInsert.UpdatedBy = itemAdd.HandleBy;

            if (itemAdd.Status.HasValue)
            {
                campagnInsert.Status = itemAdd.Status.Value;
            }
            await _repository.AddOrUPdate(campagnInsert);
            return result;
        }
    }
}
