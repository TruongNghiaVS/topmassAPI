

namespace Topmass.core.Business.MessageError
{
    public class ErrorMessage
    {
        private static ErrorMessage errorMessage;
        public ErrorMessage()
        {

        }
        public static ErrorMessage GetErrorMessage()
        {
            if (errorMessage == null)
            {

                errorMessage = new ErrorMessage();
            }
            return errorMessage;

        }
        // deffine Error
        public string Msg_user_misingParamr = "Thiếu tham số, vui lòng cung cấp thông tin tên đăng nhập hoặc mật khẩu";

    }
}
