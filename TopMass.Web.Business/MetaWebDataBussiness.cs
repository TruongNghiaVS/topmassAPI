using Topmass.Core.Model.Profile;
using Topmass.Core.Repository;
using TopMass.Core.Result;
using TopMass.Web.Business;
using TopMass.Web.Business.Model;


namespace Topmass.Web.Business
{
    public partial class MetaWebDataBussiness : IMetaWebDataBussiness
    {
        private readonly IMetaDataRepository dataRepository;
        public MetaWebDataBussiness(IMetaDataRepository _dataRepository)
        {

            dataRepository = _dataRepository;
        }
        public async Task<MetaDataReponse> GetInfo(MetaDataRequest request)
        {
            var response = new MetaDataReponse();
            var result = await dataRepository.GetInfo(request.KeyScreen);
            if (result == null || result.Id < 1)
            {
                result = new MetaDataPage()
                {
                    Title = "Topmass_hởi đầu sự bứt phá",
                    ImageLink = "https://topmass.vn/imgs/logo-footer.png",
                    KeyWord = "Topmass",
                    Author = "Topmass",
                    ShortDes = "Khởi đầu sự bứt phá",
                };
            }
            response.MetaTitle = result.Title;
            response.MetaDes = result.ShortDes;
            response.MetaKeyWord = result.KeyWord;
            response.MetaImage = result.ImageLink;
            response.MetaAuthor = result.Author;
            return response;
        }
    }
}
