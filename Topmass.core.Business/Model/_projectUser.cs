namespace Topmass.core.Business
{
    public class ProjectUserInfoReponse
    {

        public bool IsSave { get; set; }

        public ProjectUserInfoReponse()
        {
            IsSave = true;
        }

    }

    public class ProjectUserInfoUpdateReponse
    {


    }

    public class ProjectUserInfoDeleteRequest
    {
        public int Id { get; set; }


    }

    public class ProjectUserInfoUpdateRequest : ProjectUserInfoRequest
    {

        public int Id { get; set; }
    }

    public class ProjectUserInfoRequest
    {

        public int UserId { get; set; }







        public string? FromMonth { get; set; }

        public string? FromYear { get; set; }

        public string? ToMonth { get; set; }

        public string? ToYear { get; set; }

        public bool? IsEnd { get; set; }

        public string? Introduction { get; set; }

        public string? LinkFile { get; set; }



        public string? ProjectName { get; set; }

        public string? CustomerName { get; set; }

        public int NumOfMember { get; set; }
        public string Position { get; set; }

        public string Techology { get; set; }
    }



    public class ProjectUserGetAllResult
    {

        public List<ProjectUserDisplayItem> Data { get; set; }


    }



    public class ProjectUserDisplayItem

    {

        public int Id { get; set; }
        public string? ProjectName { get; set; }

        public string? CustomerName { get; set; }

        public string NumOfMember { get; set; }
        public string Position { get; set; }

        protected string Techology { get; set; }

        public string Technology
        {
            get
            {
                return Techology;
            }
        }

        public string? FromMonth { get; set; }

        public string? FromYear { get; set; }

        public string? ToMonth { get; set; }

        public string? ToYear { get; set; }

        public bool? IsEnd { get; set; }

        public string? Introduction { get; set; }

        public string? LinkFile { get; set; }


        public ProjectUserDisplayItem()
        {

            Position = "";
            FromMonth = "";
            FromYear = "";
            ToMonth = "";
            ToYear = "";
            IsEnd = false;
            Introduction = "";
            LinkFile = "";
            ProjectName = "";
            CustomerName = "";
            NumOfMember = "";
            ToMonth = "";
            ToYear = "";
            Introduction = "";








        }
    }


}
