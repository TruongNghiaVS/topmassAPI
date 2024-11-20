namespace Topmass.Core.Model
{
    public class ForgetPasswordModel : BaseModel
    {
        public ForgetPasswordModel()
        {
            this.Table = "ForgetPassword";
        }
        public int? UserId { get; set; }

        public string? Code { get; set; }

        public int? TypeUser { get; set; }

        public int? SendMailStatus { get; set; }

        public string? Email { get; set; }

    }
}
