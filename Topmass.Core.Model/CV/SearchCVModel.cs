namespace Topmass.Core.Model.CV
{
    public class SearchCVModel : BaseModel
    {
        public SearchCVModel()
        {
        }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public int CandidateId { get; set; }
        public string LocationText { get; set; }
        public string ExperienceText { get; set; }
        public string Educationtext { get; set; }
        public string IntroductionText { get; set; }
        public string LocationCode { get; set; }
        public int CountView { get; set; }
        public int CountContact { get; set; }

    }

}
