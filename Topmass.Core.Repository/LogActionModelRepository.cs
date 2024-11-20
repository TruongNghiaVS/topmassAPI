using Microsoft.Extensions.Configuration;
using Topmass.Core.Model.Campagn;
using Topmass.Core.Repository.Model;

namespace Topmass.Core.Repository
{
    public partial class LogActionModelRepository : RepositoryBase<LogActionModel>, ILogActionModelRepository
    {
        public LogActionModelRepository(IConfiguration configuration, IJobRepository jobRepository) : base(configuration)
        {
            tableName = "LogAction";
        }

        public async Task<List<LogActionModel>> GetAllHistory(GetAllHistoryRequest request)
        {

            var sqlStatement = "SELECT * from LogAction where  typeData  = @typedata and userId = @userId and source =@source and CreateAt >= @from and CreateAt <= @to order by CreateAt desc";
            var parramRequest = new
            {
                userId = request.UserId,
                typedata = request.Typedata,
                from = request.From,
                to = request.To,
                source = request.Source
            };
            var datas = await this.ExecuteSqlProcerduceToList<LogActionModel>(sqlStatement, parramRequest, System.Data.CommandType.Text);
            return datas;
        }

    }
}
