namespace Topmass.Recruiter.Repository
{
    public class RecruiterRepAdd
    {

        public string? UserName { get; set; }
        public string? Pass { get; set; }
        public string? TaxCode { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }

        public int CreatedBy { get; set; }
    }



    public class AuthenRepSearch
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class ActiveCodeRecruiterRepAdd
    {
        public string TaxCode { get; set; }
        public string Email { get; set; }

        public int? UserId { get; set; }


    }


}
