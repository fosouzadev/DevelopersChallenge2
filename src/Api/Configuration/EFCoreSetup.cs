using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository.Context;

namespace Api.Configuration
{
    public static class EFCoreSetup
    {
        public static  void AddEFCore(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EFContext>(c => c.UseSqlite(configuration.GetConnectionString("Sqlite")));
        }
    }
}