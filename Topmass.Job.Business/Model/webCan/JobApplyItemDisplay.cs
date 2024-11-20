namespace Topmass.Job.Business.Model.webCan
{
    public class JobApplyItemDisplay : BaseItemProductDisplay
    {

        public string ReasonText { get; set; }

        public int ReasonId { get; set; }

        public string Note { get; set; }

        public string LinkFile { get; set; }
        public JobApplyItemDisplay()
        {

        }
    }


    public class GetAllCVApplyReponse
    {
        public List<JobApplyItemDisplay> Data { get; set; }
    }




}
