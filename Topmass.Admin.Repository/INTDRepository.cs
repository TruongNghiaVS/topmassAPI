namespace Topmass.Admin.Repository
{
    public interface INTDRepository
    {
        public Task<SearchNTDReponse> GetAll(dynamic request);
        public Task<List<NTDLogInfo>> GetAllLog(int id);
        public  Task<List<NTDAccountLogInfo>> GetALlLogAccount(int id);
        public Task<List<NTDShortInfo>> GetAllShortNTD();
        public Task<BasicInfoNTD> GetDetailBasic(int id);
        public Task<CompanyInfoNTD> GetCompanyInfo(int id);
        public Task<DocumentNTDInfo> GetDocumentNTD(int id);
        public Task<bool> UpdateDocumentNTD(string StatusChange,
            string NotedChange,
            int Id,
            string content,
            int reasonReject
            );
        public Task<bool> AddDocumentNTD(string StatusChange,
        string NotedChange,
        int Id,
        string content,
        string linkFile,
        int reasonReject);
        public Task<bool> UpdateInfoHuman(int statusAccout, int statusConfirm, int id, int reasonCode = -1, string noted ="");
        public Task<bool> UpdatePersonalPerson(string FullName, int Gender, string phoneNumber, int id);
        public Task<bool> UpdateConfirmStatus(int id, int statusChange, string noted = "", string content = "");
        public Task<bool> UpdateStatusDisplay(int id, int statusChange, string noted = "", string content = "");
    }
}
