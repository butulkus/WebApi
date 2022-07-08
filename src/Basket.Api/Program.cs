using Basket.Infrastructure;
using Microsoft.OpenApi.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDataContext(builder.Configuration);

builder.Services
    .AddControllers()
    //.AddMvcOptions(options =>
    //{
    //    options.Filters.Add<ResponseFilter>();
    //})
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

if (true/*app.Environment.IsDevelopment()*/)
{
    app.UseSwagger(c =>
    {
        c.SerializeAsV2 = true;
    });

    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint($"{(!string.IsNullOrEmpty(pathForSwagger) ? pathForSwagger : string.Empty)}/swagger/v1/swagger.json", "Basket.Api V1");
    });
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();
