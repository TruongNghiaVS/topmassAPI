using Topmass.Admin.Repository;
using Topmass.Core.Model;
using Topmass.Utility;
using TopMass.Core.Result;

namespace Topmass.Admin.Business
{
    public class AdminArticleBusiness : BaseBusiness, IAdminArticleBusiness
    {

        private readonly IArticleAdminRepository _articleAdminRepository;
        public AdminArticleBusiness(IAdminRepository _adminRepository,

            IArticleAdminRepository articleAdminRepository
            ) : base(_adminRepository)
        {
            _articleAdminRepository = articleAdminRepository;

        }

        public async Task<BaseResult> GetAll(SearchArticleRequest request)
        {
            var reponse = new BaseResult();
            var searchRequest = new ArticleAdminRequest()
            {
                From = request.From,
                To = request.To,
                Page = request.Page,
                Limit = request.Limit,

            };
            var data = await _articleAdminRepository.GetAll(searchRequest);
            reponse.Data = data;
            return reponse;
        }

        public async Task<BaseResult> Detail(int id)
        {
            var reponse = new BaseResult();
            reponse.Data = await _articleAdminRepository.GetDetail(id);
            return reponse;

        }

        public async Task<bool> Delete(int id)
        {
            return await _articleAdminRepository.DeleteArticle(id);
        }

        public async Task<BaseResult> AddorUpdate(ArticleRequestAdd request)
        {

            var reponseData = new BaseResult();

            var slugInput = request.Slug;
            if (string.IsNullOrEmpty(slugInput))
            {
                slugInput = Utilities.SlugifySlug(request.Title);
            }
            var articleReqest = new ArticleModel()
            {
                Content = request.Content,
                CreateAt = DateTime.Now,
                CreatedBy = -1,
                Deleted = false,
            };
            if (request.Id > 0)
            {
                var reponse = await _articleAdminRepository.GetDetail(request.Id.Value);
                articleReqest = reponse.Data;
                articleReqest.UpdateAt = DateTime.Now;
                articleReqest.CreateAt = DateTime.Now;

            }
            var reponseCheck = await _articleAdminRepository.CheckSlug(slugInput);
            if (articleReqest.Id < 1)
            {
                //articleReqest.Slug = Utilities.SlugifySlug(request.Title);
                articleReqest.CreateAt = DateTime.Now;
                articleReqest.UpdateAt = DateTime.Now;
            }
            if (reponseCheck != null)
            {
                if (reponseCheck.Id != articleReqest.Id)
                {
                    reponseData.AddError("slug", "Đã tồn tại slug trong hệ thống, vui lòng chọn slug khác");
                    return reponseData;
                }
            }
            articleReqest.Slug = slugInput;
            articleReqest.linked = request.CategryIdLink;
            articleReqest.ShortDes = request.ShortDes;
            articleReqest.Content = request.Content;
            articleReqest.Title = request.Title;
            articleReqest.KeyWord = request.Keyword;
            articleReqest.CoverImage = request.LinkImage;
            reponseData.Data = await _articleAdminRepository.AddorUpdateArticle(articleReqest);
            return reponseData;
        }

    }
}
