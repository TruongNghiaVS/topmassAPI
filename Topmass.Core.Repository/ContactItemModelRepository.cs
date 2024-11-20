using Microsoft.Extensions.Configuration;
using Topmass.Core.Model.Support;

namespace Topmass.Core.Repository
{
    public partial class ContactItemModelRepository : RepositoryBase<ContactItemModel>, IContactItemModelRepository
    {
        public ContactItemModelRepository(IConfiguration configuration) : base(configuration)
        {
            tableName = "ContactsRequest";
        }

    }
}
