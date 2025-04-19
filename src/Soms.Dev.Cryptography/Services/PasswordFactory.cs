using System;
using System.Security.Cryptography;
using Microsoft.Extensions.Options;

namespace Soms.Dev.Cryptography;

public sealed class PasswordFactory : IPasswordFactory
{
    private readonly PasswordHashingOptions _options;

    public PasswordFactory(IOptions<PasswordHashingOptions> options)
    {
        _options = options.Value;
    }

    public IPasswordHasher CreatePasswordHasher()
    {
        var hashType = _options.HashType;
        return CreatePasswordHasher(hashType);
    }

    private IPasswordHasher CreatePasswordHasher(HashType hashType)
    {
        return hashType switch
        {
            HashType.Argon2 => CreateArgon2Hasher(),
            HashType.PBKDF2 => CreatePbkdf2Hasher(),
            _ => throw new NotSupportedException($"Hash type '{hashType}' is not supported."),
        };
    }

    private IPasswordHasher CreateArgon2Hasher()
    {
        var defaultOptions = new Argon2Options();
        var argon2 = _options?.Argon2;

        return new Argon2Hasher(
            argon2?.Iterations ?? defaultOptions.Iterations,
            argon2?.MemorySize ?? defaultOptions.MemorySize,
            argon2?.DegreeOfParallelism ?? defaultOptions.DegreeOfParallelism,
            argon2?.HashSize ?? defaultOptions.HashSize
        );
    }

    private IPasswordHasher CreatePbkdf2Hasher()
    {
        var defaulfOptions = new Pbkdf2Options();
        var pbkdf2 = _options?.PBKDF2;

        HashAlgorithmName parsedHashAlgorithm = new(defaulfOptions.HashAlgorithm);
        if (
            !string.IsNullOrEmpty(pbkdf2?.HashAlgorithm)
            && !HashAlgorithmName.TryFromOid(pbkdf2.HashAlgorithm, out var tempParsedHashAlgorithm)
        )
        {
            parsedHashAlgorithm = tempParsedHashAlgorithm;
        }

        return new Pbkdf2Hasher(
            pbkdf2?.Iterations ?? defaulfOptions.Iterations,
            pbkdf2?.HashSize ?? defaulfOptions.HashSize,
            pbkdf2?.SaltSize ?? defaulfOptions.SaltSize,
            parsedHashAlgorithm
        );
    }
}
