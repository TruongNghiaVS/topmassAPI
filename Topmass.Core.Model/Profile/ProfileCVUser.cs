namespace Topmass.Core.Model.Profile
{
    public class ProfileCVUser : BaseModel
    {
        public string? FullName { get; set; }

        public string? Position { get; set; }

        public string? Level { get; set; }

        public int Gender { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Introduction { get; set; }

        public DateTime? DateOfBirth { get; set; }


        public string? AvatarLink { get; set; }

        public string? AddressInfo { get; set; }

        public int RelId { get; set; }

        public ProfileCVUser()
        {
            FullName = "";
            Position = "";
            Level = "";
            Gender = -1;
            Email = "";
            PhoneNumber = "";
            Introduction = "";
            AvatarLink = "";
            RelId = -1;

        }

    }

}
