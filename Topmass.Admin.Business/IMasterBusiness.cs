namespace Topmass.Admin.Business
{
    public interface IMasterBusiness
    {
        public Task<dynamic> GetAllDataByType(int typeData);
    }
}
