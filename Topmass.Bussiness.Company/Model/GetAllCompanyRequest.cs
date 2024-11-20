using Topmass.Core.Model;

namespace Topmass.Bussiness.Company.Model
{
    public class GetAllCompanyRequest
    {

        public int UserId { get; set; }

        public bool LoadJob { get; set; }

        public string Keyword { get; set; }

        public GetAllCompanyRequest()
        {
            LoadJob = false;

        }

    }

    public class GetAllCompanyReponse
    {
        public List<CompanyItemDisplay> Data { get; set; }

    }


    public class GetCompanyBySlugRequest
    {
        public string Slug { get; set; }
    }

    public class GetCompanyBySlugReponse
    {
        public CompanyInfoModel Data { get; set; }
    }
    public class GetInfomationDetailReponse
    {
        public CompanyDetailDisplay Data { get; set; }

    }

    public class CompanyDetailDisplay
    {
        public string Logo { get; set; }

        public string CoverImage { get; set; }
        public bool IsFollow { get; set; }
        public string CoverFullLink
        {
            get
            {
                if (string.IsNullOrEmpty(CoverImage))
                {
                    return "";
                }
                return "https://www.cdn.topmass.vn/static/" + CoverImage;
            }

        }



        public string LogoFullLink
        {
            get
            {
                if (string.IsNullOrEmpty(Logo))
                {
                    return "/imgs/logo-work.png";
                }
                return "https://www.cdn.topmass.vn/static/" + Logo;
            }

        }

        public string Introduction { get; set; }

        public string MapInfo { get; set; }

        public string AddressInfo { get; set; }

        public string Email { get; set; }

        public string Website { get; set; }

        public int CountFollow { get; set; }

        public string Capacity { get; set; }

        public string Name { get; set; }


    }






}
