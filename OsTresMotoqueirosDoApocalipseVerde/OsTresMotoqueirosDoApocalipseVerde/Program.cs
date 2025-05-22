using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request;
using OsTresMotoqueirosDoApocalipseVerde.Application.UseCases;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entities;
using OsTresMotoqueirosDoApocalipseVerde.Infrastructure.Context;
using OsTresMotoqueirosDoApocalipseVerde.Infrastructure.Persistence;
using OsTresMotoqueirosDoApocalipseVerde.Application.Validador;
using System.Text.Json.Serialization;

namespace OsTresMotoqueirosDoApocalipseVerde
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = builder.Configuration["Swagger:Title"],
                    Description = "Api para o challeng, responsaveis são o grupo Os Tres Motoqueiros Do Apocalipse Verde",
                    Contact = new OpenApiContact
                    {
                        Email = "luizhneri12@gmail.com",
                        Name = "Luiz Henrique Neri"
                    }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                swagger.IncludeXmlComments(xmlPath);
            });

            builder.Services.AddDbContext<MottuContext>(options =>
            {
                options.UseOracle(builder.Configuration.GetConnectionString("OracleMottu"))
                       .UseLazyLoadingProxies();
            });

            builder.Services.AddScoped<IRepository<Motorista>, Repository<Motorista>>();
            builder.Services.AddScoped<IRepository<Dados>, Repository<Dados>>();

            builder.Services.AddScoped<CreateMotoristaDtoValidator>();
            builder.Services.AddScoped<CreateDadosDtoValidator>();
            

            builder.Services.AddScoped<MotoristaUseCase>();
            builder.Services.AddScoped<DadosUseCase>();


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
        }
    }
}
