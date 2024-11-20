namespace topmass.Model
{

    public class InputCompanyRequestGetAll
    {
        public string? KeyWord { get; set; }

        public bool LoadDataJob { get; set; }
        public InputCompanyRequestGetAll()
        {
            LoadDataJob = false;
        }
    }


    public class InputCompanyGetInfo
    {
        public string? Slug { get; set; }

        public int Location { get; set; }

        public string? Keyword { get; set; }

        public InputCompanyGetInfo()
        {
            Location = -1;
        }
    }

    public class InputCompanyFlower
    {
        public int Slug { get; set; }


        public InputCompanyFlower()
        {
            Slug = -1;
        }
    }

}
