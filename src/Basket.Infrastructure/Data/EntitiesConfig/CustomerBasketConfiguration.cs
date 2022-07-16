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

            builder.UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasKey("CustomerId");

            builder.UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasMany("Items")
                .WithMany("customerBaskets")
                .UsingEntity(j => j.ToTable("PotentialPurchases"));
        }
    }
}
