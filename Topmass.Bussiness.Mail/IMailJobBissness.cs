namespace Topmass.Bussiness.Mail
{
    public partial interface IMailJobBissness
    {

        public Task<ResultNotficationRecruiterWhenHasApplyRequest> NotficationRecruiterWhenHasApply(NotficationRecruiterWhenHasApplyRequest request);


    }
}
