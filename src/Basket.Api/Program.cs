using Autofac;
using Autofac.Extensions.DependencyInjection;
using Basket.Api;
using Basket.Api.IntegrationEvents.EventHandlers;
using Basket.Domain.Interfaces;
using Basket.Infrastructure;
using Basket.Infrastructure.Data.Repositories;
using Basket.Infrastructure.Services;
using Microsoft.OpenApi.Models;
using RabbitMQBus;
using RabbitMQBus.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDataContext(builder.Configuration);
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddScoped<IBasketService, BasketService>();

builder.Services.AddTransient<CatalogItemPriceChangedEventHandler>();

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

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services
    .AddControllers()
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

var pathForSwagger = builder.Configuration["PATH_FOR_SWAGGER"];

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "WebApi Basket",
        Version = "v1",
        Description = "Basket for microservices"
    });
});

var app = builder.Build();

app.UseSwagger(c =>
{
    c.SerializeAsV2 = true;
});

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint($"{(!string.IsNullOrEmpty(pathForSwagger) ? pathForSwagger : string.Empty)}/swagger/v1/swagger.json", "Basket.Api V1");
});

app.ConfigureEventBus();

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();
