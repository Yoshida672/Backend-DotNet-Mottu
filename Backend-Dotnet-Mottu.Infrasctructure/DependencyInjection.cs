using Backend_Dotnet_Mottu.Application;
using Backend_Dotnet_Mottu.Infrasctructure.Persistence.Context;
using Backend_Dotnet_Mottu.Infrasctructure.Persistence.Repositories;
using Backend_Dotnet_Mottu.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Backend_Dotnet_Mottu.Infrastructure;

namespace Backend_Dotnet_Mottu.Infrasctructure
{
    public static class DependencyInjection
    {
        private static IServiceCollection AddContextSqlServer(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("SqlServerContext");
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            return services;
        }
        private static IServiceCollection AddMongoDB(this IServiceCollection services)
        {
            return services.AddScoped<MongoContext>();
        }

        private static IServiceCollection AddRepositories(IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }

        private static IServiceCollection AddHealthCheck(IServiceCollection services, IConfiguration configuration)
        {
            
            var sqlConnection = configuration.GetConnectionString("SqlServerContext");
            services.AddSingleton(sp =>
        new MongoClient("mongodb://admin:admin123@localhost:27017/SendNotificationDB?authSource=admin"));

            services.AddHealthChecks()
                .AddSqlServer(
                    connectionString: sqlConnection,
                    name: "SQL Server 2TDS")
                .AddUrlGroup(new Uri("https://google.com"), "Google");

            return services;
        }

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddContextSqlServer(services, configuration);
            AddMongoDB(services);

            AddRepositories(services);
            AddHealthCheck(services, configuration);

            return services;
        }
    }
}