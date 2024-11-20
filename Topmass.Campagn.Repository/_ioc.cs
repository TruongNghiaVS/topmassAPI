using Microsoft.Extensions.DependencyInjection;

namespace Topmass.Campagn.Repository
{
    public static class DependencyRegister
    {
        public static void ConfigRepCampagn(this IServiceCollection services)
        {
            services.AddSingleton<ICampagnExRepository, CampagnExRepository>();
        }

    }
}
