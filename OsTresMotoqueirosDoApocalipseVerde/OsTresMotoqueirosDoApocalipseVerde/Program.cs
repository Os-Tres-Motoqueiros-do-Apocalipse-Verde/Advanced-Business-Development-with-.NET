using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using NuGet.Protocol.Core.Types;
using OsTresMotoqueirosDoApocalipseVerde;
using OsTresMotoqueirosDoApocalipseVerde.Application.UseCase;
using OsTresMotoqueirosDoApocalipseVerde.Application.Validators;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entities;
using OsTresMotoqueirosDoApocalipseVerde.Infraestructure.Context;
using System.Globalization;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleMottu")));

// Adiciona controladores e configura JsonOptions
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();

// Registro de dependências
builder.Services.AddScoped<IRepository<Dados>, Repository<Dados>>();
builder.Services.AddScoped<IRepository<Endereco>, Repository<Endereco>>();
builder.Services.AddScoped<IRepository<Filial>, Repository<Filial>>();
builder.Services.AddScoped<IRepository<Funcionario>, Repository<Funcionario>>();
builder.Services.AddScoped<IRepository<Modelo>, Repository<Modelo>>();
builder.Services.AddScoped<IRepository<Moto>, Repository<Moto>>();
builder.Services.AddScoped<IRepository<Motorista>, Repository<Motorista>>();
builder.Services.AddScoped<IRepository<Patio>, Repository<Patio>>();
builder.Services.AddScoped<IRepository<Setor>, Repository<Setor>>();
builder.Services.AddScoped<IRepository<Situacao>, Repository<Situacao>>();
builder.Services.AddScoped<CreateDadosDtoValidator>();
builder.Services.AddScoped<CreateEnderecoDtoValidator>();
builder.Services.AddScoped<CreateFilialDtoValidator>();
builder.Services.AddScoped<CreateFuncionarioDtoValidator>();
builder.Services.AddScoped<CreateModeloDtoValidator>();
builder.Services.AddScoped<CreateMotoDtoValidator>();
builder.Services.AddScoped<CreateMotoristaDtoValidator>();
builder.Services.AddScoped<CreatePatioDtoValidator>();
builder.Services.AddScoped<CreateSetorDtoValidator>();
builder.Services.AddScoped<CreateSituacaoDtoValidator>();
builder.Services.AddScoped<FilialUseCase>();
builder.Services.AddScoped<FuncionarioUseCase>();
builder.Services.AddScoped<MotoristaUseCase>();
builder.Services.AddScoped<MotoristaUseCase>();
builder.Services.AddScoped<PatioUseCase>();
builder.Services.AddScoped<SituacaoUseCase>();

// Configuração para remover a validação automática do ModelState
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

// Swagger/OpenAPI
builder.Services.AddSwaggerGen(swagger =>
{
    swagger.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = builder.Configuration["Swagger:Title"] ?? "API Os Tres Motoqueiros do Apocalipse Verde",
        Description = "Organização de motos no Patios da Mottu",
        Contact = new OpenApiContact
        {
            Email = "luizhneri12@gmail.com",
            Name = "Luiz Henrique Neri Reimberg"
        }
    });

    // Inclusão do arquivo XML para documentação Swagger
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    swagger.IncludeXmlComments(xmlPath);

    // Mostrar enums como string no Swagger
    var enumTypes = Assembly
        .GetExecutingAssembly()
        .GetTypes()
        .Where(t => t.IsEnum && t.Namespace != null && t.Namespace.Contains("OsTresMotoqueirosDoApocalipseVerde.Domain.Enums"));

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
