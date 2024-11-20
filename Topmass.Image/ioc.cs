namespace Topmass.Image
{
    public static class DependencyRegister
    {

        public static void ConfigIoc(this IServiceCollection services)
        {
            services.AddSingleton<IFileMedia, FileMedia>();


        }
    }
}
