namespace Topmass.Campagn.Repository.Model
{
    public class CampangnSearchReponse
    {

        public int Total { get; set; }

        public int Page { get; set; }

        public List<CampagnItemDisplay> Data { get; set; }

        public CampangnSearchReponse()
        {
            Total = 0;
            Data = new List<CampagnItemDisplay>();
        }
    }



    public class CampangnSearchJobReponse
    {

        public int Total { get; set; }

        public int Page { get; set; }

        public List<JobItemDisplay> Data { get; set; }

        public CampangnSearchJobReponse()
        {
            Total = 0;
            Data = new List<JobItemDisplay>();
        }
    }
}
