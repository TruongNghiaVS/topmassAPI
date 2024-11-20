using Topmass.Admin.Repository;

namespace Topmass.Admin.Business
{
    public interface INTDBusiness : IBaseBusiness
    {

        public Task<List<NTDShortInfo>> GetAllShortNTD();
        public Task<SearchNTDReponse> GetAllNTD(SearchNTDRequest request);
        public Task<DataDetailInfo> GetDetail(int id);
        public Task<bool> AddOrUpdateDocumnet(UpdateDocumnetRequest request);

        public Task<bool> UpdateInfoHuman(int statusAccout, int statusConfirm, int id , int reasonCode=-1, string noted ="");
        public Task<bool> UpdatePersonalPerson(string FullName, int Gender, string phoneNumber, int id);

        

        public Task<bool> ResetPasswordAccount(int id);
        public Task<bool> SendMailActiveAccount(int id);


        public Task<List<NTDLogInfo>> GetAllLog(int request);

        public Task<List<NTDAccountLogInfo>> GetALlLogAccount(int id);

        //public Task<BaseResultAdd> AddNTD(NTDRequestAdd request);
    }
}
