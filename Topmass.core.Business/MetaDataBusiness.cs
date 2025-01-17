using Topmass.core.Business.Model;
using Topmass.Core.Model.Profile;
using Topmass.Core.Repository;


namespace Topmass.core.Business
{
    public partial class MetaDataBusiness : IMetaDataBussiness
    {
        private readonly IMetaDataRepository _repository;
        public MetaDataBusiness(IMetaDataRepository repository
            )
        {
            _repository = repository;
        }
        public async Task<MetaDataReponse> GetInfo(MetaDataRequest request)
        {
            var response = new MetaDataReponse();
            var result = await _repository.GetInfo(request.KeyScreen);
            if (result == null || result.Id < 1)
            {
                result = new MetaDataPage()
                {
                    Title = "Topmass-tuyển dụng việc làm",
                    ImageLink = "https://topmass.vn/imgs/logo-new.svg",
                    KeyWord = "Topmass",
                    Author = "Topmass",
                    ShortDes = "Topmass-tuyển dụng việc làm",
                };
            }

            response.MetaTitle = result.Title;
            response.MetaDes = result.ShortDes;
            response.MetaKeyWord = result.KeyWord;
            response.MetaImage = result.ImageLink;
            response.MetaAuthor = result.Author;
            return response;
        }

        public async Task<BaseResult> GetAllWebSlug()
        {
            var response = new BaseResult();
            var result = await _repository.ExecuteSqlProcerduceToList<SlugWithRouterReponse>("sp_getAllWebSlug", new
            {
            });
            var listReponse = new List<string>();
            foreach (var item in result)
            {
                listReponse.Add(item.Slug);
            }
            response.Data = listReponse;
            return response;
        }
    }
}
