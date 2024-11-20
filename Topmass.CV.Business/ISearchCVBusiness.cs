namespace Topmass.CV.Business
{
    public interface ISearchCVBusiness
    {
        public Task<bool> SaveResultSearch(int searchId,
            string LinkFile, int userId,
            int Campaign = -1, int jobid = -1);
    }
}
