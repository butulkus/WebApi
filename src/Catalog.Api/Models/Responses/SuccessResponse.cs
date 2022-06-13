using Catalog.Api.Models.Responses.Base;

namespace Catalog.Api.Models.Responses
{
    public class SuccessResponse : Response
    {
        public SuccessResponse()
        {
            IsSuccess = true;
        }
    }
}
