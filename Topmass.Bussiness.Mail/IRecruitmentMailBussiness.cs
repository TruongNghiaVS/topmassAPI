namespace Topmass.Bussiness.Mail
{
    public partial interface IRecruitmentMailBussiness
    {

        public Task<MailReponse> PushMail(MailItem mailItem);

        public Task<ResultRequestSendMail> RecruitmentSuccessRegister(string Email);

        public Task<ResultRequestSendMail> RecruitmentCheckMailPassword(string email, string code);
        public Task<ResultRequestSendMail> RecruitmentSucessChangePassNoti(string email);

    }
}
