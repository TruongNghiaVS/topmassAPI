namespace Topmass.Admin.Pages

{

    public class ActiveStatusControl : SelectControl
    {
        public ActiveStatusControl()
        {
            Lable = "Trạng thái";
            DataSource = new List<dynamic>()
            {
                new  {id = "0", text = "Không hoạt động"
                },
                 new  {id = "1", text = "Hoạt động"
                },
            };
        }
    }
}