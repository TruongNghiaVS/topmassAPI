using TopMass.Core.Result;

namespace Topmass.Recruiter.Bussiness

{
    public partial interface IRecruiterMail
    {
        public Task<BaseResult> RequestMailValidAccount(string email);
        public Task<BaseResult> NotificationMailWhenJobAplyStatusChange(int applyId);

    }
}
