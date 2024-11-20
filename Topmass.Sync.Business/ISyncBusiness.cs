namespace Topmass.Sync.Business
{
    public interface ISyncBusiness
    {
        public Task<bool> HandleCVSyncDataFromTopmass();
    }
}
