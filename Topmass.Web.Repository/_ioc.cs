using Microsoft.Extensions.DependencyInjection;
using Topmass.Core.Repository;

namespace Topmass.Web.Repository
{
    public static class DependencyRegister
    {

        public static void ConfigRep(this IServiceCollection services)
        {
            services.AddSingleton<IArticleRepository, ArticleRepository>();
            services.AddSingleton<ICategoryAritcleRepository, CategoryAritcleRepository>();
            services.AddSingleton<IContactItemModelRepository, ContactItemModelRepository>();
        }
    }
}
