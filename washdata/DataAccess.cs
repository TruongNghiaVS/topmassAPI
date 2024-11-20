using Dapper;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.SqlClient;

namespace washdata
{
    public class DataAccess

    {
        private IDbConnection _connection;

        public DataAccess()
        {

            _connection = new SqlConnection("Server=192.168.1.3,1433; Initial Catalog=vsrolapi;User ID=crm;Password=Vietstar@2018; Persist Security Info=False;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;Integrated Security=false;");

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

        public Task AddLogCall(string phoneNumber, string noAgreement)
        {
            using (var con = GetConnection())
            {
                con.Execute("insert into WashData(PhoneNumber, NoAgreement, TypeData) values ( @PhoneNumber,@NoAgreement,1)",
                    new
                    {
                        PhoneNumber = phoneNumber,
                        NoAgreement = noAgreement
                    });

            }
            return Task.CompletedTask;
        }


        public Task AddResultWashData(WashDataDBItem item)
        {
            using (var con = GetConnection())
            {
                con.Execute("insert into WashDataResult ( Calldate, Src,Dcontext, Disposition, Duration, Billsec) values ( @Calldate, @Src, @Dcontext, @Disposition, @Duration, @Billsec)",
                    item);

            }
            return Task.CompletedTask;
        }
        public Task GetDataWashData()

        {
            var listData = new List<WashDataDBItem>();
            using (var con = GetMysqlConnection())
            {
                var data = con.Query<WashDataDBItem>("SELECT d.calldate, d.src, d.dcontext, d.src, d.disposition, d.duration, d.billsec    FROM cdr d  WHERE  d.calldate >=\"2024-10-16 14:00:00\" AND  ( d.dcontext ='from-pstn'  OR d.dcontext = 'ring_only')", new
                {

                }, commandType: CommandType.Text);

                listData = data.ToList();

            }
            for (int i = 0; i < listData.Count; i++)
            {
                AddResultWashData(listData[i]);

            }
            return Task.CompletedTask;

        }
    }
}
