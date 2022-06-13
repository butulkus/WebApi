using Catalog.DataContext.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Catalog.DataContext
{
    public class CatalogDBContext : DbContext
    {
        public CatalogDBContext(DbContextOptions<CatalogDBContext> options) : base(options)
        {
        }
        public DbSet<CatalogBrand> CatalogBrand { get; set; }
        public DbSet<CatalogItem> CatalogItem { get; set; }
        public DbSet<CatalogType> CatalogType { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
