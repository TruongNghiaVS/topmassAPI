namespace Topmass.CV.Business.Model
{
    public class ResumeItemDisplay
    {
        public string Phone { get; set; }

        public int CVId { get; set; }

        public int UserId { get; set; }

        public int TemplateId { get; set; }

        public string FullName { get; set; }
        public DateTime CreateAt { get; set; }
        public string Email { get; set; }
        public string? LinkCV { get; set; }
        public string StatusText { get; set; }

        public ResumeItemDisplay()
        {
            TemplateId = 1;
        }


    }
}
