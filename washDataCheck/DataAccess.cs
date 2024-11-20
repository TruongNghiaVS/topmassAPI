using Dapper;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.SqlClient;

namespace washDataCheck
{
    public class WashDataDBItem
    {
        public DateTime Calldate { get; set; }
        public string Src { get; set; }
        public string Dcontext { get; set; }
        public string Disposition { get; set; }
        public int Duration { get; set; }
        public int Billsec { get; set; }


    }
    public class DataAccess

    {
        private IDbConnection _connection;
        private DataGlobal _DataGlobal;



        public DataAccess()
        {

            _connection = new SqlConnection("Server=192.168.1.3,1433; Initial Catalog=vsrolapi;User ID=crm;Password=Vietstar@2018; Persist Security Info=False;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;Integrated Security=false;");
            _DataGlobal = DataGlobal.Instance;

        }
        protected IDbConnection GetConnection()
        {
            var con = new SqlConnection("Server=192.168.1.3,1433; Initial Catalog=vsrolapi;User ID=crm;Password=Vietstar@2018; Persist Security Info=False;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;Integrated Security=false;");
            con.Open();
            return con;
        }

        protected IDbConnection GetMysqlConnection()
        {
            var con = new MySqlConnection("Server=192.168.1.151;uid=demo1;Pwd=123456789;database=asteriskcdrdb;");
            con.Open();
            return con;
        }



        public Task GetDataWashData()

        {
            var listData = new List<WashDataDBItem>();
            using (var con = GetMysqlConnection())
            {
                var data = con.Query<WashDataDBItem>("SELECT d.calldate, d.src, d.dcontext, d.src, d.disposition, d.duration, d.billsec  \r\n\r\n  FROM cdr d  WHERE  d.calldate >=\"2024-10-27 00:00:00\" AND  ( d.dcontext ='from-pstn'  OR d.dcontext = 'ring_only')", new
                {

                }, commandType: CommandType.Text);

                listData = data.ToList();

            }
            foreach (var item in listData)
            {
                var tieminsert = new InputCaseReponse()
                {
                    Disposition = item.Disposition,
                    Duration = item.Duration,
                    Phone = item.Src
                };
                _DataGlobal.Add(tieminsert);
            }
            return Task.CompletedTask;

        }
    }
}
