﻿namespace Topmass.Admin.Repository
{
    public interface IJobAdminRepository
    {
        public Task<SearchJobAdminReponse> GetAll(dynamic request);
        public Task<BasicInfoJobAdmin> GetDetailBasic(int id);

        public Task<DataInfoJobAdmin> GetInfoJobAdmin(int id);
        public Task<CompanyInfoNTD> GetCompanyInfo(int id);
        public Task<DocumentNTDInfo> GetDocumentNTD(int id);

        public Task<bool> UpdateDocumentNTD(string StatusChange,
            string NotedChange,
            int Id,
            string content);
        public Task<bool> AddDocumentNTD(string StatusChange,
     string NotedChange,
     int Id,
     string content,
     string linkFile);
        public Task<List<JobLogAdminItemDisplay>> GetAllLog(int id);
        public Task<bool> UpdateInfoHuman(int statusAccout, int statusConfirm, int id);

    }
}
