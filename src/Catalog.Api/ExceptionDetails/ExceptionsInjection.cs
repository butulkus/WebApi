using Catalog.Domain.Exceptions;
using Hellang.Middleware.ProblemDetails;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Api.ExceptionDetails
{
    public static class ExceptionsInjection
    {
        public static void AddExceptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddProblemDetails(setup =>
            {
                setup.Map<IncorrectProductIdException>(exception => new IncorrectProductIdDetails
                {
                    Title = exception.Title,
                    Detail = exception.Detail,
                    Status = StatusCodes.Status400BadRequest,
                    Type = exception.Type,
                    Instance = exception.Instance,
                    AdditionalInfo = exception.AdditionalInfo
                });
            });
        }
    }
}
