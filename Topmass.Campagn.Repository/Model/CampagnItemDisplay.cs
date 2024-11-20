namespace Topmass.Campagn.Repository.Model
{
    public class CampagnItemDisplay
    {
        public CampagnItemDisplay()
        {
            TotalRecord = 0;
        }
        public string Name { get; set; }
        public int TotalRecord { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int Status { get; set; }
        public int Id { get; set; }
        public string StatusText
        {
            get
            {
                if (Status > 0)
                {
                    return "Chiến dịch đang chạy";
                }
                return "Chiến dịch đang tắt";

            }
        }


        public string LableText
        {
            get
            {
                if (Status > 0)
                {
                    return "Đăng tin tuyển dụng";
                }
                return "Bật chiến dịch để thực hiện đăng tin";

            }
        }
    }
}
