

using TopMass.Core.Result;

namespace Topmass.Job.Business.Model
{
    public class JobItemReponse : BaseResult
    {



        public JobItemReponse()
        {

        }

    }


    public class JobItemUpdateReponse
    {


        public bool Result { get; set; }
        public JobItemUpdateReponse()
        {
            Result = true;
        }

    }

    public class JobItemStatusUpdateReponse
    {

        public bool Result { get; set; }
        public JobItemStatusUpdateReponse()
        {
            Result = true;
        }


    }


    public class JobSearchReponse
    {


    }

    public class JobInfoReponse
    {

    }
}
