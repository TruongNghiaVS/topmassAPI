namespace Topmass.Admin.Business.Model
{

    public class SearchNTDBusinessReponse
    {

        public List<NTDItemDisplay> Data { get; set; }

        public SearchNTDBusinessReponse()
        {
            Data = new List<NTDItemDisplay>();
        }
    }

    public class GetDetailNTDReponse
    {

        public dynamic Data { get; set; }
    }

    public class JobAdminDetail
    {
        public string TextTimeWorking { get; set; }
        public dynamic DataBasic { get; set; }

        public dynamic InfoBasic { get; set; }

        public string SalaryText { get; set; }
        public string UnitText { get; set; }
        public string AddressDetail { get; set; }

    }


}
