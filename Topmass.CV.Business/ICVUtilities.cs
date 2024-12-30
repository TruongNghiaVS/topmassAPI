using Topmass.CV.Business.Model;

namespace Topmass.CV.Business
{
    public interface ICVUtilities
    {
        public Task<bool> UpdateViewModel(CVChangeViewModeRequest request);
        public Task<bool> AddHistoryStatus(CVStatusHistoryRequest request);

        public Task<bool> AddViewerByHumnan(int humanId, int cvAply);

        public Task<bool> CandidateViewerAddStatus(
                int identiti, int handleby, int noteCode, string noted

            );
    }

}
