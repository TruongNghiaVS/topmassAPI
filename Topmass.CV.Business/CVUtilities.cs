using Topmass.Core.Model.JobAply;
using Topmass.Core.Repository;
using Topmass.CV.Business.Model;

namespace Topmass.CV.Business
{
    public class CVUtilities : ICVUtilities
    {


        private readonly IJobApplyRepository _jobApplyRepository;
        private readonly IJobApplyStatusRepository _jobApplyStatusRepository;

        public CVUtilities(
             IJobApplyRepository jobApplyRepository,
             IJobApplyStatusRepository jobApplyStatusRepository

            )
        {

            _jobApplyRepository = jobApplyRepository;
            _jobApplyStatusRepository = jobApplyStatusRepository;

        }

        public async Task<bool> AddHistoryStatus(CVStatusHistoryRequest request)
        {
            if (request.Identi < 1)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(request.Noted))
            {
                return false;
            }
            if (request.NoteCode < 1)
            {
                return false;
            }

            var itemInsert = new JobApplyStatus()
            {
                RelId = request.Identi,
                CreatedBy = request.HandleBy,
                CreateAt = DateTime.Now,
                Deleted = false,
                Status = request.NoteCode,
                Note = request.Noted,
                UpdateAt = DateTime.Now,
                UpdatedBy = request.HandleBy
            };
            await _jobApplyStatusRepository.AddOrUPdate(itemInsert);

            var jobApply = await _jobApplyRepository.GetById(itemInsert.RelId);
            if (jobApply != null)
            {
                jobApply.Status = itemInsert.Status;
                jobApply.UpdateAt = DateTime.Now;
                await _jobApplyRepository.AddOrUPdate(jobApply);
            }
            await AddViewerByHumnan(request.HandleBy, request.Identi);
            return true;

        }


        public async Task<bool> AddViewerByHumnan(int humanId, int cvAply)
        {
            var addViewerJob = await _jobApplyRepository.ExecuteSqlProcedure("sp_addHumanViewerJob",
                new
                {
                    cvId = cvAply,
                    UserId = humanId
                });

            return addViewerJob;

        }
        public async Task<bool> UpdateViewModel(CVChangeViewModeRequest request)
        {
            var requestUpdate = await _jobApplyRepository.GetById(request.Identi);
            if (requestUpdate != null)
            {
                requestUpdate.ViewMode = request.ViewMode;
                requestUpdate.UpdatedBy = request.HandleBy;
                requestUpdate.UpdateAt = DateTime.Now;
                await _jobApplyRepository.AddOrUPdate(requestUpdate);
            }
            return true;

        }
    }
}
