using Topmass.Core.Model.Profile;

namespace Topmass.core.Business.Model
{
    public class CandidateInfoRequest
    {
        public string Email { get; set; }
        public int UserId { get; set; }

    }
    public class Idinfo
    {
        public int Id { get; set; }
    }
    public class CandidateInfoUpdate
    {
        public string Phone { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName { get; set; }

        public int? UserId { get; set; }

        public int? HandleBy { get; set; }

        public bool? PrivateMode { get; set; }

        public bool? PublicMode { get; set; }

        public string AvatarLink { get; set; }

        public bool? IsSwichSearchMode { get; set; }



    }

    public class CandidateInfoResult
    {
        public int? UserId { get; set; }

        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? AvatarLink { get; set; }
        public bool WorkMode { get; set; }
        public bool SearchMode { get; set; }
        public string? UserName { get; set; }

        public int? Status { get; set; }

        public string? StatusText
        {
            get
            {
                return "Hoạt động";
            }
        }
        //public string? FullName { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }
        public string? AuthenticationLevelText { get; set; }
        public CandidateInfoResult()
        {
            AuthenticationLevelText = "Tài khoản đã xác thực";

        }

    }

    public class NTDViewer
    {
        public string Name { get; set; }
        protected string AvatarLink { get; set; }
        public string CompanyName { get; set; }
        public string Slug { get; set; }
        public string FullLinkAvatar
        {
            get
            {
                if (string.IsNullOrEmpty(AvatarLink))
                {
                    return "";
                }
                return "https://www.cdn.topmass.vn/static/" + AvatarLink;
            }
        }
        public string Url
        {
            get
            {
                if (string.IsNullOrEmpty(Slug))
                {
                    return "";
                }
                return "https://topmass.vn/cong-ty/" + Slug;
            }
        }

        public DateTime CreateAt { get; set; }



    }

    public class ProfileCVUserDisplay : ProfileCVUser
    {
        public string ProvinceName { get; set; }

        public ProfileCVUserDisplay()
        {
            ProvinceName = "Tỉnh thành";
        }
    }

    public class AllowSearchIndex
    {
        public int Allow { get; set; }
    }

    public class RegionalSearchItem
    {
        public string Code { get; set; }
        public string Slug { get; set; }
        public string Name { get; set; }
    }
}
