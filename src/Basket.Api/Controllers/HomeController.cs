using Microsoft.AspNetCore.Mvc;
using RabbitMQBus.Interfaces;

namespace Basket.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]/")]
    public class HomeController : Controller
    {
        private readonly IEventBus _eventBus; // make readonly if it works
        public HomeController(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        [HttpGet]
        [Route("productsByType/")]
        public async Task<IActionResult> GetProductsByTypeAsync()
        {
            return Ok();
        }
    }
}
