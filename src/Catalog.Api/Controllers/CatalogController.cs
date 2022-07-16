using AutoMapper;
using Catalog.Api.Controllers.Base;
using Catalog.Core.Interfaces;
using Catalog.Domain.Exceptions;
using Catalog.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Catalog.Api.Models.Responses;
using Catalog.Domain.Models;
using Catalog.Api.Models.Request;
using Catalog.Api.IntegrationEvents;
using Catalog.Api.IntegrationEvents.Events;

namespace Catalog.Api.Controllers
{
    public class CatalogController : BaseController
    {
        private readonly ICatalogService _catalogService;
        private readonly ICatalogIntegrationEventService _catalogIntegrationEventService;

        public CatalogController(
            IMapper mapper,
            ICatalogService catalogService,
            ICatalogIntegrationEventService catalogIntegrationEventService) : base(mapper)
        {
            _catalogService = catalogService;
            _catalogIntegrationEventService = catalogIntegrationEventService;
        }

        [HttpGet]
        [Route("productsByType/{typeId:int}")]
        [ProducesResponseType(typeof(SuccessBodyResponse<CatalogItemModel[]>), StatusCodes.Status200OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<List<CatalogItemModel>>> GetProductsByTypeAsync(int typeId, [FromQuery]int pageSize = 10, [FromQuery]int pageIndex = 0)
        {
            if (typeId <= 0)
                throw new IncorrectProductIdException(Request.Path.Value);

            var products = await _catalogService.GetAllItemsByTypeWithPagging(pageSize, pageIndex, typeId);

            if (!products.Any())
               return BadRequest("Products with that type was not found");

            return Ok(products);
        }

        [HttpGet]
        [Route("productById/{productId}")]    
        [ProducesResponseType(typeof(SuccessBodyResponse<CatalogItemModel>), StatusCodes.Status200OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<CatalogItemModel>> ProductById(string productId)
        {
            if (string.IsNullOrEmpty(productId))
                throw new IncorrectProductIdException(Request.Path.Value);

            var products = await _catalogService.FindByIdWithIncludes(new Guid(productId));

            if (products is null)
                return BadRequest("Product with that id was not found");

            return Ok(products);
        }

        [HttpGet]
        [Route("products")]
        [ProducesResponseType(typeof(SuccessBodyResponse<CatalogItemModel>), StatusCodes.Status200OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<List<CatalogItemModel>>> GetAllProducts([FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
        {
            var products = await _catalogService.GetAllItemsWithPagging(pageSize, pageIndex);

            if (!products.Any())
                return BadRequest("Products was not found");

            return Ok(products);
        }

        [HttpPost]
        [Route("updatePrice")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateCatalogItemPrice([FromBody] NewProductPriceRequest newProductPriceRequest)
        {
            if (newProductPriceRequest.NewPrice <= 0)
                throw new ArgumentException("Price cannot be less or equal zero");

            var products = await _catalogService.UpdateItem(newProductPriceRequest.productId, newProductPriceRequest.NewPrice);

            if (products == 0)
                return Ok("Price wasn't changed");

            var itemPriceChangedEvent = new CatalogItemPriceChangedEvent(newProductPriceRequest.productId, newProductPriceRequest.NewPrice);
            _catalogIntegrationEventService.Publish(itemPriceChangedEvent);

            return Ok(newProductPriceRequest.productId);
        }
    }
}
