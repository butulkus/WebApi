using Basket.Domain.Entities.BasketItemNS;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Basket.Infrastructure.Data.EntitiesConfig
{
    public class BasketItemConfiguration : IEntityTypeConfiguration<BasketItem>
    {
        public void Configure(EntityTypeBuilder<BasketItem> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Ignore(x => x.GetCustomerBaskets);
            builder.Ignore(x => x.GetProductName);
            builder.Ignore(x => x.GetPrice);
            builder.Ignore(x => x.GetOldPrice);

            builder.Property<string>("ProductName")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("ProductName")
                .IsRequired(true);

            builder.Property<decimal>("CurrentPrice")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("CurrentPrice")
                .IsRequired(true);

            builder.Property<decimal>("OldPrice")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("OldPrice")
                .IsRequired(true);

            builder.Property<string>("ItemHashCode")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("ItemHashCode")
                .IsRequired(true);
        }
    }
}
