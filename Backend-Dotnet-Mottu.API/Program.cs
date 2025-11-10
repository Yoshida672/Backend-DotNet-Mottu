using Asp.Versioning;
using Backend_Dotnet_Mottu.Application.Configs;
using Backend_Dotnet_Mottu.Application.DTOs.Request;
using Backend_Dotnet_Mottu.Application.DTOs.Response;
using Backend_Dotnet_Mottu.Application.DTOs.Validators;
using Backend_Dotnet_Mottu.Application.UseCases;
using Backend_Dotnet_Mottu.Domain.Entities;
using Backend_Dotnet_Mottu.Extensions;
using Backend_Dotnet_Mottu.Extensions.Backend_Dotnet_Mottu.API.Extensions;
using Backend_Dotnet_Mottu.Infrasctructure;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Identity;

namespace Backend_Dotnet_Mottu;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Logging.ClearProviders();
        builder.Logging.AddConsole();


        var configs = builder.Configuration.Get<Settings>();
      
        builder.Services.AddControllers();
        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddFluentValidationClientsideAdapters();
        builder.Services.AddValidatorsFromAssemblyContaining<CreateCondicaoRequestValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<CreateLocalizacaoRequestValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<CreateMotoRequestValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<CreateTagUwbValidator>();
        builder.Services.AddEndpointsApiExplorer();

        builder.Services
           .AddAuthentication()
           .AddBearerToken(IdentityConstants.BearerScheme);

        builder.Services
            .AddAuthorizationBuilder();

        if (configs != null)
        {
            builder.Services.AddSingleton(configs.MongoDb);
            builder.Services.AddSingleton(configs.Jwt);
        }
        builder.Services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
        })
             .AddApiExplorer(options =>
             {
                 options.GroupNameFormat = "'v'VVV"; 
                 options.SubstituteApiVersionInUrl = true;
             });
        if (configs != null)
        {
            builder.Services.AddSwagger(configs.Swagger);
        }

        builder.Services.AddInfrastructure(builder.Configuration);


        builder.Services.AddScoped<
            IUseCase<Moto, CreateMoto, UpdateMotoRequest, MotoResponse>,
            MotoUseCase>();

        builder.Services.AddScoped<
            IUseCase<Condicao, CreateCondicaoRequest, UpdateCondicaoRequest, CondicaoResponse>,
            CondicaoUseCase>();

        builder.Services.AddScoped<
            IUseCase<TagUwb, CreateTagUwb, UpdateTagUwbRequest, TagUwbResponse>,
            TagUwbUseCase>();
        builder.Services.AddScoped<ILocalizacaoUseCase, LocalizacaoUseCase>();

        if (configs != null)
        {
            builder.Services.AddVerifyJwt(configs.Jwt);
        }


        var app = builder.Build();
        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(ui =>
            {
                ui.SwaggerEndpoint("/swagger/v1/swagger.json", "PM.API v1");
                ui.SwaggerEndpoint("/swagger/v2/swagger.json", "PM.API v2");
            }
            );
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        app.MapHealthChecks("/health-check", new HealthCheckOptions()
        {
            Predicate = _ => true,
            ResponseWriter = HealthCheckExtensions.WriteResponse
        });

        app.Run();
    }
}
