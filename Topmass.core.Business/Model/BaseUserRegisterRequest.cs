namespace Topmass.core.Business.Model
{
    public class BaseUserRegisterRequest
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
