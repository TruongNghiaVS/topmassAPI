namespace Topmass.Business.History.model
{
    public class HIstoryDataRequest
    {
        public int UserId { get; set; }


        public HIstoryDataRequest()
        {

        }


    }

    public class HIstoryDataReponse
    {

        public List<HIstoryDataDisplay> Data { get; set; }
        public HIstoryDataReponse()
        {
            Data = new List<HIstoryDataDisplay>();
        }
    }


    public class HIstoryDataRequestAdd
    {
        public int UserId { get; set; }
        public string Content { get; set; }
        public DateTime? BusinessTime { get; set; }
        public int Actor { get; set; }
        public int TypeData { get; set; }

        public int Source { get; set; }
        public HIstoryDataRequestAdd()
        {
            Actor = 1; TypeData = 1;

            Source = 2;
        }


    }
}
