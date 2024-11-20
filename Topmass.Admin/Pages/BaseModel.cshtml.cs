
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Topmass.Admin.Pages.Model;

namespace Topmass.Admin
{
    [Authorize]
    public class BaseModel : PageModel
    {
        public PermisionUser Permision { get; set; }
        public string TitlePage { get; set; }
        public string KeyPage { get; set; }
        public string NameController { get; set; }
        public string TitleList { get; set; }
        public int? RecordSource { get; set; }
        public List<string> TableColumnText { get; set; }
        public string PhoneToXXX(string phone)
        {
            if (string.IsNullOrEmpty(phone))
            {
                return "";
            }
            var text = phone.Remove(0, 7);
            var textPhone = "xxxxxxx" + text;
            return textPhone;
        }
        public UserDataView UserData
        {
            get; set;
        }
        public BaseModel()
        {
            UserData = new UserDataView()
            {
                UserId = -1
            };
            Permision = new PermisionUser();

        }
        public void GetInfoUser()
        {


        }

    }
}
