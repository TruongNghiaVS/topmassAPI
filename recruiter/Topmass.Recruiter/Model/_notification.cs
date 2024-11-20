namespace Topmass.Recruiter.Model
{



    public class InputGetAllNotification
    {
        public int Status { get; set; }

        public InputGetAllNotification()
        {
            Status = -1;
        }
    }

    public class InputGetDetailInfoNotification
    {
        public int Id { get; set; }


    }


    public class InputChangeStatusUpdate
    {
        public int Id { get; set; }

        public int Status { get; set; }


    }

}
