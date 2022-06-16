using AutoMapper;
using Catalog.Api.Controllers.Base;
using Catalog.Core.Interfaces;
using Catalog.Domain.Exceptions;
using Catalog.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Catalog.Api.Models.Responses;
using Catalog.Domain.Models;

namespace Catalog.Api.Controllers
{
    public class CatalogController : BaseController
    {
        public readonly ICatalogService _catalogService;
        public CatalogController(
            IMapper mapper,
            ICatalogService catalogService
            ) : base(mapper)
        {
            _catalogService = catalogService;
        }

        [HttpGet]
        [Route("productsByType/{typeId:int}")]
        [ProducesResponseType(typeof(SuccessBodyResponse<CatalogItemModel[]>), StatusCodes.Status200OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<CatalogItemModel[]>> ProductsByTypeAsync(int typeId, [FromQuery]int pageSize = 10, [FromQuery]int pageIndex = 0)
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
        public async Task<ActionResult<CatalogItemModel>> GetProductsById(string productId)
        {
            if (string.IsNullOrEmpty(productId))
                throw new IncorrectProductIdException(Request.Path.Value);

            var products = await _catalogService.FindByIdWithIncludes(new Guid(productId));

            if (products is null)
                return BadRequest("Products with that id was not found");

            return Ok(products);
        }
    }
}
