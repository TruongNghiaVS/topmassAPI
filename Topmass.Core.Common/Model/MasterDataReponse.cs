namespace Topmass.Core.Common.Model
{


    public class MasterDataIndex
    {
        public int TypeData { get; set; }
        public string Text { get; set; }
        public int Id { get; set; }
        public string Code { get; set; }
    }

    public enum MasterDataTypeData
    {
        LINHVUC = 1,
        NGANHNGHE = 2
    }
    public class MasterDataRequest
    {
        public int TypeData { get; set; }

        public MasterDataRequest()
        {

        }
    }
}
