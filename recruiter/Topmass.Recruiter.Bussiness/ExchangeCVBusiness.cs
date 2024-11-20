using Topmass.Core.Model.Reward;
using Topmass.Core.Repository;
using Topmass.Recruiter.Bussiness.Model;
using Topmass.Recruiter.Repository;
using TopMass.Core.Result;

namespace Topmass.Recruiter.Bussiness
{
    public partial class ExchangeCVBusiness : IExchangeCVBusiness
    {
        private readonly ITicketItemRepository _ticketItemRepository;
        private readonly IExchangeCVRepository _exchangeCVRepository;
        private readonly IExchangeCVDetailRepository _exchangeCVDetailRepository;
        public ExchangeCVBusiness(
            ITicketItemRepository ticketItemRepository,
            IExchangeCVRepository exchangeCVRepository,
            IExchangeCVDetailRepository exchangeCVDetailRepository
            )
        {
            _ticketItemRepository = ticketItemRepository;
            _exchangeCVRepository = exchangeCVRepository;
            _exchangeCVDetailRepository = exchangeCVDetailRepository;
        }

        public async Task<BaseResult> AddExchange(ExchangeCVRequestAdd request)
        {
            var reponse = new BaseResult();
            var iteminsert = new ExchangeCV()
            {
                BusinessTime = DateTime.Now,
                CreateAt = DateTime.Now,
                CreatedBy = request.UserId,
                Point = request.Point,
                Title = request.Title,
                Status = 0,
                Deleted = false,
                UpdateAt = DateTime.Now,
                UpdatedBy = request.UserId,
                Position = request.Position,
                Rank = request.Rank,
                Experience = request.Experience
            };

            var idInsert = await _exchangeCVRepository.AddAndGetId(iteminsert);

            foreach (var item in request.LinkCVs)
            {
                var detailCv = new ExchangeCVDetail()
                {
                    BusinessTime = DateTime.Now,
                    CreateAt = DateTime.Now,
                    Linkfile = item.LinkFile,
                    CreatedBy = request.UserId,
                    RelId = idInsert,
                    Noted = "",
                    Status = 1,
                    Point = 0
                };

                await _exchangeCVDetailRepository.AddOrUPdate(detailCv);
            };
            return reponse;
        }
        public async Task<BaseResult> GetHistory(int Status, int userId)
        {
            var reponse = new BaseResult();
            var sqlString = "select * from exchangeCV  where createdBy = @createdBy and status != @status  order by id desc ";
            if (Status > 0)
            {
                sqlString = "select * from exchangeCV  where createdBy = @createdBy and status = @status order by id desc";
            }
            var dataAll = await _exchangeCVRepository.GetAllByStatementSql<ExchangeCVDisplay>(sqlString, new
            {
                createdBy = userId,
                status = Status
            });

            reponse.Data = dataAll;
            return reponse;
        }
        public async Task<BaseResult> GetDetail(int id)
        {
            var reponse = new BaseResult();
            var dataItem = await _exchangeCVRepository.FindOneByStatementSql<ExchangeCVDesiplay>
                ("select  *, dbo.getNameMaster(e.Rank) as RankName, dbo.getNameMaster(e.Experience) as ExperienceName from exchangeCV e where e.id = @id ",
                new
                {
                    id = id
                });
            if (dataItem.Id < 1)
            {
                reponse.AddError("Id", "Không có thông tin tìm thấy");
                reponse.Data = new
                {
                    id = -1
                };
                return reponse;
            }
            var allData = await _exchangeCVDetailRepository.GetAllByStatementSql<ExchangeCVDetailDisplay>("select * from exchangeCVDetail   where relId = @rel",

                new { rel = id });
            if (allData == null | allData.Count < 1)
            {

                allData = new List<ExchangeCVDetailDisplay>();
            }
            var result = new
            {
                dataItem.Title,
                dataItem.Point,
                dataItem.Position,
                dataItem.Rank,
                dataItem.Status,
                dataItem.BusinessTime,
                dataItem.RankName,
                dataItem.ExperienceName,
                dataItem.Experience,
                LinkCVs = allData
            };
            reponse.Data = result;
            return reponse;
        }


        public async Task<bool> CancelExchange(int id)
        {
            var reponse = new BaseResult();
            var dataItem = await _exchangeCVRepository.GetById(id);
            //dataItem.Deleted = true;
            dataItem.UpdateAt = DateTime.Now;
            dataItem.Status = 3;
            return await _exchangeCVRepository.AddOrUPdate(dataItem);

        }

    }
}
