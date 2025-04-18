using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Soms.Dev.Cryptography;

public static class PasswordFactoryServiceCollectionExtensions
{
    public static IServiceCollection AddPasswordHashing(this IServiceCollection services)
    {
        var configSection = services
            .BuildServiceProvider()
            .GetRequiredService<IConfiguration>()
            .GetSection("PasswordHashing");
        if (configSection == null)
        {
            throw new InvalidOperationException($"Configuration section '{nameof(PasswordHashingOptions)}' not found.");
        }
        services.AddPasswordHashing(configSection.Bind);
        return services;
    }

    public static IServiceCollection AddPasswordHashing(
        this IServiceCollection services,
        Action<PasswordHashingOptions> configureOptions
    )
    {
        services.Configure(configureOptions);
        services.AddScoped<IPasswordFactory, PasswordFactory>();
        return services;
    }
}
