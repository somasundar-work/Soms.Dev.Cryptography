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
        if (!Enum.IsDefined(typeof(HashType), hashType))
        {
            throw new NotSupportedException($"Invalid HashType configured: {hashType}");
        }

        return CreatePasswordHasher(hashType);
    }

    private IPasswordHasher CreatePasswordHasher(HashType hashType)
    {
        return hashType switch
        {
            HashType.PBKDF2 => CreatePbkdf2Hasher(),
        };
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
