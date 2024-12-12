namespace Topmass.Recruiter.Model
{

    public class AuthenRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class RequestChangePasswordRequest
    {

        public string Email { get; set; }
    }
    public class PasswordChanged
    {
        public string? CurrentPassword { get; set; }
        public string Password { get; set; }
    }
    public class ValidLinkRequest
    {
        public string Code { get; set; }
    }

    public class UpdateBasicRequest
    {
        public string? Phone { get; set; }
        public int Gender { get; set; }
        public string? FullName { get; set; }

        public string? AvatarLink { get; set; }

    }

    public class BusinessLicenseRequest

    {
        public string DocumentLink { get; set; }
        public string DocumentType { get; set; }
        public BusinessLicenseRequest()
        {

        }

    }

    public class CompanyInfoRequest

    {

        public string? TaxCode { get; set; }

        public string? Email { get; set; }

        public string? Website { get; set; }

        public string? Capacity { get; set; }

        public string? FullName { get; set; }

        public string? AddressInfo { get; set; }
        public string? Phone { get; set; }

        public string? ShortDes { get; set; }
        public string? LogoLink { get; set; }
        public string? CoverLink { get; set; }

        public string? MapInfo { get; set; }
        public int? RelId { get; set; }

        public string? IframeEmbeddedMap { get; set; }
        public CompanyInfoRequest()
        {
            RelId = -1;
        }

    }

    public class InputSearchCV
    {
        public string? KeyWord { get; set; }
        public string? LocationCode { get; set; }

        public string? CvKey { get; set; }

        public int? Gender { get; set; }
        public int? FromYear { get; set; }

        public int? ToYear { get; set; }

        public DateTime? DayOfBirth { get; set; }

        public string? EducationalLevelArray { get; set; }

        public string? SchoolSearch { get; set; }

        public int? Limit { get; set; }

        public int? Page { get; set; }
        public InputSearchCV()
        {
            Limit = 10;
            Page = 1;
        }

    }

    public class HistoryRequest
    {

        public DateTime? From { get; set; }

        public DateTime? To { get; set; }

        public HistoryRequest()
        {
            From = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            To = From.Value.AddDays(1).AddSeconds(-1);
        }
    }
}
