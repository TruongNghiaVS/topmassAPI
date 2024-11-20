namespace Topmass.Admin.Pages

{
    public class SelectMutipleControl : ControlItem
    {
        public List<dynamic> DataSource { get; set; }
        public SelectMutipleControl()
        {
            Type = 8;
            DataSource = new List<dynamic>();
        }

    }



}