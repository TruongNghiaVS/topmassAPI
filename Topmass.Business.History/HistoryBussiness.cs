using Topmass.Business.History.model;
using Topmass.Core.Model.Campagn;
using Topmass.Core.Repository;

namespace Topmass.Business.History
{
    public class HistoryBussiness : IHistoryBussiness
    {
        private readonly ILogActionModelRepository _logActionModelRepository;

        public HistoryBussiness(ILogActionModelRepository logActionModelRepository)
        {
            _logActionModelRepository = logActionModelRepository;
        }
        public async Task<bool> Add(HIstoryDataRequestAdd request)
        {

            var itemAdd = new LogActionModel()
            {
                Actor = request.Actor,
                BusinessTime = DateTime.Now,
                Content = request.Content,
                CreateAt = DateTime.Now,
                TypeData = request.TypeData,
                CreatedBy = request.UserId,
                UserId = request.UserId,
                Deleted = false,
                Status = 0,
                UpdateAt = DateTime.Now,
                Source = request.Source,
                UpdatedBy = request.UserId
            };
            await _logActionModelRepository.AddOrUPdate(itemAdd);
            return true;
        }

        public async Task<HIstoryDataReponse> GetAccessLog(int userId, int source = 2, int typedata = 1,

            DateTime? from = null, DateTime? to = null
            )
        {
            var reponse = new HIstoryDataReponse();
            var dataAll = await _logActionModelRepository.GetAllHistory(new Core.Repository.Model.GetAllHistoryRequest()
            {
                Source = source,
                Typedata = typedata,

                UserId = userId,
                From = from,
                To = to
            });
            var listData = new List<HIstoryDataDisplay>();
            foreach (var item in dataAll)
            {
                var dateGroup = item.BusinessTime.Value.ToShortDateString();
                var itemExit = listData.Where(x => x.GroupDate == dateGroup).FirstOrDefault();
                if (itemExit == null)
                {
                    itemExit = new HIstoryDataDisplay();
                    itemExit.GroupDate = dateGroup;
                    listData.Add(itemExit);
                }
                var itemData = new HIstoryDataItem()
                {
                    Content = item.Content,
                    TimeBusiness = item.BusinessTime
                };
                itemExit.Data.Add(itemData);
            }
            reponse.Data = listData;
            return reponse;

            //reponse.Data = new List<HIstoryDataDisplay>()
            //{
            //   new HIstoryDataDisplay()
            //   {
            //       GroupDate ="29/09/2024",
            //       Data = new List<HIstoryDataItem>
            //       {
            //          new HIstoryDataItem()
            //          {
            //              Content ="Đăng nhập",
            //              TimeBusiness = DateTime.Now.AddDays(-1)

            //          },
            //             new HIstoryDataItem()
            //          {
            //              Content ="Đăng xuất",
            //              TimeBusiness = DateTime.Now.AddDays(-1)

            //          }
            //       }
            //   },
            //    new HIstoryDataDisplay()
            //   {
            //       GroupDate ="30/09/2024",
            //       Data = new List<HIstoryDataItem>
            //       {
            //          new HIstoryDataItem()
            //          {
            //              Content ="Đăng nhập",
            //              TimeBusiness = DateTime.Now.AddDays(0)

            //          },
            //             new HIstoryDataItem()
            //          {
            //              Content ="Đăng xuất",
            //              TimeBusiness = DateTime.Now.AddDays(0)

            //          }
            //       }
            //   }
            //};

            //return reponse;



        }
    }
}
