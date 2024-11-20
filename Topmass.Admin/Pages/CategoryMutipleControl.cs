namespace Topmass.Admin.Pages

{
    public class CategoryMutipleControl : ControlItem
    {
        public List<dynamic> DataSource { get; set; }
        public CategoryMutipleControl()
        {
            Type = 9;
            DataSource = new List<dynamic>();
        }

    }



}