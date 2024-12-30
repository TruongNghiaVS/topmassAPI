namespace Topmass.Core.Model.CV
{
    public class OpenViewerResult : BaseModel
    {
        public OpenViewerResult()
        {
        }
        public int ViewId { get; set; }
        public int RelId { get; set; }
        public bool? LockInfo { get; set; }
        public int? TranSactionId { get; set; }
        public DateTime? BussinessTime { get; set; }
    }
}
