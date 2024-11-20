namespace Topmass.Job.Business
{
    public interface IJobUtilitiesBusiness
    {

        public Task<bool> AddJobSave(int jobId, int userId);
        public Task<bool> AddJobSave(string jobSlug, int userId);

        public Task<bool> RemoveJobSave(int id, int userId);

    }
}
