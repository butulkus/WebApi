using Basket.Domain.Entities.CustomerBasketNS;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Basket.Infrastructure.Data.EntitiesConfig
{
    public class CustomerBasketConfiguration : IEntityTypeConfiguration<CustomerBasket>
    {
        public void Configure(EntityTypeBuilder<CustomerBasket> builder)
        {
            builder.Ignore(x => x.Id);

            builder.HasKey(x => x.GetCustomerId);

            builder.HasMany(x => x.GetItems)
                .WithMany(x => x.GetCustomerBaskets);
        }
    }
}
