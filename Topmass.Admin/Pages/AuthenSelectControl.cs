namespace Topmass.Admin.Pages

{

    public class AuthenSelectControl : SelectControl
    {
        public AuthenSelectControl()
        {
            Lable = "Cấp độ xác thực";
            DataSource = new List<dynamic>()
            {
            new  {id = "0", text = "Chưa xác thực email"
            },
                new  {id = "1", text = "Cấp độ 1"
            },
                new  {id = "2", text = "Cấp độ 2"
            },
                new  {id = "3", text = "Cấp độ 3"
            }
            };
        }
    }


}