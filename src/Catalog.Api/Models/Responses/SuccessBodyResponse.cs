using Catalog.Api.Models.Responses.Base;

namespace Catalog.Api.Models.Responses
{
    public class SuccessBodyResponse : Response
    {
        public SuccessBodyResponse(object body)
        {
            Body = body;
            IsSuccess = true;
        }

        public object Body { get; set; }
    }
}
