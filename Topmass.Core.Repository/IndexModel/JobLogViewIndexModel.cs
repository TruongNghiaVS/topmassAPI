namespace Topmass.Core.Repository.IndexModel
{
    public class JobLogViewIndexModel
    {
        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public DateTime CreateAt { get; set; }

        public DateTime? Dob { get; set; }
        public int Id { get; set; }
        public string ExtraText { get; set; }

        public JobLogViewIndexModel()
        {
            ExtraText = "";
        }


    }
}
