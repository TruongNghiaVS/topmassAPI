namespace Topmass.Core.Model.Campagn
{
    public class JobInfoModel : BaseModel
    {
        public string? Name { get; set; }
        public int? CapagnId { get; set; }

        public int? JobId { get; set; }
        public string? Position { get; set; }
        public int? Profession { get; set; }
        public DateTime? Expired_date { get; set; }
        public int? Quantity { get; set; }
        public int? Type_of_work { get; set; }
        public int? Rank { get; set; }

        public int? Experience { get; set; }

        public bool? Aggrement { get; set; }

        public int? Salary_from { get; set; }

        public int? Salary_to { get; set; }

        public string? Type_money { get; set; }

        public int? Gender { get; set; }

        public string? Description { get; set; }

        public string? Requirement { get; set; }


        public string? Benefit { get; set; }

        public string? Skill { get; set; }

        public string? fullName { get; set; }

        public string? Phone { get; set; }
        public string? Emails { get; set; }
        public string? Locations { get; set; }
        public string? Time_workings { get; set; }


    }

}
