namespace Topmass.Business.History.model
{
    public class HIstoryDataDisplay
    {
        public string GroupDate { get; set; }

        public List<HIstoryDataItem> Data { get; set; }

        public HIstoryDataDisplay()
        {
            GroupDate = "";
            Data = new List<HIstoryDataItem>();
        }


    }

    public class HIstoryDataItem
    {
        public DateTime? TimeBusiness { get; set; }

        public string Content { get; set; }

        public string TimeText { get; set; }


        public string DateText
        {
            get
            {
                if (TimeBusiness.HasValue)
                {
                    return TimeBusiness.Value.Hour + ":" + TimeBusiness.Value.Minute;
                }
                return "00:00";
            }
        }
        public HIstoryDataItem()
        {
            Content = "";
            TimeText = "00:00";

        }
    }
}
