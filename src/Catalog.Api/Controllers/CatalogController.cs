using AutoMapper;
using Catalog.Api.Controllers.Base;
using Catalog.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers
{
    public class CatalogController : BaseController
    {
        public CatalogController(IMapper mapper) : base(mapper)
        {
            
        }

        [HttpGet]
        [Route("products/{id:int}")]
        public async Task<IActionResult> ProductByIdAsync(int id)
        {
            if (id <= 0)
                throw new IncorrectProductIdException(Request.Path.Value);

            return Ok();
        }
    }
}
