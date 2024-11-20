namespace Topmass.Recruiter.Model
{



    public class InputExchangeCVRequest
    {
        public string Title { get; set; }
        public string Position { get; set; }
        public string Rank { get; set; }
        public string Experience { get; set; }
        public List<string> LinkCVs { get; set; }
        public InputExchangeCVRequest()
        {
            Experience = string.Empty;
            Rank = string.Empty;
        }
    }

    public class InputLinkFileExchangeCV
    {
        public string LinkFile { get; set; }

    }


    public class InputExchangeSearchRequest
    {
        public int Status { get; set; }
        public InputExchangeSearchRequest()
        {
            Status = -1;
        }
    }


    public class InputExchangeDetail
    {
        public int Id { get; set; }
        public InputExchangeDetail()
        {

        }
    }



}
