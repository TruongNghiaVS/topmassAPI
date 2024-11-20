namespace Topmass.Admin.Business.Model
{

    public class NTDItemDisplay
    {
        public string Code { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Taxcode { get; set; }
        public DateTime? DateRegister { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int Status { get; set; }

        public string StatusText
        {
            get
            {
                if (Status == 0)
                {
                    return "Không hoạt động";
                }
                return "Hoạt động";
            }
        }
        public DateTime? CreateAt { get; set; }

        public string CompanyName { get; set; }

        public DateTime? UpdateAt { get; set; }

        public string AuthenText { get; set; }
    }

    public class ControlOptionDisplay
    {
        public int id { get; set; }
        public string text { get; set; }


    }

}
