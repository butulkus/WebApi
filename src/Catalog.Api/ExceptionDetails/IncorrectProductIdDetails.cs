using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.ExceptionDetails
{
    public class IncorrectProductIdDetails : ProblemDetails
    {
        public string AdditionalInfo { get; set; }
    }
}
