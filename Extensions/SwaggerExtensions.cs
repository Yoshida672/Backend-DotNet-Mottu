using Backend_Dotnet_Mottu.Application.Configs;
using Microsoft.OpenApi.Models;

namespace Backend_Dotnet_Mottu.Extensions;


public static class SwaggerExtensions
{
    public static IServiceCollection AddSwagger(this IServiceCollection services, SwaggerSettings settings)
    {
        return services.AddSwaggerGen(swagger =>
        {
            swagger.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = settings.Title,
                Version = "v1",
                Description = settings.Description,
                Contact = new OpenApiContact
                {
                    Name = settings.Contact.Name,
                    Email = settings.Contact.Email,
                }
            });

            swagger.SwaggerDoc("v2", new OpenApiInfo
            {
                Title = settings.Title,
                Version = "v2",
                Description = settings.Description,
                Contact = new OpenApiContact
                {
                    Name = settings.Contact.Name,
                    Email = settings.Contact.Email,
                }
            });

            swagger.EnableAnnotations();

        }
        );
    }
}