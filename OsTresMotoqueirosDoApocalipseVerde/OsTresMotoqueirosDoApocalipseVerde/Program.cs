using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using OsTresMotoqueirosDoApocalipseVerde;
using OsTresMotoqueirosDoApocalipseVerde.Application.Swagger;
using OsTresMotoqueirosDoApocalipseVerde.Application.UseCase;
using OsTresMotoqueirosDoApocalipseVerde.Application.Validators;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entity;
using OsTresMotoqueirosDoApocalipseVerde.Infraestructure.Context;
using OsTresMotoqueirosDoApocalipseVerde.Services;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// JWT
var jwtKey = builder.Configuration["Jwt:Key"];
var jwtIssuer = builder.Configuration["Jwt:Issuer"];
var jwtAudience = builder.Configuration["Jwt:Audience"];

// Configuração do banco Oracle
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
builder.Services.AddScoped<IRepository<Motorista>, Repository<Motorista>>();
builder.Services.AddScoped<IRepository<Funcionario>, Repository<Funcionario>>();
builder.Services.AddScoped<IRepository<Endereco>, Repository<Endereco>>();
builder.Services.AddScoped<IRepository<Filial>, Repository<Filial>>();
builder.Services.AddScoped<IRepository<Modelo>, Repository<Modelo>>();
builder.Services.AddScoped<IRepository<Moto>, Repository<Moto>>();
builder.Services.AddScoped<IRepository<Patio>, Repository<Patio>>();
builder.Services.AddScoped<IRepository<Setor>, Repository<Setor>>();
builder.Services.AddScoped<IRepository<Situacao>, Repository<Situacao>>();
builder.Services.AddScoped<IRepository<Usuarios>, Repository<Usuarios>>();

// Validators
builder.Services.AddScoped<CreateDadosRequestValidator>();
builder.Services.AddScoped<CreateFuncionarioRequestValidator>();
builder.Services.AddScoped<CreateMotoristaRequestValidator>();
builder.Services.AddScoped<CreateFilialRequestValidator>();
builder.Services.AddScoped<CreateEnderecoRequestValidator>();
builder.Services.AddScoped<CreateUsuariosRequestValidator>();
builder.Services.AddScoped<CreateMotoRequestValidator>();
builder.Services.AddScoped<CreateModeloRequestValidator>();
builder.Services.AddScoped<CreatePatioRequestValidator>();
builder.Services.AddScoped<CreateSetorRequestValidator>();
builder.Services.AddScoped<CreateSituacaoRequestValidator>();

// UseCases
builder.Services.AddScoped<DadosUseCase>();
builder.Services.AddScoped<FuncionarioUseCase>();
builder.Services.AddScoped<MotoristaUseCase>();
builder.Services.AddScoped<EnderecoUseCase>();
builder.Services.AddScoped<FilialUseCase>();
builder.Services.AddScoped<ModeloUseCase>();
builder.Services.AddScoped<MotoUseCase>();
builder.Services.AddScoped<PatioUseCase>();
builder.Services.AddScoped<SetorUseCase>();
builder.Services.AddScoped<SituacaoUseCase>();
builder.Services.AddScoped<UsuariosUseCase>();
builder.Services.AddScoped<MotoPredictionUseCase>();

// Configuração do comportamento API
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

// Versionamento
builder.Services.AddApiVersioning(o =>
{
    o.AssumeDefaultVersionWhenUnspecified = true;
    o.DefaultApiVersion = new ApiVersion(1, 0);
});

builder.Services.AddVersionedApiExplorer(setup =>
{
    setup.GroupNameFormat = "'v'VVV";
    setup.SubstituteApiVersionInUrl = true;
});

// Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.OperationFilter<SwaggerDefaultValues>();

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira o token JWT desta forma: Bearer {seu_token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// HealthChecks
builder.Services.AddHealthChecks()
    .AddOracle(builder.Configuration.GetConnectionString("OracleMottu"), name: "oracle");

builder.Services.AddHealthChecksUI(opt =>
{
    opt.SetEvaluationTimeInSeconds(10);
    opt.MaximumHistoryEntriesPerEndpoint(60);
    opt.AddHealthCheckEndpoint("API Health", "/health");
}).AddInMemoryStorage();

builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerGenOptions>();
builder.Services.AddSingleton<TokenService>();

// JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
            ValidateIssuer = true,
            ValidIssuer = jwtIssuer,
            ValidateAudience = true,
            ValidAudience = jwtAudience,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();
var versionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        foreach (var description in versionDescriptionProvider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                $"Web API - {description.GroupName.ToUpper()}");
        }
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.MapHealthChecksUI(options =>
{
    options.UIPath = "/health-ui";
});

app.Run();

// Para o WebApplicationFactory nos testes
public partial class Program { }
