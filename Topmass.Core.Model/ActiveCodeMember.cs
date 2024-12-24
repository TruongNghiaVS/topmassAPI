namespace Topmass.Core.Model
{
    public class ActiveCodeMember : BaseModel
    {

        public string Email { get; set; }
        public string? Code { get; set; }
        public int? UserId { get; set; }


    }
}
