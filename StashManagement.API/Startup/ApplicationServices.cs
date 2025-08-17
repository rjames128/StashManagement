using Microsoft.EntityFrameworkCore;
using StashManagement.API.Infrastructure;

namespace StashManagement.API.Startup
{
    public static class ApplicationServices
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StashDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("stash")));
            services.AddScoped<IStashItemRepository, StashItemRepository>();
        }
    }
}
