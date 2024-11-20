using Topmass.Core.Common;
using Topmass.Web.Repository;
using TopMass.Web.Business;
using TopMass.Web.Business.Model;


namespace Topmass.Web.Business
{
    public partial class CategoryArticleBusiness : ICategoryArticleBusiness
    {
        private readonly ICategoryAritcleRepository _repository;

        public CategoryArticleBusiness(ICategoryAritcleRepository userRepository)
        {

            _repository = userRepository;
        }
        public async Task<CategoryResult> AddOrUpdate(CategoryRequestAdd article)
        {
            var reponse = new CategoryResult();

            if (string.IsNullOrWhiteSpace(article.Title) ||
                string.IsNullOrWhiteSpace(article.ShortDes)
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
            var inputArticle = new Core.Model.CategoryAritcle()
            {

                CoverImage = article.CoverImage,
                CreateAt = DateTime.Now,
                CreatedBy = -1,
                Deleted = false,
                Slug = article.Slug,
                Id = -1,
                KeyWord = article.KeyWord,

                ShortDes = article.ShortDes,
                Status = 1,
                Title = article.Title,
                UpdateAt = DateTime.Now,
                UpdatedBy = -1
            };

            await _repository.AddOrUPdate(inputArticle);
            return reponse;
        }


        public async Task<DeleteResult> Delete(DeleteRequest article)
        {
            var reponse = new DeleteResult();

            if (article.Id < 1
                )
            {
                reponse.AddError("Thiếu thông tin");

            }
            if (!reponse.Success)
            {
                return reponse;

            }
            await _repository.Delete(article.Id);
            return reponse;
        }





    }
}
