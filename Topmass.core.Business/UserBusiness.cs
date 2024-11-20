//using Topmass.core.Business.Model;
//using Topmass.Core.Model;
//using Topmass.Core.Repository;


//namespace Topmass.core.Business
//{
//    public partial class CandidateBusiness : BaseBusiness<CandidateModel>, IUserBuisiness
//    {
//        private readonly IUserRepository _repository;
//        public UserBusiness(IUserRepository userRepository) : base(userRepository)
//        {
//            _repository = userRepository;
//        }

//        public Task<bool> AddOrUPdate(UserModel itemModel)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<bool> Delete(UserModel itemModel)
//        {
//            throw new NotImplementedException();
//        }

//        public async Task<LoginResult> Login(string username, string password)
//        {
//            var result = new LoginResult();
//            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
//            {
//                result.Success = false;
//                result.Message = MessageEror.Msg_user_misingParamr;

//            }
//            var user = new BaseUserInfo();
//            user.CreateAt = DateTime.Now;
//            //user.UserName = "NghiaNT15";
//            //user.Password = "Nhi20222023@";
//            var tokenUser = this.GenerateToken(user);
//            result.Token = tokenUser;
//            result.Success = true;
//            result.Message = "Tạo token thành công";
//            return result;
//        }


//        public async Task<UserResgisterResult> RegisterUser(CandidateRegisterRequest request)
//        {
//            var reponse = new UserResgisterResult();




//            return reponse;

//        }

//        Task<IEnumerable<UserModel>> IBaseBusiness<UserModel>.GetAllBase()
//        {
//            throw new NotImplementedException();
//        }

//        Task<UserModel> IBaseBusiness<UserModel>.GetById(int id)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
