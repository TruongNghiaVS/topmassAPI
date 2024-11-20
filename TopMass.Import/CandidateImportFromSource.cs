namespace TopMass.Import
{
    using Dapper;
    using System.Data.SqlClient;

    public class CandidateImportFromSource
    {
        private List<CandidateInfoImport> Data { get; set; }
        public CandidateImportFromSource()
        {
            Data = new List<CandidateInfoImport>();
        }
        public void LoadData()
        {
            var sqlText = "Server=192.168.1.3,1433; Initial Catalog=human;User ID=crm;Password=Vietstar@2018; Persist Security Info=False;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;Integrated Security=false;";
            try
            {
                using (SqlConnection connection = new SqlConnection(sqlText))
                {
                    connection.Open();
                    var dataReponse = connection.Query<CandidateInfoImport>("select e.Name, dbo.getFullNameJob(f.JobId) as Postion, e.Phone,\r\ne.Email, e.CVLink, f.CVLink as 'CVLink2' \r\nfrom Candidate e inner join [Order] f on e.Id = f.CandidateId\r\nwhere Email is not null", new { }, commandType: System.Data.CommandType.Text);
                    Data = dataReponse.ToList();
                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception Occre while creating table:" + e.Message + "\t" + e.GetType());
            }

        }

        public void HandleData()
        {
            if (Data.Count < 1)
            {
                LoadData();
            }
            foreach (var item in Data)
            {
                CreateAccount(item);
            }
        }
        public void CreateAccount(CandidateInfoImport itemAdd)
        {
            using (SqlConnection conn = new SqlConnection("Server=192.168.1.3,1433; Initial Catalog=jobvieclam;User ID=crm;Password=Vietstar@2018; Persist Security Info=False;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;Integrated Security=false;"))
            {
                conn.Execute("importCandidate", itemAdd,
                    commandType: System.Data.CommandType.StoredProcedure);
                conn.Close();
            }

        }


    }
}
