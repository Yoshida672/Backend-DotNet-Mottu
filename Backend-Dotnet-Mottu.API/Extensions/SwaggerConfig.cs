using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Backend_Dotnet_Mottu.Extensions
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AddSwaggerWithVersioning(this IServiceCollection services)
        {
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen();

            return services;
        }
    }

    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
        }

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var desc in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(desc.GroupName, new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Ping Mottu API",
                    Version = desc.ApiVersion.ToString(),
                    Description = "API de monitoramento de motos via UWB"
                });
            }
        }
    }
}
