using Microsoft.Extensions.Configuration;
using Topmass.Core.Model;

namespace Topmass.Core.Repository
{
    public partial class ActiveCodeMemberRepository : RepositoryBase<ActiveCodeMember>, IActiveCodeMemberRepository
    {
        public ActiveCodeMemberRepository(IConfiguration configuration) : base(configuration)
        {
            tableName = "ActiveCodeMember";
        }




    }
}
