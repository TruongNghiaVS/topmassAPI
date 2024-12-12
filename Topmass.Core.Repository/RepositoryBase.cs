using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Text;
using Topmass.Core.Model;

namespace Topmass.Core.Repository
{
    public class RepositoryBase<TModel> : BaseDataAccess,
        IBaseRepository<TModel> where TModel : BaseModel, new()
    {
        public RepositoryBase(IConfiguration configuration) : base(configuration)
        {

        }
        public async Task<bool> ExceueProdure(string sqlPro, dynamic param)
        {
            if (string.IsNullOrEmpty(sqlPro))
            {
                return false;
            }
            return await this.ExecuteSQL(sqlPro, param);
        }

        public async Task<bool> AddOrUPdate(TModel itemModel)
        {
            if (itemModel.Id < 1)
                return await Add(itemModel);
            else
                return await Update(itemModel);

        }
        public async Task<int> AddAndGetId(TModel itemModel)
        {
            var intoTable = tableName;

            var properties = itemModel.GetType().GetProperties();
            var parameters = new DynamicParameters();
            StringBuilder query = new StringBuilder();
            string[] ignore = { "UpdatedBy", "Id","UpdateAt", "CreateAt",
            "Deleted"};
            query.Append($"insert into  {intoTable} (  ");



            var columnInsert = new List<string>();
            var valuesInser = new List<string>();
            foreach (var item in properties)
            {


                var tempCol = item.Name;
                var tempVal = item.GetValue(itemModel);
                if (tempCol.ToLower() == "id")
                {
                    continue;
                }
                if (ignore.Contains(tempCol))
                {
                    continue;
                }
                columnInsert.Add(tempCol);
                valuesInser.Add("@" + tempCol);

                parameters.Add(tempCol, tempVal);


            }
            var columnInserttext = string.Join(",", columnInsert.ToArray());
            var valuesInsertText = string.Join(",", valuesInser.ToArray());
            var sqlInsertStatement = "insert into " + tableName + " (" + columnInserttext + " )" + "values  (" + valuesInsertText + ")";

            sqlInsertStatement += ";";

            sqlInsertStatement += "SELECT CAST(SCOPE_IDENTITY() as int) ";




            try
            {
                using (var _con = GetConnection())
                {
                    var result =
                        await _con.QuerySingleAsync<int>(sqlInsertStatement,
                        parameters);
                    return result;
                }
            }
            catch (Exception)
            {
                return -1;

            }
        }



        public async Task<bool> Insert<TModelInsert>(TModelInsert itemModel, string tableInsert = "") where TModelInsert : class, new()
        {
            if (string.IsNullOrWhiteSpace(tableInsert))
            {
                return false;
            }
            return await Add(itemModel, tableInsert);

        }
        public async Task<bool> Delete(int id)
        {
            return await DeleteBase(id, tableDelete: tableName);
        }

        public async Task<bool> Delete(TModel itemModel)
        {
            if (itemModel.Id < 1)
            {
                return false;
            }
            return await DeleteBase(itemModel.Id, tableDelete: tableName);
        }

        public async Task<IEnumerable<TModel>> GetAllBase()
        {
            var sqlText = " select top 1000 * from " + tableName + " order by Id desc";
            var result = await ExecutePro<TModel>(sqlText, null, System.Data.CommandType.Text);
            return result;
        }


        public async Task<List<TModel>> GetAllToList()
        {
            var sqlText = " select top 1000 * from " + tableName + " order by Id asc";
            var result = await ExecuteSqlProcerduceToList<TModel>(sqlText, null, System.Data.CommandType.Text);
            return result;
        }

        public async Task<TModel> GetById(int id)
        {
            return await GetByIdBase<TModel>(id);
        }
        public async Task<TModel> GetByUserName(string email)
        {
            return await GetByMail<TModel>(email);
        }


        private async Task<bool> Update(TModel itemModel)
        {
            var itemUpdate = await GetById(itemModel.Id);


            return await UpdateBasic(itemModel);
        }
        private async Task<bool> Add<TItem>(TItem itemModel, string tableInsert = "") where TItem : class, new()
        {
            var intoTable = tableInsert;
            if (string.IsNullOrEmpty(tableInsert))
            {
                intoTable = tableName;
            }
            var properties = itemModel.GetType().GetProperties();
            var parameters = new DynamicParameters();
            StringBuilder query = new StringBuilder();
            string[] ignore = { "UpdatedBy", "Id","UpdateAt", "CreateAt",
            "Deleted"};
            query.Append($"insert into  {intoTable} (  ");



            var columnInsert = new List<string>();
            var valuesInser = new List<string>();
            foreach (var item in properties)
            {


                var tempCol = item.Name;
                var tempVal = item.GetValue(itemModel);
                if (tempCol.ToLower() == "id")
                {
                    continue;
                }
                if (ignore.Contains(tempCol))
                {
                    continue;
                }
                columnInsert.Add(tempCol);
                valuesInser.Add("@" + tempCol);

                parameters.Add(tempCol, tempVal);


            }
            var columnInserttext = string.Join(",", columnInsert.ToArray());
            var valuesInsertText = string.Join(",", valuesInser.ToArray());
            var sqlInsertStatement = "insert into " + tableName + " (" + columnInserttext + " )" + "values  (" + valuesInsertText + ")";
            try
            {
                using (var _con = GetConnection())
                {
                    var result =
                        await _con.ExecuteAsync(sqlInsertStatement,
                        parameters, commandType: CommandType.Text);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;

            }
        }
        private async Task<bool> Add2(TModel itemModel)
        {
            string[] ignore = { nameof(itemModel.UpdatedBy), nameof(itemModel.Id), nameof(itemModel.UpdateAt), nameof(itemModel.CreateAt),
            nameof(itemModel.Deleted)};
            var columnInsert = "";
            var valueInsert = "";

            var properties = itemModel.GetType().GetProperties();
            foreach (var item in properties)
            {
                var nameProper = item.Name;
                var valueProper = item.GetValue(itemModel);
                if (valueProper == null)
                {
                    continue;
                }
                columnInsert += nameProper + ",";
                valueInsert += valueProper + ",";
            }
            if (columnInsert.EndsWith(","))
            {
                columnInsert = columnInsert.Remove(columnInsert.Length - 1);
            }
            if (valueInsert.EndsWith(","))
            {
                valueInsert = valueInsert.Remove(valueInsert.Length - 1);

            }
            var sqlText = " insert into table " + tableName + " (" + columnInsert + ") values (" + valueInsert + ") ";
            return await ExecuteSQLText(sqlText);
        }
    }
}
