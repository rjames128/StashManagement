using StashManagement.API.Infrastructure;

namespace StashManagement.API.Startup
{
    public static class ApplicationServices
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IStashItemRepository, StashItemRepository>();
        }
    }
}
