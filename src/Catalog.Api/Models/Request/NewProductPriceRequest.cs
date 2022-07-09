namespace Catalog.Api.Models.Request
{
    public class NewProductPriceRequest
    {
        public Guid productId { get; set; }
        public decimal NewPrice { get; set; }
    }
}
