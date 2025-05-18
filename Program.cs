
using System.Reflection;
using CP2_BackEndMottu_DotNet.Application.UseCases;
using CP2_BackEndMottu_DotNet.Application.Validators;
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

            var configuration = builder.Configuration;

            var rawConnection = configuration.GetConnectionString("OracleMoto");
            var user = Environment.GetEnvironmentVariable("DB_USER");
            var password = Environment.GetEnvironmentVariable("DB_PASSWORD");

            var connectionString = rawConnection
                .Replace("${DB_USER}", user)
                .Replace("${DB_PASSWORD}", password);


            builder.Services.AddDbContext<MotoContext>(options =>
                options.UseOracle(connectionString));

            builder.Services.AddControllers();

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

            builder.Services.AddScoped<IRepository<Moto>, Repository<Moto>>();
            builder.Services.AddScoped<IRepository<LocalizacaoUWB>, Repository<LocalizacaoUWB>>();
            builder.Services.AddScoped<CreateMotoRequestValidator>();
            builder.Services.AddScoped<CreateLocalizacaoRequestValidator>();
            builder.Services.AddScoped<MotoUseCase>();
            builder.Services.AddScoped<LocalizacaoUseCase>();

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