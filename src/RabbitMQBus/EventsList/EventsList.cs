namespace RabbitMQBus.EventsAndEventHandlersList
{
    /// <summary>
    /// Tuple of event path
    /// </summary>
    public static class EventsList
    {
        /// <summary>
        /// first argument - event name,
        /// second argument - event full path with dll name,
        /// third argument - event handler full path with dll name
        /// </summary>
        public static readonly (string, string, string) CatalogItemPriceChangedEvent = (
            "CatalogItemPriceChangedEvent",
            "Basket.Api.IntegrationEvents.Events.CatalogItemPriceChangedEvent,Basket.Api",
            "Basket.Api.IntegrationEvents.EventHandlers.CatalogItemPriceChangedEventHandler,Basket.Api");

        public static IReadOnlyCollection<(string eventName, string eventPath, string eventHandlerPath)> Events= new List<(string, string, string)>
        {
            CatalogItemPriceChangedEvent
        }
        .AsReadOnly();
    }
}
