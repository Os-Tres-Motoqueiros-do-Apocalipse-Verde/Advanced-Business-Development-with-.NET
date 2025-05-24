using Microsoft.EntityFrameworkCore;
using OsTresMotoqueirosDoApocalipseVerde.Infraestructure;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Enum;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using System;

var builder = WebApplication.CreateBuilder(args);

// ENV VARIABLES
builder.Configuration.AddEnvironmentVariables();

var connectionString = builder.Configuration["ConnectionStrings:OracleMottu"];

// DATABASE
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseOracle(connectionString));

// AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// CONTROLLERS com enum serializado como string
builder.Services.AddControllers()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

// SWAGGER com enum como string
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Apresenta enums como strings no Swagger UI
    options.MapType<Plano>(() => new OpenApiSchema
    {
        Type = "string",
        Enum = Enum.GetNames(typeof(Plano))
                   .Select(name => new OpenApiString(name))
                   .ToList<IOpenApiAny>()
    });
});

var app = builder.Build();

// Configure o pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
