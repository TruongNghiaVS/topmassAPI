namespace Topmass.Core.Repository.IndexModel
{
    public class JobLogViewIndexModel
    {
        public string FullName { get; set; }

        public string PhoneNumber
        {
            get
            {

                return "0xxxxxxxxx";

            }
        }

        protected string PhoneNumberInput
        {
            get; set;
        }
        protected string EmailInput { get; set; }

        public string Email { get { return "xxxxxx@gmail.com"; } }

        public DateTime CreateAt { get; set; }

        public DateTime? Dob { get; set; }
        public int Id { get; set; }
        public string ExtraText { get; set; }


        protected bool Lockinfo { get; set; }
        public int ViewMode { get; set; }
        public string ViewModeText
        {
            get
            {
                if (ViewMode == 0)
                {
                    return "Chưa xem";
                }
                else
                {
                    return "Đã xem";
                }

            }
        }
        public bool IsOpenedCV
        {
            get
            {
                return Lockinfo;
            }
        }


        public JobLogViewIndexModel()
        {
            ExtraText = "";
            ViewMode = 0;
            Lockinfo = false;
        }


    }
}
