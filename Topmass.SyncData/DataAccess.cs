using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace HandleSyncImportCV
{
    public class InputReponseId
    {
        public int Id { get; set; }
    }

    public class SearchCVAdd
    {


        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Position { get; set; }
        public string LocationText { get; set; }
        public string IntroductionText { get; set; }
        public string LocationCode { get; set; }
        public int Gender { get; set; }
        public int DobYear { get; set; }
        public int RandEducation { get; set; }
        public string ContentCV { get; set; }
        public string ContentCV2 { get; set; }
        public string LinkCV { get; set; }
        public string LinkCVHide { get; set; }

        public SearchCVAdd()
        {

        }

    }
    public class DataAccess
    {
        private IDbConnection _connection;
        public DataAccess()
        {
            _connection = new SqlConnection("Server=192.168.1.3,1433; Initial Catalog=jobvieclam;User ID=crm;Password=Vietstar@2018; Persist Security Info=False;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;Integrated Security=false;");
        }
        protected IDbConnection GetConnection()
        {
            var con = new SqlConnection("Server=192.168.1.3,1433; Initial Catalog=jobvieclam;User ID=crm;Password=Vietstar@2018; Persist Security Info=False;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;Integrated Security=false;");
            con.Open();
            return con;
        }
        public async Task<bool> ExecuteSQlProcerdure(string nameProcerdure, object request)
        {
            using (var con = GetConnection())
            {

                var result = con.ExecuteScalar(nameProcerdure,
                request,
                commandType: CommandType.StoredProcedure);

                if (result == null)
                {
                    result = false;
                }
                return true;


            }
        }

        public async Task<bool> CreateSearchCV(SearchCVAdd requestSearch)
        {


            var sqlText = "CreateAccountWithSearchSync";

            var result = await ExecuteSQlProcerdure(sqlText, requestSearch);
            return result;



        }
    }
}
