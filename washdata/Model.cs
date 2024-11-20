namespace washdata
{
    public class WashDataItem
    {

        public string NoAgree { get; set; }
        public string Phone { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Phone3 { get; set; }
    }

    public class CreateTokenReponse
    {
        public string Status { get; set; }

        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }

    }


    public class WashDataDBItem
    {
        public DateTime Calldate { get; set; }
        public string Src { get; set; }
        public string Dcontext { get; set; }
        public string Disposition { get; set; }
        public int Duration { get; set; }
        public int Billsec { get; set; }
    }
}
