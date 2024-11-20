namespace Topmass.Core.Model
{
    public class BaseUserInfo : BaseModel
    {
        public string? FirstName { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public DateTime? DateRegister { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
