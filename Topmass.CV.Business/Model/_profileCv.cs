namespace Topmass.CV.Business.Model
{
    public class EducationDisplayItem

    {

        public int Id { get; set; }
        public string? SchoolName { get; set; }

        public string? Major { get; set; }

        public string Position { get; set; }

        public string? FromMonth { get; set; }

        public string? FromYear { get; set; }

        public string? ToMonth { get; set; }

        public string? ToYear { get; set; }

        public bool? IsEnd { get; set; }

        public string? Introduction { get; set; }

        public string? LinkFile { get; set; }

        public int RelId { get; set; }

        public string? Rank { get; set; }

        public EducationDisplayItem()
        {
            SchoolName = "";
            Major = "";
            Position = "";
            FromMonth = "";
            FromYear = "";
            ToMonth = "";
            ToYear = "";
            IsEnd = false;
            Introduction = "";
            LinkFile = "";


        }
    }


    public class ExperienceDisplayItem


    {

        public int Id { get; set; }
        public string? CompanyName { get; set; }

        public string Position { get; set; }

        public string? FromMonth { get; set; }

        public string? FromYear { get; set; }

        public string? ToMonth { get; set; }

        public string? ToYear { get; set; }

        public bool? IsEnd { get; set; }

        public string? Introduction { get; set; }

        public string? LinkFile { get; set; }

        public int RelId { get; set; }

        public ExperienceDisplayItem()
        {
            CompanyName = "";

            Position = "";
            FromMonth = "";
            FromYear = "";
            ToMonth = "";
            ToYear = "";
            IsEnd = false;
            Introduction = "";
            LinkFile = "";


        }
    }

    public class EducationUserInfoReponse
    {

        public bool IsSave { get; set; }

        public EducationUserInfoReponse()
        {
            IsSave = true;
        }

    }

    public class EducationUserInfoUpdateReponse
    {


    }

    public class EducationUserInfoDeleteRequest
    {
        public int Id { get; set; }


    }

    public class EducationUserInfoUpdateRequest : EducationUserInfoRequest
    {

        public int Id { get; set; }
    }

    public class EducationUserInfoRequest
    {

        public int UserId { get; set; }
        public string? SchoolName { get; set; }

        public string? Major { get; set; }

        public string Position { get; set; }

        public string? DataInput { get; set; }

        public string? FromMonth { get; set; }

        public string? FromYear { get; set; }

        public string? ToMonth { get; set; }

        public string? ToYear { get; set; }

        public bool? IsEnd { get; set; }

        public string? Introduction { get; set; }

        public string? LinkFile { get; set; }

        public string? Rank { get; set; }
    }



    public class EducationGetAllResult
    {

        public List<EducationDisplayItem> Data { get; set; }


    }
    public class ExperienceUserInfoReponse
    {

        public bool IsSave { get; set; }

        public ExperienceUserInfoReponse()
        {
            IsSave = true;
        }

    }

    public class ExperienceUserInfoUpdateReponse
    {


    }

    public class ExperienceUserInfoDeleteRequest
    {
        public int Id { get; set; }


    }

    public class ExperienceUserInfoUpdateRequest : ExperienceUserInfoRequest
    {

        public int Id { get; set; }
    }

    public class ExperienceUserInfoRequest
    {

        public int UserId { get; set; }
        public string? CompanyName { get; set; }

        public string? Major { get; set; }

        public string Position { get; set; }

        public string? DataInput { get; set; }

        public string? FromMonth { get; set; }

        public string? FromYear { get; set; }

        public string? ToMonth { get; set; }

        public string? ToYear { get; set; }

        public bool? IsEnd { get; set; }

        public string? Introduction { get; set; }

        public string? LinkFile { get; set; }
    }



    public class ExperienceGetAllResult
    {
        public List<ExperienceDisplayItem> Data { get; set; }

    }

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
    public class AddOtherProfileRequest
    {
        public int UserId { get; set; }
        public int TypeData { get; set; }
        public string FullName { get; set; }
        public int? Level { get; set; }
        public string Description { get; set; }

        public int Id { get; set; }
    }


    public class OtherProfileRequestReponse
    {

        public bool IsSave { get; set; }

        public OtherProfileRequestReponse()
        {
            IsSave = true;
        }

    }

    public class OtherUserProfileDisplayItem
    {

        public string? FullName { get; set; }
        public string? Level { get; set; }

        public int Id { get; set; }

        public string? Description { get; set; }


    }

    public class OtherUserProfileSkillDisplayItem
    {

        public string? FullName { get; set; }
        public string? Level { get; set; }

        public int Id { get; set; }


    }


    public class OtherUserProfileGetAllResult
    {
        public dynamic Data { get; set; }

    }
    public class OtherUserProfileGetAllSkill
    {
        public List<OtherUserProfileSkillDisplayItem> Data { get; set; }

    }



    public class CertifyUserInfoReponse
    {

        public bool IsSave { get; set; }

        public CertifyUserInfoReponse()
        {
            IsSave = true;
        }

    }

    public class CertifyUserInfoUpdateReponse
    {


    }

    public class CertifyUserInfoDeleteRequest
    {
        public int Id { get; set; }


    }

    public class CertifyUserInfoUpdateRequest : CertifyUserInfoRequest
    {

        public int Id { get; set; }
    }

    public class CertifyUserInfoRequest
    {

        public int UserId { get; set; }
        public int TypeData { get; set; }


        public string? FullName { get; set; }

        public string? CompanyName { get; set; }

        public int MonthGet { get; set; }

        public int YearGet { get; set; }

        public string Introduction { get; set; }

        public string LinkFile { get; set; }

        public string? MonthExpired { get; set; }
        public string? YearExpired { get; set; }
        public bool IsExpired { get; set; }
        public CertifyUserInfoRequest()
        {
            IsExpired = false;
            MonthExpired = "0";
            YearExpired = "0";
        }
    }



    public class CertifyUserGetAllResult
    {

        public List<CertifyUserDisplayItem> Data { get; set; }


    }



    public class CertifyUserDisplayItem

    {

        public int Id { get; set; }



        public string? FullName { get; set; }

        public string? CompanyName { get; set; }

        public int MonthGet { get; set; }

        public int YearGet { get; set; }

        public string Introduction { get; set; }

        public string LinkFile { get; set; }


        public int RelId { get; set; }



        public CertifyUserDisplayItem()
        {



        }
    }


}
