using Microsoft.Extensions.Configuration;
using Topmass.Core.Model.CV;
using Topmass.Core.Model.location;
using Topmass.Core.Repository.Model;

namespace Topmass.Core.Repository
{
    public partial class SearchCVRepository : RepositoryBase<SearchCVModel>, ISearchCVRepository
    {
        public SearchCVRepository(IConfiguration configuration) : base(configuration)
        {
            tableName = "SearchCV";
        }

        public async Task<DigitalFileCVModelReponse> CheckReloadFile(int searchId)
        {

            var reponseDigital = new DigitalFileCVModelReponse();
            var result = await FindOneByStatementSql<SearchCVModel>("select * from SearchCV  where id = @id", new
            {
                id = searchId
            });

            if (result == null || result.CandidateId < 1)
            {
                return reponseDigital;
            }
            var candidateId = result.CandidateId;


            var digitalFileCV = await FindOneByStatementSql<DigitalFileCVModel>("select * from DigitalFileCV where RelId = @candidateId",
                new
                {
                    candidateId = candidateId
                }
            );

            if (digitalFileCV == null)
            {
                return new DigitalFileCVModelReponse()
                {
                    Id = -1
                };
            }
            return new DigitalFileCVModelReponse()
            {
                Id = digitalFileCV.Id,
                RelId = digitalFileCV.RelId,
                ReloadFile = digitalFileCV.ReloadFile,
                FileCV = digitalFileCV.FileCV,
                FileCVHide = digitalFileCV.FileCVHide,
                CreateAt = digitalFileCV.CreateAt,
                UpdateAt = digitalFileCV.UpdateAt
            };
        }

    }
}
