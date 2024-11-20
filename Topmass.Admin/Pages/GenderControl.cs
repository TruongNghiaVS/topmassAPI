namespace Topmass.Admin.Pages

{
    public class GenderControl : ControlItem
    {
        public bool DisplayAll { get; set; }
        public List<dynamic> DataSource { get; set; }
        public GenderControl()
        {
            Type = 10;
            DisplayAll = false;
            DataSource = new List<dynamic>();
            
        }

    }



}