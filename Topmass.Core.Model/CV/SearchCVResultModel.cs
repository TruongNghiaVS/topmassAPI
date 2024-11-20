namespace Topmass.Core.Model.CV
{
    public class SearchCVResultModel : BaseModel
    {
        public SearchCVResultModel()
        {
        }
        public int SearchId { get; set; }
        public int CampaignId { get; set; }
        public int RelId { get; set; }
        public int Jobid { get; set; }
        public string LinkFile { get; set; }



    }
}
