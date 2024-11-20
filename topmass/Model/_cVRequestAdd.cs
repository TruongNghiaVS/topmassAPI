namespace topmass.Model
{


    public class CreateCVAddRequest
    {
        public int TypeData { get; set; }
        public int? TemplateID { get; set; }
        public string? LinkFile { get; set; }
        public CreateCVAddRequest()
        {
            TemplateID = 0;

        }

    }

    public class InputApplyJobAddRequest
    {
        public string? JobId { get; set; }
        public int CVId { get; set; }


        public string FullName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

    }

    public class InputGetAllCVApply
    {
        public int JobId { get; set; }
        public int CampagnId { get; set; }
        public int? TypeData { get; set; }
        public InputGetAllCVApply()
        {

            TypeData = -1;

        }
    }
    public class InputGetAllCVApplyOfCampagn
    {

        public int CampagnId { get; set; }


    }
    public class InputGetAllCVApplyOfJob
    {
        public int JobId { get; set; }

        public int? TypeData { get; set; }
        public InputGetAllCVApplyOfJob()
        {
            TypeData = -1;

        }
    }

    public class InputGetInfoCV
    {
        public int CVId { get; set; }

        public InputGetInfoCV()
        {

        }
    }


    public class InputApplyJobWithCreateCVRequest
    {
        public int? TypeData { get; set; }
        public int? TemplateID { get; set; }
        public string? LinkFile { get; set; }

        public string? FullName { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public string? JobId { get; set; }

    }


    public class InputJobId
    {
        public int JobId { get; set; }


    }


    public class InputJobSave
    {
        public string JobId { get; set; }

    }

    public class InputJobInfo
    {
        public string JobId { get; set; }



    }

    public class InputJobInfoRequest
    {
        public string? JobId { get; set; }
        //public int? JobId { get; set; }
    }



}
