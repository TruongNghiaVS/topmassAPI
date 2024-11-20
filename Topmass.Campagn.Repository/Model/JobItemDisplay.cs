namespace Topmass.Campagn.Repository.Model
{
    public class JobItemDisplay
    {
        public JobItemDisplay()
        {
            TotalRecord = 0;
        }
        public string Name { get; set; }
        public int TotalRecord { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        protected int Status { get; set; }
        public string StatusText
        {
            get
            {

                return "";

            }
        }



    }
}
