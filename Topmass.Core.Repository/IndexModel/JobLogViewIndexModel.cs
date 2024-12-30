namespace Topmass.Core.Repository.IndexModel
{
    public class JobLogViewIndexModel
    {
        public string FullName { get; set; }

        public string PhoneNumber
        {
            get
            {
                if (ViewMode == 0)
                {
                    return "0xxxxxxxxx";
                }
                return PhoneNumberInput;

            }
        }

        public string StatusText { get; set; }

        public int Status { get; set; }

        protected string PhoneNumberInput
        {
            get; set;
        }
        protected string EmailInput { get; set; }

        public string Email
        {
            get
            {

                if (ViewMode == 0)
                {
                    return "xxxxx@gmail.com";
                }
                return PhoneNumberInput;

            }
        }

        public DateTime CreateAt { get; set; }

        public DateTime? Dob { get; set; }
        public int Id { get; set; }
        public string ExtraText { get; set; }


        protected bool Lockinfo
        {
            get
            {
                if (ViewMode == 0)
                {
                    return true;
                }
                return false;
            }
        }
        public int ViewMode { get; set; }
        public string ViewModeText
        {
            get
            {
                if (ViewMode == 0)
                {
                    return "Chưa mở";
                }
                else
                {
                    return "Đã mở";
                }

            }
        }
        public bool IsOpenedCV
        {
            get
            {
                return !Lockinfo;
            }
        }


        public JobLogViewIndexModel()
        {


        }


    }
}
