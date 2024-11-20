using Microsoft.Extensions.Configuration;
using Topmass.Core.Model.Support;

namespace Topmass.Core.Repository
{
    public partial class TicketItemRepository : RepositoryBase<TicketItemModel>, ITicketItemRepository
    {
        public TicketItemRepository(IConfiguration configuration) : base(configuration)
        {
            tableName = "TicketItem";
        }

    }
}
