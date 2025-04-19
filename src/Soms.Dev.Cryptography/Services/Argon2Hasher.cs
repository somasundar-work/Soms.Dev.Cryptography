using System;
using System.Security.Cryptography;
using Konscious.Security.Cryptography;

namespace Soms.Dev.Cryptography;

public sealed class Argon2Hasher : IPasswordHasher
{
    private readonly int _Iterations;
    private readonly int _MemorySize;
    private readonly int _DegreeOfParallelism;
    private readonly int _HashSize;

    public Argon2Hasher(int iterations = 4, int memorySize = 65536, int degreeOfParallelism = 2, int hashSize = 32)
    {
        _Iterations = iterations;
        _MemorySize = memorySize;
        _DegreeOfParallelism = degreeOfParallelism;
        _HashSize = hashSize;
    }

    public (string Hash, string Salt) HashPassword(string password)
    {
        if (string.IsNullOrEmpty(password))
            throw new ArgumentNullException(nameof(password), "Password cannot be null or empty.");

        byte[] salt = GenerateSalt(16);
        using var argon2 = new Argon2id(System.Text.Encoding.UTF8.GetBytes(password))
        {
            Iterations = _Iterations,
            MemorySize = _MemorySize,
            DegreeOfParallelism = _DegreeOfParallelism,
            Salt = salt,
        };

        byte[] hash = argon2.GetBytes(_HashSize);
        return (Convert.ToBase64String(hash), Convert.ToBase64String(salt));
    }

    public bool VerifyPassword(string password, string hash, string salt)
    {
        if (string.IsNullOrEmpty(password))
            throw new ArgumentNullException(nameof(password), "Password cannot be null or empty.");

        if (string.IsNullOrEmpty(hash))
            throw new ArgumentNullException(nameof(hash), "Hash cannot be null or empty.");

        if (string.IsNullOrEmpty(salt))
            throw new ArgumentNullException(nameof(salt), "Salt cannot be null or empty.");

        byte[] saltBytes = Convert.FromBase64String(salt);
        byte[] hashBytes = Convert.FromBase64String(hash);

        using var argon2 = new Argon2id(System.Text.Encoding.UTF8.GetBytes(password))
        {
            Iterations = _Iterations,
            MemorySize = _MemorySize,
            DegreeOfParallelism = _DegreeOfParallelism,
            Salt = saltBytes,
        };

        byte[] computedHash = argon2.GetBytes(_HashSize);
        return CryptographicOperations.FixedTimeEquals(hashBytes, computedHash);
    }

    private byte[] GenerateSalt(int size)
    {
        using var rng = RandomNumberGenerator.Create();
        byte[] salt = new byte[size];
        rng.GetBytes(salt);
        return salt;
    }
}
