namespace Topmass.CV.Repository.Model
{
    public class JobApplyDisplayItem
    {
        public int Id { get; set; }
        public string Phone { get; set; }

        public string FullName { get; set; }
        public string Email { get; set; }
        public int StatusCode { get; set; }
        public int CVId { get; set; }
        public string StatusText { get; set; }
        public int JobId { get; set; }
        public string JobName { get; set; }
        public string CampagnText { get; set; }
        public int CampagnId { get; set; }
        public DateTime CreateAt { get; set; }
        public string LinkFile { get; set; }

        public string AvatarLink { get; set; }
        public int? SearchId { get; set; }

        protected bool Lockinfo { get; set; }
        public int ViewMode { get; set; }
        public string ViewModeText
        {
            get
            {
                if (ViewMode == 0)
                {
                    return "";
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
        public JobApplyDisplayItem()
        {

        }


    }




    public class JobApplyDisplayItemSearchCV
    {
        public int Id { get; set; }

        protected string PhoneInput { get; set; }
        protected string EmailInput { get; set; }

        protected string FileCVHide { get; set; }
        protected string FileCV { get; set; }

        public int SourceType { get; set; }
        public string Phone
        {
            get
            {
                if (Lockinfo == true)
                {
                    if (string.IsNullOrEmpty(PhoneInput) || PhoneInput.Length < 10)
                    {
                        return "xxxxxxxxxx";
                    }

                    return "0xxxxxxxxx";
                }
                return PhoneInput;

            }
        }
        public string Email
        {
            get
            {
                if (Lockinfo == true)
                {
                    if (string.IsNullOrEmpty(EmailInput) || EmailInput.Length < 5)
                    {
                        return "xxx@gmail.com";
                    }

                    return "xxxxxxx@gmail.com";
                }
                return EmailInput;

            }
        }

        public string FullName { get; set; }


        public int StatusCode { get; set; }

        public int CVId { get; set; }
        public string StatusText { get; set; }

        public int JobId { get; set; }

        public string JobName { get; set; }
        public string CampagnText { get; set; }

        public int CampagnId { get; set; }

        public DateTime CreateAt { get; set; }

        protected bool CheckURLValid(string source)
        {
            Uri uriResult;
            return Uri.TryCreate(source, UriKind.Absolute, out uriResult) && uriResult.Scheme == Uri.UriSchemeHttp;
        }
        public string GetFullLink(string linkfile)
        {

            if (string.IsNullOrEmpty(linkfile))
            {
                return "";
            }
            if (linkfile.ToLower().Contains("cdn.topmass.vn"))
            {

                return linkfile;
            }

            linkfile = linkfile.Replace("\\", "/");

            return "https://www.cdn.topmass.vn/static/" + linkfile;
        }

        public string LinkFile
        {
            get
            {
                if (Lockinfo == true)
                {
                    return GetFullLink(FileCVHide);
                }
                return GetFullLink(FileCV);

            }
        }

        protected bool Lockinfo { get; set; }
        public int ViewMode
        {
            get
            {
                if (Lockinfo == true)
                    return 0;
                return 1;
            }
        }
        public string AvatarLink { get; set; }
        public int? SearchId { get; set; }

        public string ViewModeText
        {
            get
            {
                if (ViewMode == 0)
                {
                    return "";
                }
                else
                {
                    return "Đã xem";
                }

            }
        }

        public int Point { get; set; }

        public bool IsOpenedCV
        {
            get
            {
                return !Lockinfo;
            }
        }
        public JobApplyDisplayItemSearchCV()
        {
            StatusText = "Lưu CV";
            Point = 2;
        }



    }

    public class ShortCVManagentInfoDisplayItem
    {
        public int Id { get; set; }
        public string Phone { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public int StatusCode { get; set; }
        public int CVId { get; set; }
        public string StatusText { get; set; }
        public int JobId { get; set; }
        public string JobName { get; set; }
        public string CampagnText { get; set; }
        public int CampagnId { get; set; }
        public DateTime CreateAt { get; set; }
        public string LinkFile
        {
            get
            {
                return GetFullLink(LinkFile1);
            }
        }
        protected string LinkFile1 { get; set; }


        public string GetFullLink(string linkfile)
        {

            if (string.IsNullOrEmpty(linkfile))
            {
                return "";
            }
            if (linkfile.ToLower().Contains("cdn.topmass.vn"))
            {

                return linkfile;
            }

            linkfile = linkfile.Replace("\\", "/");

            return "https://www.cdn.topmass.vn/static/" + linkfile;
        }
        protected bool Lockinfo { get; set; }

        public string AvatarLink { get; set; }

        public ShortCVManagentInfoDisplayItem()
        {


        }


    }

}
