namespace Catalog.DataContext.Entities
{
    public class CatalogItem
    {
        public Guid ItemId { get; set; }
        public string Name { get; set; }
        public string ItemHashCode { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CatalogTypeId { get; set; }
        public int CatalogBrandId { get; set; }

        public CatalogType CatalogType { get; set; }    
        public CatalogBrand CatalogBrand { get; set; }
    }
}
