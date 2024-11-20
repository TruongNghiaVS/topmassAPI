using Microsoft.Extensions.DependencyInjection;
using Topmass.Bussiness.Mail;
using Topmass.Core.Repository;

namespace Topmass.core.Business
{
    public static class DependencyRegister
    {
        public static void ConfigBusiness(this IServiceCollection services)
        {
            services.ConfigRep();
            services.AddSingleton<ICandidateBusiness, CandidateBusiness>();
            services.AddSingleton<IAuthenBuisiness, AuthenBuisiness>();

            services.AddSingleton<IProfileBusiness, ProfileBusiness>();
            services.AddSingleton<IMetaDataBussiness, MetaDataBusiness>();
            
            services.ConfigMailBusiness();
        }
    }
}
