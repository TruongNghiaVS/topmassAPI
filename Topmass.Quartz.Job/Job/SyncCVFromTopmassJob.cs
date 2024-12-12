using Quartz;
using Topmass.Sync.Business;


namespace Topmass.Quartz.Job
{
    public class SyncCVFromTopmassJob : IJob
    {
        private ISyncBusiness _syncBusiness;

        public SyncCVFromTopmassJob(
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
