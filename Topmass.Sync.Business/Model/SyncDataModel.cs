namespace Topmass.Sync.Business.Model
{
    public class SyncDataModel
    {
        public int CandidateId { get; set; }

        public string FullName { get; set; }

        public string ExperienceText { get; set; }

        public string Educationtext { get; set; }

        public string Position { get; set; }
        // sourceType: 3 headhunt  4 topmass
        public int SourceType { get; set; }
    }
}
