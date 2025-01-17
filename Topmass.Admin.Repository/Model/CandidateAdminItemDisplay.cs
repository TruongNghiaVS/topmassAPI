namespace Topmass.Admin.Repository
{
    public class CandidateAdminItemDisplay
    {

        public int Id { get; set; }

        public string UserName { get; set; }

        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string Phone { get; set; }

        public string Email { get; set; }

        public DateTime? DateRegister { get; set; }

        public int Rulestatus { get; set; }

        public int PrivateMode { get; set; }

        public int PublicMode { get; set; }

        public DateTime CreateAt { get; set; }

        public DateTime? UpdateAt { get; set; }

        public DateTime DateActive { get; set; }
        public DateTime LastChange { get; set; }
        public int Status { get; set; }

        public string EmailStatus
        {
            get
            {
                if (Rulestatus == 2)
                {
                    return "Đã xác thực";
                }
                return "Chưa xác thực";
            }
        }

        public string StatusText
        {
            get
            {
                if (Status == 1)
                {
                    return "Hoạt động";
                }
                return "Không hoạt động";
            }
        }

    }



    public class CandidateAdminInfo
    {

        public int Id { get; set; }

        public string UserName { get; set; }

        public string FullName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public DateTime? DateRegister { get; set; }

        public int Rulestatus { get; set; }

        public int PrivateMode { get; set; }

        public int PublicMode { get; set; }

        public DateTime DateActive { get; set; }
        public DateTime LastChange { get; set; }
        public int Status { get; set; }



    }

}
