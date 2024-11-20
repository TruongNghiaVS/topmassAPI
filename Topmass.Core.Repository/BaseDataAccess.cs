using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Topmass.Core.Model;
using Topmass.Core.Repository.Log;

namespace Topmass.Core.Repository
{
    public class BaseDataAccess
    {
        protected IDbConnection _connection { get; set; }
        protected readonly IConfiguration _configuration;
        protected int offset;
        protected string tableName = "";
        protected string sqlGetALl = "";


        private LogWriter _log;
        public BaseDataAccess(IConfiguration configuration)
        {
            _configuration = configuration;

            _log = new LogWriter("tạo thành công");
            try
            {
                _connection = new SqlConnection(configuration.GetConnectionString("stringConnect"));
                _log.LogWrite("kết nối thành công");
            }
            catch (Exception e)
            {
                _log.LogWrite("error: " + e.Message);
            }
        }
        protected IDbConnection GetConnection()
        {
            try
            {
                var con = new SqlConnection(_configuration.GetConnectionString("stringConnect"));
                con.Open();
                return con;
            }
            catch (Exception e)
            {

                _log.LogWrite("error: " + e.Message);
                return null;
            }


        }



        protected DynamicParameters GetParams<T>(T model,
            string[] ignoreKey = null,
            string outputParam = "",
            DbType type = DbType.Int32)
        {
            var p = new DynamicParameters();
            if (!string.IsNullOrWhiteSpace(outputParam))
                p.Add(outputParam, dbType: type, direction: ParameterDirection.Output);
            var properties = model.GetType().GetProperties();
            foreach (var prop in properties)
            {
                var key = prop.Name;
                var value = prop.GetValue(model);
                if (ignoreKey != null && ignoreKey.Contains(key))
                    continue;
                if (!string.IsNullOrWhiteSpace(outputParam))
                {
                    if (key.ToLower() == outputParam.ToLower())
                    {
                        continue;
                    }
                }
                if (!string.IsNullOrWhiteSpace(key))
                    p.Add(key, value);
            }
            return p;
        }
        protected void ProcessInputPaging(ref int page, ref int limit, out int offset)
        {
            page = page <= 0 ? 1 : page;
            if (limit <= 0)
                limit = 20;
            if (limit > 1000)
                limit = 100;
            offset = (page - 1) * limit;
        }

