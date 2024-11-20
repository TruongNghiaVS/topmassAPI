using System.Data;
using Topmass.Core.Model;

namespace Topmass.Core.Repository
{
    public interface IBaseRepository<T> where T : BaseModel, new()
    {

        Task<IEnumerable<T>> GetAllBase();
        public Task<List<T>> GetAllToList();
        public Task<bool> AddOrUPdate(T itemModel);
        public Task<int> AddAndGetId(T itemModel);
        public Task<bool> Delete(T itemModel);
        public Task<bool> Delete(int id);
        public Task<T> GetById(int id);

        public Task<T> GetByUserName(string email);
        public Task<TmodelGet> FindOneByStatementSql<TmodelGet>(string sqlText,
            object param) where TmodelGet : class, new();

        public Task<List<TmodelGet>> GetAllByStatementSql<TmodelGet>(string sqlText,
            object param) where TmodelGet : class, new();


        public Task<List<TIndexModel>> ExecuteSqlProcerduceToList<TIndexModel>
            (string sql = "",
           object parameter = null,
           CommandType commandType = CommandType.StoredProcedure);

        public Task<T> ExecuteSqlProcedure<T>(string sql = "",
            object parameter = null)
            where T : class, new();

        public Task<bool> ExecuteSqlProcedure(string sql = "",
         object parameter = null);

        public Task<bool> ExecuteStatementSql(string sql = "",
        object parameter = null);

    }
}
