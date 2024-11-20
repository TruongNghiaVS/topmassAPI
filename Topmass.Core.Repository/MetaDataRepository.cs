using Microsoft.Extensions.Configuration;

using Topmass.Core.Model.Profile;


namespace Topmass.Core.Repository
{
    public partial class MetaDataRepository : RepositoryBase<MetaDataPage>,
        IMetaDataRepository
    {


        public MetaDataRepository(IConfiguration configuration) : base(configuration)
        {
            tableName = "metadata";
        }

        public async Task<MetaDataPage> GetInfo(string keyScreen, int typedata = 0 )
        {
            var reponse = new MetaDataPage();
            var dataResult = await this.ExecuteSqlProcerduceAndGetOan<MetaDataPage>
                ("sp_getInfoMetaData", new {
                    keyScreen,
                    typedata= typedata
                },
                commandType: System.Data.CommandType.StoredProcedure);
   
            return reponse;
        }
       



    }
}
