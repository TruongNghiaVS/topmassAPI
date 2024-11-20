using Microsoft.Extensions.Configuration;
using Topmass.Core.Model;
using Topmass.Core.Repository.Model;

namespace Topmass.Core.Repository
{
    public class CandidateInfoRepository : RepositoryBase<CandidateInfoModel>, ICandidateInfoRepository
    {
        private readonly ICandidateRepository _candidateRepository;
        public CandidateInfoRepository(IConfiguration configuration,

            ICandidateRepository candidateRepository
            ) : base(configuration)
        {
            tableName = "CandidateInfo";
            _candidateRepository = candidateRepository;
        }


        public async Task<CandidateInfoModel> GetInfo(string email = null)
        {
            var result = await this.FindOneByStatementSql<CandidateInfoModel>("select * from CandidateInfo where email = @email",
                 new
                 {
                     email
                 }
                 );

            if (result != null)
            {
                return result;
            }

            var candidateitem = _candidateRepository
                                .FindOneByStatementSql<CandidateModel>("select top 1 * from Candidate where email = @email ", new { email });

            var itemInsert = new CandidateInfoModel()
            {
                AvatarLink = "",
                CreateAt = DateTime.Now,
                CreatedBy = candidateitem.Id,
                Status = 1,
                UpdateAt = DateTime.Now,
                PublicMode = true,
                PrivateMode = false,
                Email = result.Email,
                UserId = candidateitem.Id,
                Deleted = false,
                UpdatedBy = candidateitem.Id,


            };

            await AddOrUPdate(itemInsert);

            var result2 = await this.FindOneByStatementSql<CandidateInfoModel>("select * from CandidateInfo where email = @email",
               new
               {
                   email
               }
               );
            return result2;

        }
        public async Task<bool> UpdateInfoCandidate(CandidateInfoUpdateRequest requestUpdate)
        {
            var result = await this.FindOneByStatementSql<CandidateInfoModel>("select * from CandidateInfo where email = @email",
                 new
                 {
                     email = requestUpdate.Email
                 }
                 );

            if (result == null)
            {
                return false;
            }

            if (string.IsNullOrEmpty(requestUpdate.AvatarLink))
            {
                result.AvatarLink = requestUpdate.AvatarLink;
            }
            result.PublicMode = requestUpdate.PublicMode;
            result.PrivateMode = requestUpdate.PrivateMode;
            result.UpdateAt = DateTime.Now;

            await AddOrUPdate(result);

            return true;

        }




    }
}
