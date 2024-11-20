namespace washDataCheck
{

    public class InputCase
    {
        public string Phone { get; set; }

        public string NoAgree { get; set; }

        public string CustomerName { get; set; }
    }

    public class InputCaseReponse
    {
        public string Phone { get; set; }
        public string Disposition { get; set; }
        public int Duration { get; set; }
        public InputCaseReponse()
        {

        }
    }

    public class DataGlobal
    {

        public List<InputCase> DataInput { get; set; }
        public List<InputCaseReponse> DataResult { get; set; }


        private DataGlobal()
        {
            // Prevent outside instantiation
            DataInput = new List<InputCase>();
            DataResult = new List<InputCaseReponse>();
        }
        private static DataGlobal instance;

        public static DataGlobal Instance
        {
            get
            {
                if (instance == null)
                    instance = new DataGlobal();
                return instance;
            }
        }
        public void AddInput(InputCase item)
        {
            DataInput.Add(item);
        }
        public void Add(InputCaseReponse item)
        {
            DataResult.Add(item);
        }

    }
}
