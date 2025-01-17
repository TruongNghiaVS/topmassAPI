namespace TopMass.Core.Result
{
    public class BaseResult
    {

        public int ErrorCode { get; set; }
        public BaseResult()
        {

            DataEror = new List<ItemError>();
        }
        public bool Success
        {
            get
            {
                var successResult = !DataEror.Any() ? true : false;

                if (successResult == true && string.IsNullOrEmpty(Message) == true)
                {
                    return true;
                }
                return false;
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


    public class DataResult
    {
        public DataResult()
        {


        }
        public bool Success
        {
            get
            {
                return string.IsNullOrEmpty(Message);
            }

        }

        public int StatusCode
        {
            get
            {
                return string.IsNullOrEmpty(Message) == true ? 200 : 302;
            }

        }




        private bool _successTemp { get; set; }

        public string Message { get; set; }
        public dynamic Data { get; set; }





    }
    public class ItemError
    {
        public string ErrorCode { get; set; }
        public string ErrorMesage { get; set; }

    }

    public class BaseResultAdd

    {
        public BaseResultAdd()
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
        public string Message { get; set; }
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
    }

}
