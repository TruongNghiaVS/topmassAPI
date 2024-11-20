namespace Topmass.Admin.Pages.Model
{
    public class UpdatedocumentChangeRequest
    {
        public int Id { get; set; }
        public string StatusChange { get; set; }
        public string NotedChange { get; set; }

        public int? ReasonReject { get; set; }

        public string Linkfile { get; set; }
    }

    public class UpdateInfoHuman
    {
        public int Id { get; set; }
        public int ConfirmAccout { get; set; }
        public int StatusAccount { get; set; }

        public string? Noted { get; set; }

        public int? ReasonLock { get; set; }


    }

    public class UpdatePersonalPerson
    {
        public int Id { get; set; }
        public string? Phone { get; set; }
        public int Gender { get; set; }

        public string? fullName { get; set; }
    }
    public class UpdateConfirmStatusJobRequest
    {
        public int Id { get; set; }
        public int StatusChange { get; set; }
        public string NotedChange { get; set; }
    }

}
