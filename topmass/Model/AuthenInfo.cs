namespace topmass.Model
{
    public class AuthenInfo
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Id { get; set; }

        public int UserId
        {
            get
            {
                if (string.IsNullOrEmpty(Id))
                {
                    return 0;
                }
                return int.Parse(Id);

            }
        }
        public dynamic DataInfo { get; set; }

        public AuthenInfo()
        {
            DataInfo = new
            {

            };
        }

    }

}
