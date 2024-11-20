namespace Topmass.Core.Model.JobAply
{
    public class JobApply : BaseModel
    {
        public int JobId { get; set; }
        public int CVId { get; set; }

        public int ViewMode { get; set; }


        public string FullName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public JobApply()
        {

        }
    }
}
