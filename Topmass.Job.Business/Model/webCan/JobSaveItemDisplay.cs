namespace Topmass.Job.Business.Model.webCan
{

    public interface IBaseItemProductDisplay
    {
        public DateTime? BusinessDate { get; set; }

        public string CompanyName { get; set; }

        public string FieldArray { get; set; }

        public string LogoImage { get; }

        public int JobId { get; set; }

        public string JobSlug { get; set; }


        public int SalaryFrom { get; set; }

        public int SalaryTo { get; set; }

        public int Id { get; set; }

        public DateTime? LastUpdate { get; set; }

        public string PositionText { get; set; }

        public string LocationText { get; set; }

        public bool IsLike { get; set; }

        public bool IsSave { get; set; }

        public bool IsApply { get; set; }
        public string CurrencyCode { get; }

        public bool? Aggrement { get; set; }


    }

    public class BaseItemProductDisplay : IBaseItemProductDisplay
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
        public BaseItemProductDisplay()
        {
            TypeMoney = 0;
            Aggrement = false;
            FieldArray = "";

        }


    }
    public class JobSaveItemDisplay : BaseItemProductDisplay
    {




    }


    public class GetAllJpobSaveReponse
    {
        public List<JobSaveItemDisplay> Data { get; set; }
    }
}
