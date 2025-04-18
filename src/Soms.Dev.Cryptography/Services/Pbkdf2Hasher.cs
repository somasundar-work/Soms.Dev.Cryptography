using System;
using System.Security.Cryptography;

namespace Soms.Dev.Cryptography;

public class Pbkdf2Hasher(
    int HashSize = 32,
    int SaltSize = 16,
    int Iterations = 100_000,
    HashAlgorithmName HashAlgorithm = default
) : IPasswordHasher
{
    private readonly int _HashSize = HashSize;
    private readonly int _SaltSize = SaltSize;
    private readonly int _Iterations = Iterations;
    private readonly HashAlgorithmName _HashAlgorithm =
        HashAlgorithm == default ? HashAlgorithmName.SHA256 : HashAlgorithm;

    public (string Hash, string Salt) HashPassword(string password)
    {
        var (hash, salt) = HashPassword(System.Text.Encoding.UTF8.GetBytes(password));
        return (Convert.ToBase64String(hash), Convert.ToBase64String(salt));
    }

    private (byte[] Hash, byte[] Salt) HashPassword(byte[] password)
    {
        using var rng = RandomNumberGenerator.Create();
        byte[] salt = new byte[_SaltSize];
        rng.GetBytes(salt);
        using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, _Iterations, _HashAlgorithm);
        byte[] hash = pbkdf2.GetBytes(_HashSize);
        return (hash, salt);
    }

    public bool VerifyPassword(string password, string hash, string salt)
    {
        return VerifyPassword(
            System.Text.Encoding.UTF8.GetBytes(password),
            Convert.FromBase64String(hash),
            Convert.FromBase64String(salt)
        );
    }

    private bool VerifyPassword(byte[] password, byte[] hash, byte[] salt)
    {
        using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, _Iterations, _HashAlgorithm);
        byte[] hashToVerify = pbkdf2.GetBytes(_HashSize);
        return CryptographicOperations.FixedTimeEquals(hash, hashToVerify);
    }
}
