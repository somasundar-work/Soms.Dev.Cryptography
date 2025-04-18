using System;
using System.Security.Cryptography;
using Microsoft.Extensions.Options;

namespace Soms.Dev.Cryptography;

public sealed class PasswordFactory : IPasswordFactory
{
    private readonly PasswordHashingOptions? _options;

    public PasswordFactory(IOptions<PasswordHashingOptions> options)
    {
        _options = options.Value;
    }

    public IPasswordHasher CreatePasswordHasher()
    {
        var hashType = _options?.HashType ?? HashType.PBKDF2;
        if (!Enum.IsDefined(typeof(HashType), hashType))
        {
            throw new ArgumentException($"Invalid HashType configured: {hashType}");
        }

        return CreatePasswordHasher(hashType);
    }

    public IPasswordHasher CreatePasswordHasher(HashType hashType)
    {
        return hashType switch
        {
            HashType.PBKDF2 => CreatePbkdf2Hasher(),
            _ => throw new NotSupportedException($"Hash type '{hashType}' is not supported."),
        };
    }

    private IPasswordHasher CreatePbkdf2Hasher()
    {
        var defaulfOptions = new Pbkdf2Options();
        var pbkdf2 = _options?.PBKDF2;

        if (!Enum.TryParse<HashAlgorithmName>(pbkdf2?.HashAlgorithm, true, out var parsedHashAlgorithm))
        {
            parsedHashAlgorithm = HashAlgorithmName.SHA256;
        }

        return new Pbkdf2Hasher(
            pbkdf2?.Iterations ?? defaulfOptions.Iterations,
            pbkdf2?.HashSize ?? defaulfOptions.HashSize,
            pbkdf2?.SaltSize ?? defaulfOptions.SaltSize,
            parsedHashAlgorithm
        );
    }
}
