namespace Topmass.Core.Repository.IndexModel
{
    public class JobOverViewCounterRequest
    {
        public DateTime? From { get; set; }


        public DateTime? To { get; set; }

        public int JobId { get; set; }



        public JobOverViewCounterRequest()
        {


        }

    }


    public class JobOverViewCounterReponse
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public int JobId { get; set; }

        public string JobName { get; set; }

        public int TotalViewer { get; set; }

        public int TotalApply { get; set; }

        public string StatusText { get; set; }

        public int Status { get; set; }

        public List<JobOverViewCounterDisplay> Data { get; set; }
        public JobOverViewCounterReponse()
        {
        }

    }


    public class JobOverViewCounterDisplay
    {
        public DateTime DayReport { get; set; }

        public int TotalViewer { get; set; }
        public int TotalApply { get; set; }



    }


}
