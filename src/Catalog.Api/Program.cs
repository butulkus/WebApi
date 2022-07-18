using Autofac;
using Autofac.Extensions.DependencyInjection;
using Catalog.Api.ExceptionDetails;
using Catalog.Api.Filters;
using Catalog.Api.Grpc;
using Catalog.Api.IntegrationEvents;
using Catalog.Application;
using Catalog.DataContext;
using Hellang.Middleware.ProblemDetails;
using Microsoft.OpenApi.Models;
using RabbitMQBus;
using RabbitMQBus.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDataContext(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);

builder.Services.AddTransient<ICatalogIntegrationEventService, CatalogIntegrationEventService>();

/// <summary> 
/// Autofac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterType<SubscriptionsManager>().As<ISubscriptionsManager>().SingleInstance();
    builder.RegisterType<RabbitMQEventBus>().As<IEventBus>().SingleInstance();
});
/// </summary> 
/// Autofac

builder.Services.AddGrpc();

builder.Services.AddSingleton(typeof(ILogger), builder.Services.BuildServiceProvider().GetService<ILogger<Program>>());

builder.Services
    .AddControllers()
    .AddMvcOptions(options =>
    {
        options.Filters.Add<ResponseFilter>();
    })
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
        options.InvalidModelStateResponseFactory = c =>
        {
            var errors = c.ModelState.Values.Where(v => v.Errors.Count > 0)
                .SelectMany(v => v.Errors)
                .Select(v => v.ErrorMessage);

            string json = JsonSerializer.Serialize(errors);
            throw new ValidationException(json);
        };
    });

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddProblemDetails(setup =>
{
    setup.IncludeExceptionDetails = (ctx, env) => builder.Environment.IsDevelopment() || builder.Environment.IsStaging();
});
builder.Services.AddExceptions(builder.Configuration);

var pathForSwagger = builder.Configuration["PATH_FOR_SWAGGER"];

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "WebApi Catalog",
        Version = "v1",
        Description = "Catalog for microservices"
    });
});

var app = builder.Build();

if (true/*app.Environment.IsDevelopment()*/)
{
    app.UseSwagger(c =>
    {
        c.SerializeAsV2 = true;
    });

    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint($"{(!string.IsNullOrEmpty(pathForSwagger) ? pathForSwagger : string.Empty)}/swagger/v1/swagger.json", "Catalog.Api V1");
    });
}

app.UseProblemDetails();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapGrpcService<CatalogService>();
});

app.MapControllers();

app.Run();
