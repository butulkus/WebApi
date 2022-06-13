namespace Catalog.Domain.Models
{
    public class CatalogItemModel
    {
        public Guid ItemId { get; set; }
        public string Name { get; set; }
        public string ItemHashCode { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CatalogTypeId { get; set; }
        public int CatalogBrandId { get; set; }

        public CatalogTypeModel CatalogType { get; set; }
        public CatalogBrandModel CatalogBrand { get; set; }
    }
}
