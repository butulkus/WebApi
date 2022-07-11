namespace Catalog.Events.CatalogEvents
{
    public record CatalogItemPriceChangedEvent
    {
        public Guid ProductId { get; private init; }
        public decimal NewPrice { get; private init; }

        public CatalogItemPriceChangedEvent(Guid productId, decimal newPrice)
        {
            ProductId = productId;
            NewPrice = newPrice;
        }
    }
}
