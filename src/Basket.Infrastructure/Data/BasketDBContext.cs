using Basket.Domain.Entities.BasketItemNS;
using Basket.Domain.Entities.CustomerBasketNS;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Basket.Infrastructure.Data
{
    public class BasketDBContext : DbContext
    {
        public BasketDBContext(DbContextOptions<BasketDBContext> options) : base(options)
        {
        }

        public DbSet<BasketItem> basketItems { get; set; }
        public DbSet<CustomerBasket> customerBaskets { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
