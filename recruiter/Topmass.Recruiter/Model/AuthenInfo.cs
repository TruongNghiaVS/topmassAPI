namespace Topmass.Recruiter.Model
{
    public class AuthenInfo
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Id { get; set; }

        public dynamic DataInfo { get; set; }

        public int UserId
        {
            get
            {
                if (string.IsNullOrEmpty(Id))
                {
                    return -1;
                }
                return int.Parse(Id);
            }
        }

        public AuthenInfo()
        {
            DataInfo = new
            {

            };
        }

    }

}
