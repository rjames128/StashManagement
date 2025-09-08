using Amazon.Extensions.NETCore.Setup;
using Amazon.S3;
using Microsoft.EntityFrameworkCore;
using StashManagement.API.Configuration;
using StashManagement.API.Infrastructure;

namespace StashManagement.API.Startup
{
    public static class ApplicationServices
    {
        public static void ConfigureServices(this IServiceCollection services, AppSettings settings)
        {
            services.AddDbContext<StashDbContext>(options =>
                options.UseNpgsql(settings.ConnectionStrings.Stash));
            services.AddScoped<IStashItemRepository, StashItemRepository>();
            services.AddSingleton<AppSettings>(settings);
            // Add AWS S3 client configuration
            services.AddAWSService<IAmazonS3>(new AWSOptions 
            {
                Region = Amazon.RegionEndpoint.GetBySystemName(settings.AWS.Region),                
                DefaultClientConfig = 
                {
                    ServiceURL = settings.AWS.IsLocal ? settings.AWS.LocalUrl : null,
                    UseHttp = settings.AWS.IsLocal
                }
            });
        }
    }
}
