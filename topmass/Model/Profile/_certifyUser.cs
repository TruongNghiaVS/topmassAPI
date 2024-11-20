namespace topmass.Model
{

    public class InputCertifyUserInfoUpdateRequest : InputCertifyUserRequestAdd
    {

    }
    public class InputCertifyUserRequestAdd
    {

        public int Id { get; set; }





        public string? FullName { get; set; }

        public string? CompanyName { get; set; }

        public int MonthGet { get; set; }

        public int YearGet { get; set; }


        public string? MonthExpired { get; set; }
        public string? YearExpired { get; set; }
        public bool IsExpired { get; set; }
        public string Introduction { get; set; }

        public string? LinkFile { get; set; }

        public InputCertifyUserRequestAdd()
        {
            MonthExpired = "0";
            YearExpired = "0";
            IsExpired = false;
        }



    }






}
