using Topmass.Core.Repository;
using Topmass.Recruiter.Bussiness.Model;
using TopMass.Core.Result;

namespace Topmass.Recruiter.Bussiness
{
    public partial class SupportBusiness : ISupportBusiness
    {
        private readonly ITicketItemRepository _ticketItemRepository;
        public SupportBusiness(
            ITicketItemRepository ticketItemRepository
            )
        {
            _ticketItemRepository = ticketItemRepository;
        }

        public async Task<BaseResult> AddTicket(TicketItemAdd request)
        {
            var reponse = new BaseResult();

            var itemInfo = new Core.Model.Support.TicketItemModel()
            {
                Content = request.Content,
                LinkFile = request.LinkFile,
                Title = request.Title,
                Status = 1,
                CreatedBy = request.HandleBy,
                Deleted = false,
                CreateAt = DateTime.Now
            };
            await _ticketItemRepository.AddOrUPdate(itemInfo);
            return reponse;
        }



    }
}
