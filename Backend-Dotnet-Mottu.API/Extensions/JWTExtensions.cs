namespace Backend_Dotnet_Mottu.Extensions
{
    using System.Text;
    using System.Text.Json;
    using global::Backend_Dotnet_Mottu.Application.Configs;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Http;
    using Microsoft.IdentityModel.Tokens;

    namespace Backend_Dotnet_Mottu.API.Extensions
    {
        public static class JwtExtensions
        {
            public static void AddVerifyJwt(this IServiceCollection services, JwtSettings jwtSettings)
            {
                var key = Encoding.ASCII.GetBytes(jwtSettings.Secret);

                services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                    .AddJwtBearer(x =>
                    {
                        x.RequireHttpsMetadata = false;
                        x.SaveToken = true;
                        x.MapInboundClaims = false;
                        x.Events = new JwtBearerEvents
                        {
                            OnAuthenticationFailed = context =>
                            {
                                Console.WriteLine($"Authentication failed: {context.Exception.Message}");
                                context.NoResult();
                                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                                context.Response.ContentType = "application/json";

                                var isExpired = context.Exception is SecurityTokenExpiredException;
                                var payload = new
                                {
                                    success = false,
                                    error = isExpired ? "token_expired" : "invalid_token",
                                    message = isExpired ? "Token expirado." : "Token inválido."
                                };

                                return context.Response.WriteAsync(JsonSerializer.Serialize(payload));
                            },
                            OnTokenValidated = context =>
                            {
                                Console.WriteLine("Token validated successfully");
                                return Task.CompletedTask;
                            },
                            OnChallenge = context =>
                            {
                                context.HandleResponse();
                                Console.WriteLine($"Challenge: {context.Error}");

                                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                                context.Response.ContentType = "application/json";

                                var payload = new
                                {
                                    success = false,
                                    error = string.IsNullOrWhiteSpace(context.Error) ? "unauthorized" : context.Error,
                                    message = string.IsNullOrWhiteSpace(context.ErrorDescription)
                                        ? "Não autorizado. Token ausente ou inválido."
                                        : context.ErrorDescription
                                };

                                return context.Response.WriteAsync(JsonSerializer.Serialize(payload));
                            }
                        };
                        x.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(key),
                            ValidateIssuer = false,
                            ValidateAudience = false,
                            ClockSkew = TimeSpan.FromMinutes(5)
                        };
                    });

                services.AddAuthorizationBuilder()
                    .AddPolicy("AdminOnly", policy =>
                    {
                        policy.RequireAuthenticatedUser();
                        policy.RequireClaim("admin", "True");
                    });
            }
        }
    }
}
