using Topmass.core.Business.MessageError;
using Topmass.Core.Model;
using Topmass.Core.Repository;


namespace Topmass.core.Business
{
    public class BaseBusiness<TModel> : IBaseBusiness<TModel> where TModel : BaseModel, new()

    {
        private readonly IBaseRepository<TModel> _repository;

        protected ErrorMessage MessageEror { get; set; }


        public BaseBusiness(IBaseRepository<TModel> baseRepository)
        {
            _repository = baseRepository;
            MessageEror = ErrorMessage.GetErrorMessage();
        }

        public async Task<bool> AddOrUPdate(TModel itemModel)
        {
            return await _repository.AddOrUPdate(itemModel);
        }

        public async Task<bool> Delete(TModel itemModel)
        {
            return await _repository.Delete(itemModel);
        }

        public async Task<bool> Delete(int id)
        {
            return await _repository.Delete(id);
        }

        public async Task<IEnumerable<TModel>> GetAllBase()
        {
            return await _repository.GetAllBase();
        }

        public async Task<TModel> GetById(int id)
        {
            return await _repository.GetById(id);
        }


    }
}
