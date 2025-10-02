using System.Reflection;
using CP2_BackEndMottu_DotNet.Api.Validators;
using CP2_BackEndMottu_DotNet.Application.DTOs.Request;
using CP2_BackEndMottu_DotNet.Application.DTOs.Response;
using CP2_BackEndMottu_DotNet.Domain.Entity;
using CP2_BackEndMottu_DotNet.Infrastructure.Context;
using CP2_BackEndMottu_DotNet.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using FluentValidation;
using FluentValidation.AspNetCore;
using CP2_BackEndMottu_DotNet.Application.UseCases.impl;
using CP2_BackEndMottu_DotNet.Infrastructure.Persistence.Repositories.impl;
using CP2_BackEndMottu_DotNet.Application.UseCases;

namespace CP2_BackEndMottu_DotNet
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

        
            builder.Services.AddDbContext<Context>(options =>
            {
                options.UseOracle(builder.Configuration.GetConnectionString("OracleMoto"))
                .UseLazyLoadingProxies();
            });



            builder.Services.AddScoped<IRepository<Moto>, Repository<Moto>>();
            builder.Services.AddScoped<IRepository<LocalizacaoUWB>, Repository<LocalizacaoUWB>>();
            builder.Services.AddScoped<IRepository<Condicao>, Repository<Condicao>>();

            builder.Services.AddScoped<
                IUseCase<Condicao, CreateCondicaoRequest, UpdateCondicaoRequest, CondicaoResponse>,
                CondicaoUseCase>();
            builder.Services.AddScoped<
                IUseCase<Moto, CreateMoto, UpdateMotoRequest, MotoResponse>,
                MotoUseCase>();
            builder.Services.AddScoped<ILocalizacaoUseCase, LocalizacaoUseCase>();


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
            builder.Services.AddControllers();
            builder.Services.AddValidatorsFromAssemblyContaining<CreateCondicaoRequestValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<CreateLocalizacaoRequestValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<CreateMotoRequestValidator>();


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
