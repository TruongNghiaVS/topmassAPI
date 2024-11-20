using Topmass.Job.Business.Model.webCan;

namespace Topmass.Job.Business.Model
{
    public class GetAllBestJobOptimization
    {
    }

    public class BestJobOptimizationDisplayItemData : BaseItemProductDisplay
    {
        public string RangeSalary { get; set; }
        public int TotalRecord { get; set; }

        //IT, Marketting
        public BestJobOptimizationDisplayItemData() : base()
        {

            Aggrement = false;
            TypeMoney = 0;
        }

    }

    public class JobIdCount
    {
        public int Id { get; set; }
    }
    public class JobCountGroupById
    {
        public int JobId { get; set; }
    }




}
