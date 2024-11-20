namespace Topmass.Core.Model.CV
{
    public class Resume : BaseModel
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public int TemplateId { get; set; }

        public int TypeData { get; set; }

        public string? DataInput { get; set; }

        public string? LinkFile { get; set; }
    }

    public class ResumeUI : BaseModel
    {
        public string Name { get; set; }
        public string CoverImage { get; set; }



    }

    public enum ResumeType
    {
        NONE = 0,
        UPLOAD = 1,
        GENNERATE = 2
    }
}
