namespace Topmass.core.Business
{
    public class CertifyUserInfoReponse
    {

        public bool IsSave { get; set; }

        public CertifyUserInfoReponse()
        {
            IsSave = true;
        }

    }

    public class CertifyUserInfoUpdateReponse
    {


    }

    public class CertifyUserInfoDeleteRequest
    {
        public int Id { get; set; }


    }

    public class CertifyUserInfoUpdateRequest : CertifyUserInfoRequest
    {

        public int Id { get; set; }
    }

    public class CertifyUserInfoRequest
    {

        public int UserId { get; set; }
        public int TypeData { get; set; }


        public string? FullName { get; set; }

        public string? CompanyName { get; set; }

        public int MonthGet { get; set; }

        public int YearGet { get; set; }

        public string Introduction { get; set; }

        public string LinkFile { get; set; }

        public string? MonthExpired { get; set; }
        public string? YearExpired { get; set; }
        public bool IsExpired { get; set; }
        public CertifyUserInfoRequest()
        {
            IsExpired = false;
            MonthExpired = "0";
            YearExpired = "0";
        }
    }



    public class CertifyUserGetAllResult
    {

        public List<CertifyUserDisplayItem> Data { get; set; }


    }



    public class CertifyUserDisplayItem

    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? CompanyName { get; set; }
        public int MonthGet { get; set; }
        public int YearGet { get; set; }
        public string Introduction { get; set; }
        public string LinkFile { get; set; }
        public int RelId { get; set; }
        public int MonthExpired { get; set; }
        public int YearExpired { get; set; }
        public CertifyUserDisplayItem()
        {

        }
    }


}
