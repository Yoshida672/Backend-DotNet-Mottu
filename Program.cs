using System.Reflection;
using CP2_BackEndMottu_DotNet.Domain.Entity;
using CP2_BackEndMottu_DotNet.Infrastructure.Context;
using CP2_BackEndMottu_DotNet.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace CP2_BackEndMottu_DotNet
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configuração do Oracle
            var configuration = builder.Configuration;
            var dbUser = Environment.GetEnvironmentVariable("DB_USER");
            var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");
            var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
            var dbPort = Environment.GetEnvironmentVariable("DB_PORT");
            var dbSid = Environment.GetEnvironmentVariable("DB_SID");

            var connectionString = $"Data Source={dbHost}:{dbPort}/{dbSid};User ID={dbUser};Password={dbPassword};";

            builder.Services.AddDbContext<Context>(options =>
       options.UseOracle(connectionString));


            // Controllers
            builder.Services.AddControllers();

            // Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = builder.Configuration["Swagger:Title"],
                    Description = "API do App da Mottu",
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                swagger.IncludeXmlComments(xmlPath);
            });

            // Repositórios genéricos (DIP)
            builder.Services.AddScoped<IRepository<Moto>, Repository<Moto>>();
            builder.Services.AddScoped<IRepository<LocalizacaoUWB>, Repository<LocalizacaoUWB>>();
            builder.Services.AddScoped<IRepository<Condicao>, Repository<Condicao>>();

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
