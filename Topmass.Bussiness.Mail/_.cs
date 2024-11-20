namespace Topmass.Bussiness.Mail
{
    public class MailReponse
    {
        public bool IsSucess { get; set; }
    }

    public class ResultRequestSendMail
    {
        public bool IsSucess { get; set; }
    }

    public class NotficationRecruiterWhenHasApplyRequest
    {
        public int JobId { get; set; }
        public int UserId { get; set; }

    }

    public class ResultNotficationRecruiterWhenHasApplyRequest
    {

        public bool IsSucess { get; set; }
    }

    public class EmailProper
    {

        public string Email { get; set; } = string.Empty;
    }
}
