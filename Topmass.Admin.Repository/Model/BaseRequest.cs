namespace Topmass.Admin.Repository
{
    public interface IBaseRequest
    {
        public int Limit { get; set; }

        public int Page { get; set; }

        public DateTime? From { get; set; }

        public DateTime? To { get; set; }

        public string? Token { get; set; }

        public string? OrderBy { get; set; }
    }
    public class BaseRequest : IBaseRequest
    {
        public int Limit { get; set; }

        public int Page { get; set; }

        public DateTime? From { get; set; }

        public DateTime? To { get; set; }

        public string? Token { get; set; }

        public string? OrderBy { get; set; }

        public int? Status { get; set; }


    }

   
}
