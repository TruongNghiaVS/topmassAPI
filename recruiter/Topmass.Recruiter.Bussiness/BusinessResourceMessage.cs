namespace Topmass.Recruiter.Bussiness
{
    //collect message
    public class BusinessResourceMessage
    {
        private static BusinessResourceMessage message = null;
        public BusinessResourceMessage()
        {


        }

        public static BusinessResourceMessage GetMessage()
        {
            if (message == null)
                message = new BusinessResourceMessage();
            return message;
        }


        //sucesss
        public string SuccessfullAuthenMsg = "Đăng nhập thành công";
        public string SuccessAuthenCreateToken = "Tạo token thành công";


        // error

        public string Missing_param = "Thiếu tham số";
        public string Missing_param_Email = "Thiếu thông tin email hoặc mật khẩu";
        public string Missing_param_Name = "Thiếu thông tin họ và tên";
        public string Missing_param_PhoneNumber = "Thiếu thông tin số điện thoại";

        public string Email_notExit_System = "Email không tìm thấy trong hệ thống";
        // authen
        public string Authen_NotFoundAccout = "Không tồn tại, đăng nhập thất bại";
        public string Authen_NotActive = "Bạn chưa  kích hoạt tài khoản, vui lòng kiểm tra lại email và làm theo hướng dẫn";

        public string Message_CheckMail = "Vui lòng kiểm tra email,và làm theo hướng dẫn";
        public string ValidLinkFail = "Không đúng code yêu cầu";

        public string NotExitAccount = "Không tìm thấy tài khoản";

        public string ChangePasswordSuccess = "Đổi mật khẩu thành công";
    }

}
