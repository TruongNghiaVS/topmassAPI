using Topmass.CV.Repository.Model;
using TopMass.Core.Result;

namespace Topmass.CV.Business.Model
{


    public class CVRequestAdd
    {
        public int UserId { get; set; }
        public int TypeData { get; set; }
        public int? TemplateID { get; set; }

        public string? LinkFile { get; set; }

        public int HandleBy { get; set; }

        public string? DataInput { get; set; }


        public CVRequestAdd()
        {
            TemplateID = 1;

        }

    }

    public class CVReponseAdd
    {
        public bool? Success { get; set; }
        public CVReponseAdd()
        {
            Success = true;

        }

    }
    public class ApplyJobRequestAdd
    {
        public string JobSlug { get; set; }
        public int CVId { get; set; }
        public int HandleBy { get; set; }

        public string FullName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public ApplyJobRequestAdd()
        {


        }

    }

    public class ApplyJobResponeAdd : BaseResult
    {


        public ApplyJobResponeAdd()
        {


        }

    }

    public class ApplyJobWithCreateCVAdd
    {

        public int TypeData { get; set; }
        public int? TemplateID { get; set; }
        public string? LinkFile { get; set; }

        public string FullName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public int UserId { get; set; }

        public string jobSlug { get; set; }
    }

    public class ApplyJobWithCreateResponeAdd : BaseResult
    {


        public ApplyJobWithCreateResponeAdd()
        {


        }

    }
    public class GetAllOfHumanRequest
    {
        public int JobId { get; set; }

        public int UserId { get; set; }
        public int CampagnId { get; set; }
        public int? TypeData { get; set; }
        public int? Status { get; set; }
        public string? Key { get; set; }

        public int? Source { get; set; }

        public GetAllOfHumanRequest()
        {

            TypeData = -1;
            Status = -1;
            CampagnId = -1;
            JobId = -1;
            Source = 1;

        }

    }

    public class GetAllCVOfJobRequest
    {
        public int JobId { get; set; }

        public int UserId { get; set; }
        public int? TypeData { get; set; }
        public GetAllCVOfJobRequest()
        {

            TypeData = -1;

        }

    }

    public class GetAllCVOfJobReponse
    {
        public List<JobApplyDisplayItem> Data { get; set; }
        public GetAllCVOfJobReponse()
        {
            Data = new List<JobApplyDisplayItem>();
        }


    }

    public class GetAllOfHumanRequestReponse
    {
        public List<JobApplyDisplayItem> Data { get; set; }
        public GetAllOfHumanRequestReponse()
        {
            Data = new List<JobApplyDisplayItem>();
        }


    }


    public class GetInfoCVReponse
    {
        public dynamic Data { get; set; }
    }


    public class GetInfoCVRequest
    {
        public int CVId { get; set; }

        public GetInfoCVRequest()
        {



        }

    }


    public class CVChangeViewModeRequest
    {

        public int Identi { get; set; }

        public int ViewMode { get; set; }

        public int HandleBy { get; set; }
    }

    public class CVStatusHistoryRequest
    {

        public int Identi { get; set; }
        public int NoteCode { get; set; }
        public string Noted { get; set; }

        public int HandleBy { get; set; }

    }


    public class GetAllCVUserReponse
    {
        public List<ResumeDisplayItem> Data { get; set; }
    }

}

