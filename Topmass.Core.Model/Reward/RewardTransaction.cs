﻿namespace Topmass.Core.Model.Reward
{
    public class RewardTransaction : BaseModel
    {
        public int Rel { get; set; }
        public int Point { get; set; }
        public string Content { get; set; }
        public DateTime? BusinessDate { get; set; }
        // 1 
        public int? DataType { get; set; }
        public RewardTransaction()
        {
            BusinessDate = DateTime.Now;
            Rel = 0;
            Point = 0;
            DataType = 1;
        }
    }
}
