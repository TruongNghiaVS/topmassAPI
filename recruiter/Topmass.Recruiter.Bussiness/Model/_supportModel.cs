namespace Topmass.Recruiter.Bussiness.Model
{
    public class TicketItemAdd
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? LinkFile { get; set; }
        public int HandleBy { get; set; }
        public TicketItemAdd()
        {
            HandleBy = -1;
        }

    }


    public class PageContactRequest
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        public string? Title { get; set; }

        public string? Content { get; set; }

        public PageContactRequest()
        {

        }

    }
}
