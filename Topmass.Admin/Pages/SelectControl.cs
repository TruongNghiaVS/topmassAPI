namespace Topmass.Admin.Pages
{
    public class SelectControl : ControlItem
    {
        public List<dynamic> DataSource { get; set; }
        public bool DisplayAll { get; set; }
        public SelectControl()
        {
            Type = 7;
            DisplayAll = false;
            DataSource = new List<dynamic>();
        }
    }
}