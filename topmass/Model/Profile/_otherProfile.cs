namespace topmass.Model
{

    public class InputOtherProfileUserRequestAdd
    {

        public string FullName { get; set; }
        public int? Level { get; set; }
        public int Id { get; set; }
        public string? Description { get; set; }

    }

    public class InputOtherProfileUserSaveSkill
    {

        public string FullName { get; set; }
        public int? Level { get; set; }
        public int Id { get; set; }


    }

    public class GetAllCVApplyInput
    {

        public int Status { get; set; }

        public GetAllCVApplyInput()
        {
            Status = -1;
        }
    }


    public class GetAllCVSave

    {

        public int OrderBy { get; set; }

        public GetAllCVSave()
        {
            OrderBy = -1;
        }
    }

}
