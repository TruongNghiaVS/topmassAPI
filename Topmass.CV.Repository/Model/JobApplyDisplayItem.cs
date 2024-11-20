namespace Topmass.CV.Repository.Model
{
    public class JobApplyDisplayItem
    {
        public int Id { get; set; }
        public string Phone { get; set; }

        public string FullName { get; set; }
        public string Email { get; set; }
        public int StatusCode { get; set; }

        public int CVId { get; set; }
        public string StatusText { get; set; }

        public int JobId { get; set; }

        public string JobName { get; set; }
        public string CampagnText { get; set; }

        public int CampagnId { get; set; }

        public DateTime CreateAt { get; set; }

        public string LinkFile { get; set; }

        public int ViewMode { get; set; }
        public string AvatarLink { get; set; }

        public string ViewModeText
        {
            get
            {
                if (ViewMode == 0)
                {
                    return "";
                }
                else
                {
                    return "Đã xem";
                }

            }
        }
        public JobApplyDisplayItem()
        {
            StatusText = "Nộp CV";
        }
    }
}
