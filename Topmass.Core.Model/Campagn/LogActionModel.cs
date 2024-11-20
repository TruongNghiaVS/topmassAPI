namespace Topmass.Core.Model.Campagn
{
    public class LogActionModel : BaseModel
    {
        public int UserId { get; set; }
        public string Content { get; set; }
        public DateTime? BusinessTime { get; set; }
        public int TypeData { get; set; }
        public int Actor { get; set; }
        public int Source { get; set; }



        public LogActionModel()
        {
            TypeData = -1;
            Actor = 1;
            Source = 1;
            BusinessTime = DateTime.Now;
        }

    }

}
