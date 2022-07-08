using Basket.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Basket.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddDataContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BasketDBContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("IdentityConnection"),
                    b => b.MigrationsAssembly("Basket.Infrastructure")));
        }
    }
}
