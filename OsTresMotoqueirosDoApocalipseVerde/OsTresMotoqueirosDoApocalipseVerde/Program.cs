using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using OsTresMotoqueirosDoApocalipseVerde;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entity;
using OsTresMotoqueirosDoApocalipseVerde.Infraestructure.Context;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Configura��o do banco Oracle
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleMottu")));

// Adiciona controladores e configura JsonOptions
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();

// Registro de depend�ncias
builder.Services.AddScoped<IRepository<Dados>, Repository<Dados>>();
builder.Services.AddScoped<IRepository<Moto>, Repository<Moto>>();
builder.Services.AddScoped<CreateUsuarioRequestValidator>();
builder.Services.AddScoped<CreateDesastreRequestValidator>();
builder.Services.AddScoped<UsuarioUseCase>();
builder.Services.AddScoped<DesastreUseCase>();

// Configura��o para remover a valida��o autom�tica do ModelState
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

// Swagger/OpenAPI
builder.Services.AddSwaggerGen(swagger =>
{
    swagger.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = builder.Configuration["Swagger:Title"] ?? "API de Desastres Ambientais",
        Description = "Um informativo de desastres",
        Contact = new OpenApiContact
        {
            Email = "luizhneri12@gmail.com",
            Name = "Luiz Henrique Neri Reimberg"
        }
    });

    // Inclus�o do arquivo XML para documenta��o Swagger
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    swagger.IncludeXmlComments(xmlPath);

    // Mostrar enums como string no Swagger
    var enumTypes = Assembly
        .GetExecutingAssembly()
        .GetTypes()
        .Where(t => t.IsEnum && t.Namespace != null && t.Namespace.Contains("GB1.Domain.Enums"));

    foreach (var enumType in enumTypes)
    {
        swagger.MapType(enumType, () => new OpenApiSchema
        {
            Type = "string",
            Enum = Enum.GetNames(enumType)
                       .Select(name => new OpenApiString(name))
                       .Cast<IOpenApiAny>()
                       .ToList()
        });
    }
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
