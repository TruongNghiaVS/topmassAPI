using Topmass.Job.Business.Model.webCan;

namespace Topmass.Job.Business.Model
{
    public class JobItemDisplay : BaseItemProductDisplay
    {
        public JobItemDisplay()
        {
            //Locationtext = "TP. HCM";
            LocationText = "";
            TypeMoney = 0;
            Aggrement = false;
        }
    }

    public class JobRelationItemDisplay : JobItemDisplay
    {



    }


    public class JobrecommendedItemDisplay : JobItemDisplay
    {


    }


}
