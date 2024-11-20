namespace Topmass.Core.Model
{
    public class CandidateInfoModel : BaseModel
    {
        public int? UserId { get; set; }
        public string? Email { get; set; }

        public string? AvatarLink { get; set; }

        public bool PrivateMode { get; set; }

        public bool PublicMode { get; set; }

        public CandidateInfoModel()
        {
            PrivateMode = true;
            PublicMode = true;
        }

    }
}
