using Topmass.Core.Repository;
using Topmass.Job.Business.Model;
using Topmass.Job.Business.Model.webCan;

namespace Topmass.Job.Business
{
    public class JobUserBusiness :
        IJobUserBusiness
    {

        private readonly IJobRepository _jobRep;
        public JobUserBusiness(


            IJobRepository jobrep
            )
        {

            _jobRep = jobrep;
        }

        public async Task<GetAllCVApplyReponse> GetAllCVApply(int userId, int status = -1)
        {
            var reponse = new GetAllCVApplyReponse();
            var dataReponse = await _jobRep.ExecuteSqlProcerduceToList<JobApplyItemDisplay>("sp_getAllCVApplyUserId", new
            {
                userId,
                status
            });
            reponse.Data = dataReponse;
            return reponse;

        }

        public async Task<GetAllJpobSaveReponse> GetAllJobSave(int userId, int orderBy)
        {
            var reponse = new GetAllJpobSaveReponse();
            var dataReponse = await _jobRep.ExecuteSqlProcerduceToList<JobSaveItemDisplay>("sp_user_getallJobSave", new
            {
                userId,
                orderBy
            });
            if (userId > 0)
            {
                var allJobIdSave = new List<JobCountGroupById>();
                var allJobApply = new List<JobCountGroupById>();
                allJobIdSave = await _jobRep.ExecuteSqlProcerduceToList<JobCountGroupById>
                 (
              "select DISTINCT JobId from jobSave where  UserId = @UserId and  Deleted = 0 ", new { UserId = userId },
              commandType: System.Data.CommandType.Text

              );
                allJobApply = await _jobRep.ExecuteSqlProcerduceToList<JobCountGroupById>
                    (
                    "select DISTINCT  JobId  from jobApply  where  CreatedBy = @userId ", new { UserId = userId },
                    commandType: System.Data.CommandType.Text
                    );
                var listNew = new List<JobSaveItemDisplay>();
                foreach (var item in dataReponse)
                {
                    var itemSave = allJobIdSave.Any(x => x.JobId == item.JobId);
                    var itemApply = allJobApply.Any(x => x.JobId == item.JobId);
                    item.IsLike = true;
                    item.IsSave = true;
                    if (itemApply == true)
                    {
                        continue;
                    }
                    item.IsApply = itemApply;
                    listNew.Add(item);
                }
                reponse.Data = listNew;
            }
            else
            {
                reponse.Data = dataReponse;
            }
            return reponse;

        }

    }
}
