namespace topmass.Model
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

        public string Password { get; set; }
    }
    public class ValidLinkRequest
    {
        public string Code { get; set; }
    }

    public class UpdateProfileRequest
    {
        public string? Phone { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string? FullName { get; set; }

        public bool? PrivateMode { get; set; }

        public bool? PublicMode { get; set; }

        public string? AvatarLink { get; set; }

    }
    public class UpdateBasicRequest
    {
        public string? Phone { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string? FullName { get; set; }


        public string? AvatarLink { get; set; }

    }


    public class EnableDisableModeRequest
    {

        public bool? WorkMode { get; set; }
        public bool? SearchMode { get; set; }



    }




}
