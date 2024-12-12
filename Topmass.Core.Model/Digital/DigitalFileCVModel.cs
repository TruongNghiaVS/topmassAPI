

namespace Topmass.Core.Model.location
{
    public class DigitalFileCVModel : BaseModel
    {
        public string? FileCV { get; set; }
        public string? FileCVHide { get; set; }
        public int RelId { get; set; }
        public bool ReloadFile { get; set; }
        public DigitalFileCVModel()
        {


        }
    }


}
