//using Microsoft.Extensions.Configuration;
//using Topmass.Admin.Repository;
//using Topmass.Core.Model.Campagn;
//using Topmass.Core.Repository;

//namespace Topmass.Campagn.Repository
//{
//    public partial class NTDRepository : RepositoryBase<CampagnModel>, INTDRepository
//    {
//        private readonly IJobRepository _jobRepository;
//        public NTDRepository(IConfiguration configuration, IJobRepository jobRepository) : base(configuration)
//        {
//            _jobRepository = jobRepository;
//        }

//        public Task<dynamic> GetAl(NTDRequest request)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
