namespace Topmass.Bussiness.Mail
{
    public partial interface IMailBussiness
    {
        public Task<MailReponse> PushMail(MailItem mailItem);
        public Task<ResultRequestSendMail> CandidateSuccessRegister(string Email);
        public Task<ResultRequestSendMail> CanddidateCheckMailPassword(string email, string code);
        public Task<ResultRequestSendMail> CandidateSucessChangePassNoti(string email);

    }
}
