using Catalog.Api.Models.Responses.Base;

namespace Catalog.Api.Models.Responses
{
    public class SuccessBodyResponse<T> : Response
    {
        public SuccessBodyResponse(T body)
        {
            Body = body;
            IsSuccess = true;
        }

        public T Body { get; set; }
    }
}
