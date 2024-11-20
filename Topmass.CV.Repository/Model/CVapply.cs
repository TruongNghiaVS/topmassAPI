namespace Topmass.CV.Repository.Model
{
    public class CVapplyJobRequest
    {
        public int JobId { get; set; }
        public int CVId { get; set; }
        public int HandleBy { get; set; }

        public string FullName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }
        public CVapplyJobRequest()
        {
        }

    }

    public class CVapplyJobReponse
    {
        public bool? Success { get; set; }
        public CVapplyJobReponse()
        {
            Success = true;

        }

    }
    public class CVResumeRequest
    {
        public int UserId { get; set; }
        public int TypeData { get; set; }
        public int? TemplateID { get; set; }

        public string? LinkFile { get; set; }

        public int HandleBy { get; set; }

        public string? DataInput { get; set; }
        public CVResumeRequest()
        {
            TemplateID = 1;

        }

    }

    public class GetAllCVRequest
    {
        public int UserId { get; set; }

        public GetAllCVRequest()
        {


        }

    }
    public class GetAllCVReponse
    {
        public int UserId { get; set; }

        public List<ResumeDisplayItem> Data { get; set; }
        public GetAllCVReponse()
        {
            Data = new List<ResumeDisplayItem>();
        }


    }

    public class GetAllCVByJobRequest
    {
        public int JobId { get; set; }

        public int? TypeData { get; set; }

        public int? UserId { get; set; }

        public GetAllCVByJobRequest()
        {

            TypeData = -1;

        }

    }
    public class GetAllCVByJobReponse
    {
        public int UserId { get; set; }

        public List<JobApplyDisplayItem> Data { get; set; }
        public GetAllCVByJobReponse()
        {
            Data = new List<JobApplyDisplayItem>();
        }


    }


    public class CVResumeResponse
    {
        public bool Success { get; set; }


    }

    public class GetAllCVByCampaignRequest
    {
        public int JobId { get; set; }
        public int? Status { get; set; }
        public int UserId { get; set; }
        public int CampagnId { get; set; }
        public int? TypeData { get; set; }
        public string? Key { get; set; }
        public int Source { get; set; }
        public GetAllCVByCampaignRequest()
        {

            TypeData = -1;
            Source = 1;

        }

    }
    public class GetAllCVByCampaignReponse
    {
        public List<JobApplyDisplayItem> Data { get; set; }
        public GetAllCVByCampaignReponse()
        {
            Data = new List<JobApplyDisplayItem>();
        }


    }


    public class ApplyJobWithCreateCV
    {
        public int TypeData { get; set; }
        public int? TemplateID { get; set; }
        public string? LinkFile { get; set; }

        public string FullName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public int UserId { get; set; }

        public int JobId { get; set; }




    }

    public class ApplyJobWithCreateCVReponse
    {
        public bool Success { get; set; }

    }


    public class GetIdentifyReponse
    {
        public int Id { get; set; }

        public string UserName { get; set; }
    }



}
