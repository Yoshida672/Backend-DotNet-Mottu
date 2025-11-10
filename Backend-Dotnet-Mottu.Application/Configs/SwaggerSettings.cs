using Microsoft.OpenApi.Models;

namespace Backend_Dotnet_Mottu.Application.Configs;

public class SwaggerSettings
{
    public string Title { get; init; }

    public string Description { get; init; }

    public OpenApiContact Contact { get; init; }
}