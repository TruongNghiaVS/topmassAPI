namespace Topmass.core.Business
{
    public class AddOtherProfileRequest
    {
        public int UserId { get; set; }
        public int TypeData { get; set; }
        public string FullName { get; set; }
        public int? Level { get; set; }
        public string Description { get; set; }

        public int Id { get; set; }
    }


    public class OtherProfileRequestReponse
    {

        public bool IsSave { get; set; }

        public OtherProfileRequestReponse()
        {
            IsSave = true;
        }

    }

    public class OtherUserProfileDisplayItem
    {

        public string? FullName { get; set; }
        public string? Level { get; set; }

        public int Id { get; set; }

        public string? Description { get; set; }


    }

    public class OtherUserProfileSkillDisplayItem
    {

        public string? FullName { get; set; }
        public string? Level { get; set; }

        public int Id { get; set; }


    }


    public class OtherUserProfileGetAllResult
    {
        public dynamic Data { get; set; }

    }
    public class OtherUserProfileGetAllSkill
    {
        public List<OtherUserProfileSkillDisplayItem> Data { get; set; }

    }

}
