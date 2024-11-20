namespace Topmass.Core.Model
{
    internal interface IBaseModel
    {
        public int Id { get; set; }
        public DateTime CreateAt { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdateAt { get; set; }
        public int UpdatedBy { get; set; }

    }
}
