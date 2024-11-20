using Topmass.Core.Model.location;

namespace Topmass.Core.Repository.Model
{
    public class UserAdd
    {

        public string? UserName { get; set; }
        public string? Pass { get; set; }
        public int? TypeUser { get; set; }

        public int? UserId { get; set; }

        public string? FirstName { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }

        public int CreatedBy { get; set; }
    }


    public class CandidateAdd
    {
        public string? UserName { get; set; }
        public string? Pass { get; set; }
        public string? FirstName { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public int CreatedBy { get; set; }
    }


    public class MailInfoRepAdd
    {
        public string MailTo { get; set; }

        public string Subject { get; set; }

        public string Content { get; set; }

        public int TypeContent { get; set; }

    }
    public class CandidateSearch
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }


    public class RegionalModelAdd : RegionalModel
    {

    }


    public class CandidateInfoUpdateRequest
    {
        public int? UserId { get; set; }
        public string? Email { get; set; }

        public string? AvatarLink { get; set; }

        public bool PrivateMode { get; set; }

        public bool PublicMode { get; set; }

    }


}
