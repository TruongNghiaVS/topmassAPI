namespace Topmass.Recruiter.Model
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
        public int JobId { get; set; }
        public int CVId { get; set; }

    }

    public class InputGetAllCVApply
    {
        public int JobId { get; set; }
        public int? Campaign { get; set; }
        public int? TypeData { get; set; }
        public int? Recruitment { get; set; }
        public int? Status { get; set; }

        public string? Key { get; set; }

        public int Source { get; set; }
        public InputGetAllCVApply()
        {

            TypeData = -1;
            Source = 1;

        }
    }
    public class InputGetAllCVApplyOfCampagn
    {

        public int CampagnId { get; set; }


    }

    public class IntputCVChangeViewModeRequest
    {

        public int Identi { get; set; }

        public int ViewMode { get; set; }
    }

    public class IntputCVStatusHistoryRequest
    {

        public int Identi { get; set; }
        public int NoteCode { get; set; }
        public string Noted { get; set; }

        public int HandleBy { get; set; }



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


    public class InputGetAllSearchCVApplyOfJob
    {
        public int JobId { get; set; }


        public InputGetAllSearchCVApplyOfJob()
        {


        }
    }
    public class InputGetInfoCV
    {
        public int CVId { get; set; }

        public InputGetInfoCV()
        {

        }
    }


    public class InputGetOverViewInfoMation
    {

        public int JobId { get; set; }

        public DateTime? From { get; set; }

        public DateTime? To { get; set; }
        public InputGetOverViewInfoMation()
        {
            JobId = -1;

            var datetimeNow = DateTime.Now;

            var dtFrom = datetimeNow.AddDays(-7);

            From = new DateTime(dtFrom.Year, dtFrom.Month, dtFrom.Day,
                0, 0, 0);

            To = new DateTime(datetimeNow.Year, datetimeNow.Month, datetimeNow.Day,
                23, 59, 59);
        }
    }



}
