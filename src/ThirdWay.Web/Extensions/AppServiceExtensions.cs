using ThirdWay.Data;

namespace ThirdWay.Web.Extensions
{
    public static class AppServiceExtensions
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration config)
        {
            // Add services to the container.
            services.AddControllersWithViews();
            services.AddEntityFrameworkSqlite().AddDbContext<ReaderContext>();
            
            return services;
        }
    }
}
