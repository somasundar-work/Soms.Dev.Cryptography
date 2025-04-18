using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;

namespace Soms.Dev.Cryptography.Tests;

[TestFixture]
public class PasswordFactoryServiceCollectionExtensionsTest
{
    [Test]
    public void AddPasswordHashing_WithValidConfiguration_AddsServicesSuccessfully()
    {
        // Arrange
        var services = new ServiceCollection();
        var configurationMock = new Mock<IConfiguration>();
        var configSectionMock = new Mock<IConfigurationSection>();
        configurationMock.Setup(c => c.GetSection("PasswordHashing")).Returns(configSectionMock.Object);

        services.AddSingleton<IConfiguration>(configurationMock.Object);

        // Act
        services.AddPasswordHashing();

        // Assert
        var serviceProvider = services.BuildServiceProvider();
        var passwordFactory = serviceProvider.GetService<IPasswordFactory>();
        Assert.NotNull(passwordFactory);
    }

    [Test]
    public void AddPasswordHashing_WithoutConfigurationSection_ThrowsInvalidOperationException()
    {
        // Arrange
        var services = new ServiceCollection();
        var configurationMock = new Mock<IConfiguration>();
        configurationMock.Setup(c => c.GetSection("PasswordHashing")).Returns((IConfigurationSection)null);

        services.AddSingleton<IConfiguration>(configurationMock.Object);

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() => services.AddPasswordHashing());
        Assert.AreEqual("Configuration section 'PasswordHashingOptions' not found.", exception.Message);
    }

    [Test]
    public void AddPasswordHashing_WithConfigureOptions_AddsServicesSuccessfully()
    {
        // Arrange
        var services = new ServiceCollection();
        Action<PasswordHashingOptions> configureOptions = options =>
        {
            options.HashType = HashType.PBKDF2;
            options.PBKDF2 = new();
        };

        // Act
        services.AddPasswordHashing(configureOptions);

        // Assert
        var serviceProvider = services.BuildServiceProvider();
        var passwordFactory = serviceProvider.GetService<IPasswordFactory>();
        Assert.NotNull(passwordFactory);
    }

    [Test]
    public void AddPasswordHashing_WithNullConfigureOptions_ThrowsArgumentNullException()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => services.AddPasswordHashing(null));
    }

    [Test]
    public void AddPasswordHashing_WithCustomOptions_RegistersCorrectOptions()
    {
        // Arrange
        var services = new ServiceCollection();
        Action<PasswordHashingOptions> configureOptions = options =>
        {
            options.HashType = HashType.PBKDF2;
            options.PBKDF2 = new Pbkdf2Options
            {
                Iterations = 2000,
                SaltSize = 16,
                HashSize = 32,
            };
        };

        // Act
        services.AddPasswordHashing(configureOptions);

        // Assert
        var serviceProvider = services.BuildServiceProvider();
        var options = serviceProvider.GetService<IOptions<PasswordHashingOptions>>()?.Value;
        Assert.NotNull(options);
        Assert.AreEqual(2000, options.PBKDF2.Iterations);
    }
}
