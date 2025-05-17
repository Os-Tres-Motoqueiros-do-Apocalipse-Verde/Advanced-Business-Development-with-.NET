
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request;
using OsTresMotoqueirosDoApocalipseVerde.Application.UseCases;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entity;
using OsTresMotoqueirosDoApocalipseVerde.Infrastructure.Context;
using OsTresMotoqueirosDoApocalipseVerde.Infrastructure.Persistence;

namespace OsTresMotoqueirosDoApocalipseVerde
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
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

            builder.Services.AddScoped<CreatedDadosRequest>();
            builder.Services.AddScoped<CreatedMotoristaRequest>();
            builder.Services.AddScoped<CreateEnderecoRequest>();
            builder.Services.AddScoped<CreateFilialRequest>();
            builder.Services.AddScoped<CreateFuncionarioRequest>();
            builder.Services.AddScoped<CreateModeloRequest>();
            builder.Services.AddScoped<CreateMotoRequest>();
            builder.Services.AddScoped<CreatePatioRequest>();
            builder.Services.AddScoped<CreateSetorRequest>();
            builder.Services.AddScoped<CreateSituacaoRequest>();
            
            builder.Services.AddScoped<FilialUseCase>();
            builder.Services.AddScoped<FuncionarioUseCase>();
            builder.Services.AddScoped<MotoristaUseCase>();
            builder.Services.AddScoped<MotoUseCase>();
            builder.Services.AddScoped<PatioUseCase>();

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