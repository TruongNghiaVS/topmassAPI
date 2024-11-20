using Topmass.Core.Common;
using Topmass.Web.Repository;
using Topmass.Web.Repository.Model;
using TopMass.Web.Business;
using TopMass.Web.Business.Model;


namespace Topmass.Web.Business
{
    public partial class ArticleBusiness : IArticleBusiness
    {
        private readonly IArticleRepository _repository;

        public ArticleBusiness(IArticleRepository userRepository)
        {
            _repository = userRepository;
        }
        public async Task<ArticleResult> AddOrUpdate(ArticleRequestAdd article)
        {
            var reponse = new ArticleResult();

            if (string.IsNullOrWhiteSpace(article.Title) ||
                string.IsNullOrWhiteSpace(article.Content)
                )
            {
                reponse.AddError("email", "Thiếu thông tin");

            }
            if (!reponse.Success)
            {
                return reponse;

            }
            if (article.Title != null)
            {
                article.Slug = TopmassCommon.ToUrlSlug(article.Title);
            }
            var inputArticle = new Core.Model.ArticleModel()
            {
                Content = article.Content,
                CoverImage = article.CoverImage,
                CreateAt = DateTime.Now,
                CreatedBy = -1,
                Deleted = false,
                Slug = article.Slug,
                Id = -1,
                KeyWord = article.KeyWord,
                linked = article.linked,
                ShortDes = article.ShortDes,
                Status = 1,
                Title = article.Title,
                UpdateAt = DateTime.Now,
                UpdatedBy = -1
            };
            await _repository.AddOrUPdate(inputArticle);
            return reponse;
        }


        public async Task<ArticleResult> Delete(DeleteRequest article)
        {
            var reponse = new ArticleResult();

            if (article.Id < 1
                )
            {
                reponse.AddError("email", "Thiếu thông tin");

            }
            if (!reponse.Success)
            {
                return reponse;

            }
            await _repository.Delete(article.Id);
            return reponse;
        }


        public Task<GetAllArticleReponse> GetAll(ArticleReqest request)
        {
            return _repository.GetAll(request);
        }

        public Task<GetAllArticleReponse> GetAllShort(ArticleReqest request)
        {
            return _repository.GetAllShort(request);
        }
        public Task<GetAllByCategoryReponse> GetAllBlogsCategory()
        {
            return _repository.GetAllBlogsCategory();

        }
        public Task<GetAllByCategoryReponse> GetAllCatagroy()
        {
            return _repository.GetAllCategory();
        }


    }
}
