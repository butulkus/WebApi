using Catalog.DataContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.DataContext.Configuration
{
    public class CatalogBrandConfiguration : IEntityTypeConfiguration<CatalogBrand>
    {
        public void Configure(EntityTypeBuilder<CatalogBrand> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Brand)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
