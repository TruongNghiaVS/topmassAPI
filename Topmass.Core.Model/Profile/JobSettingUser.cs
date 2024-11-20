namespace Topmass.Core.Model.Profile
{
    public class JobSettingUser : BaseModel
    {
        public int? Gender { get; set; }
        public string? Position { get; set; }
        public string Field { get; set; }

        public string Skill { get; set; }
        public string Experience { get; set; }
        public string? Salary { get; set; }
        public string? LocationAddress { get; set; }

        public int RelId { get; set; }

        public JobSettingUser()
        {
            Gender = -1;

        }

    }

}
