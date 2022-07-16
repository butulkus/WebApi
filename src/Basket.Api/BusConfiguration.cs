using RabbitMQBus.EventsAndEventHandlersList;
using RabbitMQBus.Interfaces;

namespace Basket.Api
{
    public static class BusConfiguration
    {
        public static void ConfigureEventBus(this IApplicationBuilder app)
        {
            var eventbus = app.ApplicationServices.GetRequiredService<IEventBus>();

            eventbus.Subscribe("CatalogItemPriceChangedEvent");
        }
    }
}
