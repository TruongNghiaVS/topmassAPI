using Topmass.CV.Business.Model;
using Topmass.CV.Repository;
using Topmass.CV.Repository.Model;

namespace Topmass.CV.Business
{
    public class CVUserBusiness : ICVUserBusiness
    {

        private readonly ICVRepository _cVRepository;



        public CVUserBusiness(ICVRepository cVRepository
            )
        {
            _cVRepository = cVRepository;


        }


        public async Task<GetAllCVUserReponse> GetAllCV(int UserId)
        {
            var respone = new GetAllCVUserReponse();
            if (UserId < 1)
            {
                return respone;
            }
            var dataResult = await _cVRepository.GetAllCVOfCan(new CVResumeRequest()
            {
                UserId = UserId,
                HandleBy = UserId
            });
            respone.Data = dataResult.Data;
            return respone;
        }


    }
}
