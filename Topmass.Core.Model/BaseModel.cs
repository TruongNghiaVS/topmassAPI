
namespace Topmass.Core.Model
{
    public class BaseModel : IBaseModel
    {

        public int Status { get; set; }


        public bool Deleted { get; set; }
        public int Id { get; set; }
        public DateTime CreateAt { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdateAt { get; set; }
        public int UpdatedBy { get; set; }
        protected string Table { get; set; }

        public BaseModel()
        {
            Id = -1;
            Status = 0;
            Deleted = false;
            CreateAt = DateTime.Now;
            UpdateAt = DateTime.Now;
            UpdatedBy = -1;
        }
    }

    public class BaseModelIdentiti : BaseModel
    {

        public string Email { get; set; }
        public int RelId { get; set; }
    }
}
