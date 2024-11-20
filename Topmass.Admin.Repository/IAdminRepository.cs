using System.Data;
using Topmass.Campagn.Repository;
using Topmass.Core.Repository;

namespace Topmass.Admin.Repository
{
    public interface IAdminRepository
    {
        public ICompanyInfoRepository CompanyInfoRepository { get; set; }
        public IForgetPasswordRepository ForgetPasswordRepository { get; set; }
        public IEmployeeeRepository EmployeeeRepository { get; set; }
        public INTDRepository NTDRepository { get; set; }
        public IJobAdminRepository JobAdminRep { get; set; }
        public IMasterDataRepository MasterDataRepository { get; set; }
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
        public Task<bool> ExecuteStatementSql(string sql = "",
        object parameter = null);

    }
}
