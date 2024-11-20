namespace Topmass.Bussiness.Company.Model
{

    public class BaseJobItem
    {
        public DateTime? BusinessDate { get; set; }

        public string CompanyName { get; set; }

        public string FieldArray { get; set; }
        protected string ShortLinkLogo { get; set; }
        public string LogoImage
        {
            get
            {
                if (string.IsNullOrEmpty(ShortLinkLogo))
                {
                    return "";
                }
                return "https://www.cdn.topmass.vn/static/" + ShortLinkLogo;

            }
        }

        public int JobId { get; set; }

        public string JobSlug { get; set; }



        public int Id { get; set; }

        public DateTime? LastUpdate { get; set; }

        public string PositionText { get; set; }

        public string LocationText { get; set; }
        public bool IsLike { get; set; }

        public bool IsSave { get; set; }

        public bool IsApply { get; set; }
        protected int? TypeMoney { get; set; }
        public string CurrencyCode
        {
            get
            {
                if (TypeMoney.HasValue == false)
                {
                    return "0";
                }
                return TypeMoney.Value == 0 ? "0" : "1";
            }
        }

        public int SalaryFrom { get; set; }

        public int SalaryTo { get; set; }
        public bool? Aggrement { get; set; }
        public BaseJobItem()
        {
            TypeMoney = 0;
            Aggrement = false;
            FieldArray = "";

        }

    }
    public class CompanyItemDisplay
    {

        public List<BaseJobItem> DataJob { get; set; }

        public string CoverLink { get; set; }

        public bool Isfollow
        {
            get
            {
                return ValidFollow > 0 ? true : false;
            }
        }

        protected int ValidFollow { get; set; }

        public string CoverFullLink
        {
            get
            {
                if (string.IsNullOrEmpty(CoverLink))
                {
                    return "";
                }
                return "https://www.cdn.topmass.vn/static/" + CoverLink;
            }

        }

        protected string LogoLink { get; set; }

        public string LogoFullLink
        {
            get
            {
                if (string.IsNullOrEmpty(LogoLink))
                {
                    return "/imgs/logo-work.png";
                }
                return "https://www.cdn.topmass.vn/static/" + LogoLink;
            }

        }

        public string FullName { get; set; }

        public string Slug { get; set; }

        public int id { get; set; }

        public int FollowCount { get; set; }


        public CompanyItemDisplay()
        {
            DataJob = new List<BaseJobItem>();
        }

    }

    public class JobCompanyItemDisplay
    {
        public string JobName { get; set; }

        public string CompanyName { get; set; }



        protected string AvatarLink { get; set; }

        public bool IsJobSave { get; set; }
        public bool IsJobApply { get; set; }

        public string FullLink
        {
            get
            {
                if (string.IsNullOrEmpty(AvatarLink))
                {
                    return "";
                }
                return "https://www.cdn.topmass.vn/static/" + AvatarLink;
            }
        }

        public string RangeSalary { get; set; }

        public string LocationText { get; set; }

        public DateTime? DateExpried { get; set; }

        public string DayRemainApply
        {

            get
            {
                if (DateExpried.HasValue)
                {
                    if (DateExpried.Value < DateTime.Now)
                    {
                        return "Đã quá hạn ứng tuyển";
                    }
                    else
                    {
                        return "Còn " + ((DateTime.Now - DateExpried.Value).TotalDays + 1) + " ngày để ứng tuyển";
                    }

                }
                return "Chưa cập nhật ";
            }
        }
        public int StatusCode
        {
            get
            {
                if (DateExpried.HasValue)
                {
                    if (DateExpried.Value < DateTime.Now)
                    {
                        return 2;
                    }
                    else
                    {
                        return 1;
                    }

                }
                return 0;
            }
        }
        public string Slug { get; set; }
        public int JobId { get; set; }
        public DateTime? BusinessDate { get; set; }
        public string FieldArray { get; set; }
        protected string ShortLinkLogo { get; set; }

        public string JobSlug { get; set; }
        public int SalaryFrom { get; set; }
        public int SalaryTo { get; set; }
        public int Id { get; set; }
        public DateTime? LastUpdate { get; set; }
        public string PositionText { get; set; }
        public bool IsLike { get; set; }

        public bool IsSave { get; set; }

        public bool IsApply { get; set; }
        protected int? TypeMoney { get; set; }
        public string CurrencyCode
        {
            get
            {
                if (TypeMoney.HasValue == false)
                {
                    return "0";
                }
                return TypeMoney.Value == 0 ? "0" : "1";
            }
        }

        public bool? Aggrement { get; set; }

        public string LogoImage
        {
            get
            {
                if (string.IsNullOrEmpty(ShortLinkLogo))
                {
                    return "";
                }
                return "https://www.cdn.topmass.vn/static/" + ShortLinkLogo;

            }
        }
        public JobCompanyItemDisplay()
        {
            IsJobApply = false;
            IsJobSave = false;
            TypeMoney = 0;
            Aggrement = false;
            FieldArray = "";
        }

    }


    public class JobIdCount
    {
        public int JobId { get; set; }
    }

}