        public async Task<bool> ExecuteSQL(string sql = "",
            object parameter = null,
            CommandType commandType = CommandType.StoredProcedure)
        {


            if (string.IsNullOrEmpty(sql) || parameter == null)
            {
                return false;
            }
            try
            {
                using (var _con = GetConnection())
                {

                    var result = await _con.ExecuteAsync(sql, param: parameter, commandType: commandType);

                    return true;
                }
            }
            catch (Exception e)
            {
                _log.LogWrite(e.Message);
                return false;

            }

        }
        public async Task<bool> ExecuteSQLText(string sqlText = "")
        {

            if (string.IsNullOrEmpty(sqlText))
            {
                return false;
            }
            try
            {
                using (var _con = GetConnection())
                {
                    var result = await _con.ExecuteAsync(sqlText, commandType: CommandType.Text);
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;

            }

        }




        public async Task<T> FindOneByStatementSql<T>(string sqlText,
            object param) where T : class, new()
        {



            try
            {
                using (var _con = GetConnection())
                {
                    var result =
                        await _con.QueryAsync<T>(sqlText, param,
                        commandType: CommandType.Text);
                    if (result == null)
                    {
                        return new T();
                    }
                    return result.FirstOrDefault();
                }
            }
            catch (Exception)
            {
                return new T();

            }
        }

        public async Task<List<T>> GetAllByStatementSql<T>(string sqlText,
            object param) where T : class, new()
        {
            try
            {
                using (var _con = GetConnection())
                {
                    var result =
                        await _con.QueryAsync<T>(sqlText, param,
                        commandType: CommandType.Text);
                    if (result == null)
                    {
                        return new List<T>();
                    }
                    return result.ToList();
                }
            }
            catch (Exception e)
            {
                return new List<T>();

            }
        }

        public async Task<IEnumerable<TIndexModel>> ExecutePro<TIndexModel>(string sql = "",
            object parameter = null,
            CommandType commandType = CommandType.StoredProcedure)


        {
            if (string.IsNullOrEmpty(sql))
            {
                return new List<TIndexModel>();
            }
            if (parameter == null)
            {
                parameter = new DynamicParameters();
            }

            try
            {
                using (var _con = GetConnection())
                {
                    var result = await _con.QueryAsync<TIndexModel>(sql, param: parameter, commandType: commandType);

                    if (result.Any())
                    {
                        return result.ToList();
                    }
                    return new List<TIndexModel>();
                }
            }
            catch (Exception e)
            {
                return new List<TIndexModel>();

            }
        }


        public async Task<List<TIndexModel>> ExecuteSqlProcerduceToList<TIndexModel>(string sql = "",
            object parameter = null,
            CommandType commandType = CommandType.StoredProcedure)
        {
            if (string.IsNullOrEmpty(sql))
            {
                return new List<TIndexModel>();
            }
            if (parameter == null)
            {
                parameter = new DynamicParameters();
            }

            try
            {
                using (var _con = GetConnection())
                {
                    var result = await _con.QueryAsync<TIndexModel>(sql, param: parameter, commandType: commandType);

                    if (result.Any())
                    {
                        return result.ToList();
                    }
                    return new List<TIndexModel>();
                }
            }
            catch (Exception e)
            {
                return new List<TIndexModel>();

            }
        }

        public async Task<TIndexModel> ExecuteSqlProcerduceAndGetOan<TIndexModel>(string sql = "",
           object parameter = null,
           CommandType commandType = CommandType.StoredProcedure) where TIndexModel : class, new()
        {
            if (string.IsNullOrEmpty(sql))
            {
                return new TIndexModel();
            }
            if (parameter == null)
            {
                parameter = new DynamicParameters();
            }

            try
            {
                using (var _con = GetConnection())
                {
                    var result = await _con.QueryAsync<TIndexModel>(sql, param: parameter, commandType: commandType);

                    if (result.Any())
                    {
                        return result.First();
                    }
                    return new TIndexModel();
                }
            }
            catch (Exception e)
            {
                return new TIndexModel();

            }
        }


        public async Task<T> ExecuteSQL<T>(string sql = "",
            object parameter = null)
            where T : BaseModel, new()

        {
            if (string.IsNullOrEmpty(sql) || parameter == null)
            {
                return new T()
                {
                    Id = -1
                };
            }

            using (var _con = GetConnection())
            {
                var result = await _con.QuerySingleOrDefaultAsync<T>(sql, param: parameter);

                if (result == null)
                {
                    return new T()
                    {
                        Id = -1
                    };
                }
                return result;
            }

        }
        public async Task<T> GetByMail<T>(string email) where T : BaseModel, new()
        {
            var _baseTable = tableName;
            var sql = "SELECT * FROM " + "[" + _baseTable + "]" + " WHERE email = @email";
            T result;
            try
            {
                using (var _con = GetConnection())
                {
                    result = await _con.QueryFirstOrDefaultAsync<T>(sql, param: new
                    {
                        email
                    });

                    if (result == null)
                    {
                        result = new T() { Id = -1 };
                    }

                }
            }
            catch (Exception)
            {

                result = new T() { Id = -1 };
            }
            return result;

        }
        public async Task<T> GetByIdBase<T>(int id) where T : BaseModel, new()
        {
            var _baseTable = tableName;
            var sql = "SELECT * FROM " + "[" + _baseTable + "]" + " WHERE Id = @id";
            T result;
            try
            {
                using (var _con = GetConnection())
                {
                    result = await _con.QueryFirstOrDefaultAsync<T>(sql, param: new
                    {
                        id
                    });

                    if (result == null)
                    {
                        result = new T() { Id = -1 };
                    }

                }
            }
            catch (Exception)
            {

                result = new T() { Id = -1 };
            }
            return result;

        }

        public async Task<bool> ExecuteSqlProcedure(string sql = "",
         object parameter = null)

        {
            if (string.IsNullOrEmpty(sql))
            {
                return false;
            }
            if (parameter == null)
            {
                parameter = new DynamicParameters();
            }

            try
            {
                using (var _con = GetConnection())
                {
                    var result = await _con.ExecuteScalarAsync(sql, param: parameter, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception e)
            {

                return false;

            }

        }

        public async Task<T> ExecuteSqlProcedure<T>(string sql = "",
          object parameter = null)
          where T : class, new()

        {
            if (string.IsNullOrEmpty(sql))
            {
                return null;
            }
            if (parameter == null)
            {
                parameter = new DynamicParameters();
            }

            try
            {
                using (var _con = GetConnection())
                {
                    var result = await _con.QuerySingleOrDefaultAsync<T>(sql, param: parameter);

                    if (result == null)
                    {
                        return new T()
                        {

                        };
                    }
                    return result;
                }
            }
            catch (Exception e)
            {

                return new T()
                {

                };

            }

        }

        public async Task<bool> ExecuteStatementSql(string sql = "",
         object parameter = null)

        {

            if (string.IsNullOrEmpty(sql))
            {
                return false;
            }
            if (parameter == null)
            {
                return false;
            }

            try
            {
                using (var _con = GetConnection())
                {
                    var result = await _con.ExecuteAsync(sql, param: parameter);

                    if (result == null)
                    {
                        return false;
                    }
                    return true;
                }
            }
            catch (Exception e)
            {
                _log.LogWrite(e.Message);
                return false;
            }

        }
        public async Task<bool> DeleteBase(int id, int delete = 1)
        {
            var _baseTable = tableName;

            var sql = "UPDATE " + "[" + _baseTable + "]" + " SET Deleted= @del  WHERE Id = @id";
            return await ExecuteStatementSql(sql, new
            {
                Id = id,
                del = delete
            });
        }
        public async Task<bool> DeleteBase(int id, int delete = 1, string tableDelete = "")
        {
            var _baseTable = tableName;
            if (!string.IsNullOrEmpty(tableDelete))
            {
                tableName = tableDelete;
            }
            var sql = "delete from  " + "[" + _baseTable + "]" + "  WHERE Id = @id";
            return await ExecuteStatementSql(sql, new
            {
                Id = id,
                del = delete
            });
        }


        public async Task<bool> DeleteReal(int id, int delete = 1, string tableDelete = "")
        {

            var _baseTable = tableName;
            if (!string.IsNullOrEmpty(tableDelete))
            {
                tableName = tableDelete;
            }
            var sql = "delete from  " + "[" + _baseTable + "]" + "  WHERE Id = @id";
            return await ExecuteStatementSql(sql, new
            {
                Id = id,
                del = delete
            });
        }

        public async Task<bool> UpdateBasic<T>(T itemModel) where T : class, new()
        {
            var properties = typeof(T).GetProperties();
            var parameters = new DynamicParameters();
            StringBuilder query = new StringBuilder();
            query.Append($"UPDATE {tableName} SET ");
            foreach (var item in properties)
            {
                var tempCol = item.Name;
                var tempVal = item.GetValue(itemModel);
                parameters.Add(tempCol, tempVal);
                if (tempCol.ToLower() == "id")
                {
                    continue;
                }
                query.Append($"{tempCol} = @{tempCol},");
            }
            query.Remove(query.Length - 1, 1);
            query.Append($" WHERE id = @Id ");

            try
            {
                using (var _con = GetConnection())
                {
                    var result =
                        await _con.ExecuteAsync(query.ToString(),
                        parameters, commandType: CommandType.Text);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;

            }
        }


        public async Task<BaseList> GetBaseAll<TIndexModel>(
             dynamic parameter,
             string sqlPro = ""
            ) where
            TIndexModel : BaseIndexModel
        {
            try
            {
                using (var con = GetConnection())
                {
                    var sqlGet = sqlGetALl;
                    if (!string.IsNullOrEmpty(sqlPro))
                    {
                        sqlGet = sqlPro;
                    }
                    var result = await con.QueryAsync<TIndexModel>(sqlGet, parameter as object, commandType: CommandType.StoredProcedure);
                    var fistElement = result.FirstOrDefault();
                    var totalRecord = 0;
                    if (fistElement != null)
                    {
                        totalRecord = fistElement.TotalRecord;
                    }
                    var reponse = new BaseList()
                    {
                        Total = totalRecord,
                        Data = result
                    };
                    return reponse;
                }
            }
            catch (Exception e)
            {
                return new BaseList()
                {
                    Data = null,
                    Total = 0,
                };
            }
        }

        public async Task<TResult> GetData<TResult, TRequest, TindexModel>(
             TRequest request,
             string sql
             ) where TResult : BaseList, new()
             where TRequest : BaseRequest, new()
             where TindexModel : BaseIndexModel, new()
        {
            int page = request.Page;
            int limit = request.Limit;
            ProcessInputPaging(ref page, ref limit, out offset);

            if (string.IsNullOrEmpty(sql))
                return new TResult();
            try
            {
                using (var con = GetConnection())
                {
                    var result = await con.QueryAsync<TindexModel>(sql,
                    request, commandType: CommandType.StoredProcedure);

                    var fistElement = result.FirstOrDefault();
                    var totalRecord = 0;
                    if (fistElement != null)
                    {

                        totalRecord = 10;
                    }
                    var reponse = new TResult()
                    {
                        Total = totalRecord,
                        Data = result
                    };

                    return reponse;
                }
            }
            catch (Exception e)
            {
                return new TResult();
            }
        }
    }
}
