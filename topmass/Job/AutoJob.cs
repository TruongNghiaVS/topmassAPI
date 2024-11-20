using Quartz;
using Topmass.Sync.Business;


namespace VS.core.API.job
{
    public class SyncJob : IJob
    {
        private ISyncBusiness _syncBusiness;

        public SyncJob(
            ISyncBusiness syncBusiness)
        {
            _syncBusiness = syncBusiness;

        }
        public async Task Execute(IJobExecutionContext context)
        {
            await _syncBusiness.HandleCVSyncDataFromTopmass();
        }
    }
}
