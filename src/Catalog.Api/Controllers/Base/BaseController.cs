using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers.Base
{
    [ApiController]
    [Route("api/v1/[controller]/")]
    public class BaseController : ControllerBase
    {
        protected internal IMapper _mapper;
        public BaseController(IMapper mapper)
        {
            _mapper = mapper;
        }
    }
}
