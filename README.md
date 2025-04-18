# Cryptography

Soms.Dev.Cryptography is a library designed to simplify the implementation of secure hashing, verification, encryption, and decryption in your applications. It provides robust and easy-to-use APIs to ensure data integrity and confidentiality.

## Core Features

-   Hashing and Verification
-   Encryption and Decryption

## Installation

### .NET Core Library

Install the core library via NuGet:

```bash
dotnet add package Soms.Dev.Cryptography
```

## Usage Examples

### Hashing a Password

```csharp
var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

IConfiguration configuration = builder.Build();

var passwordFactory = new PasswordFactory(configuration);
var passwordHasher = passwordFactory.CreateHasher();

var (hash, salt) = passwordHasher.HashPassword("my-secure-password");
Console.WriteLine($"Hash: {hash}, Salt: {salt}");

bool isValid = passwordHasher.VerifyPassword("my-secure-password", hash, salt);
Console.WriteLine($"Password is valid: {isValid}");
```

## Releases

-   **Soms.Dev.Cryptography**: [![NuGet Core](https://img.shields.io/nuget/v/Soms.Dev.Cryptography.svg)](https://www.nuget.org/packages/Soms.Dev.Cryptography)

### Build and Issues:

![codecov](https://codecov.io/gh/YOUR_USERNAME/YOUR_REPO/branch/main/graph/badge.svg) [![Build Status](https://github.com/somasundar-work/Soms.Dev.Cryptography/actions/workflows/devops.yml/badge.svg)](https://github.com/somasundar-work/Soms.Dev.Cryptography/actions/workflows/devops.yml) [![GitHub Packages](https://img.shields.io/github/v/release/somasundar-work/Soms.Dev.Cryptography?label=GitHub%20Packages)](https://github.com/somasundar-work/Soms.Dev.Cryptography/packages) [![Issues](https://img.shields.io/github/issues/somasundar-work/Soms.Dev.Cryptography)](https://github.com/somasundar-work/Soms.Dev.Cryptography/issues)

## Getting Started

1. Install the library using NuGet.
2. Configure the cryptography provider in your application.
3. Use the cryptography features like encryption and decryption, hashing and verification.

## Roadmap

### Ongoing

-   Implementing Core Cryptography logic.

### Backlog

-   Improving documentation with detailed examples and use cases.
-   Enhancing test coverage and adding more unit tests.
-   Adding Logger to Log Steps and errors

## Contributing

Contributions are welcome! Please open an issue or submit a pull request on [GitHub](https://github.com/somasundar-work/Soms.Dev.Cryptography).

## Export Compliance

This library may be subject to export control laws and regulations. Ensure compliance with applicable laws in your jurisdiction before using this library.

## License

This project is licensed under the MIT License. See the [LICENSE](https://github.com/somasundar-work/Soms.Dev.Cryptography/blob/main/LICENSE) file for more details.
