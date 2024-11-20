namespace Topmass.core.Business
{

    public class BaseResult
    {
        public BaseResult()
        {

            DataEror = new List<ItemError>();
        }
        public bool Success
        {
            get
            {
                return !DataEror.Any() ? true : false;
            }

        }

        public int StatusCode
        {
            get
            {
                return Success == true ? 200 : 302;
            }

        }


        private bool _successTemp { get; set; }

        public string Message { get; set; }
        public dynamic Data { get; set; }
        public List<ItemError> DataEror { get; set; }

        public void AddError(string errorCode, string contentMessage)
        {
            var itemError = new ItemError()
            {
                ErrorCode = errorCode,
                ErrorMesage = contentMessage
            };

            var item = DataEror.Where(x => x.ErrorCode == errorCode).FirstOrDefault();

            if (item == null)
            {
                DataEror.Add(itemError);
                return;
            }
            item.ErrorMesage = contentMessage;
        }

        public void AddError(string contentMessage)
        {
            var itemError = new ItemError()
            {
                ErrorCode = "",
                ErrorMesage = contentMessage
            };


            DataEror.Add(itemError);

        }


    }

    public class ItemError
    {
        public string ErrorCode { get; set; }
        public string ErrorMesage { get; set; }

    }
    public class LoginResult : BaseResult
    {
        public string Token { get; set; }

        public LoginResult()
        {


        }
    }
}
