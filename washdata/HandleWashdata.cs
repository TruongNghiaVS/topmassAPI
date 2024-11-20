namespace washdata
{
    public class HandleWashdata

    {

        private CallApi callApi;
        public HandleWashdata()
        {

            callApi = new CallApi();
        }

        public void WashData(List<WashDataItem> items)
        {
            foreach (var item in items)
            {
                UnitWashData(item);
            }
        }

        public void UnitWashData(WashDataItem item)
        {
            callApi.MakeCall(item.Phone, item);


        }
    }
}
