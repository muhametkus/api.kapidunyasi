using Api.KapiDunyasi.Infrastructure.Persistence;
using Api.KapiDunyasi.Infrastructure.Security;
using Api.KapiDunyasi.Infrastructure.Storage;
using Api.KapiDunyasi.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.KapiDunyasi.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("PostgreSql")
            )
        );
        services.AddScoped<IAppDbContext>(sp => sp.GetRequiredService<AppDbContext>());
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IFileStorageService, LocalFileStorageService>();
        services.AddScoped<AdminUserSeeder>();

        return services;
    }
}

