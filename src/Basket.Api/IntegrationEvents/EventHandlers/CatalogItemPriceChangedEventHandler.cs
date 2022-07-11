namespace Basket.Api.IntegrationEvents.EventHandlers
{
    public class CatalogItemPriceChangedEventHandler
    {
        private readonly ILogger<CatalogItemPriceChangedEventHandler> _logger;
        private readonly IBasketService _basketService;

        public CatalogItemPriceChangedEventHandler(
            ILogger<CatalogItemPriceChangedEventHandler> logger,
            IBasketService basketService)
        {
            _logger = logger;
            _basketService = basketService;
        }
    }
}
