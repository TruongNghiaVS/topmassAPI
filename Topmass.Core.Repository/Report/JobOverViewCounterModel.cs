using Topmass.Core.Model;

namespace Topmass.Core.Repository.Report
{
    public class JobOverViewCounterModel : BaseModel
    {
        public DateTime DayReport { get; set; }
        public int TotalViewer { get; set; }
        public int TotalApply { get; set; }
        public int JobId { get; set; }
    }
}
