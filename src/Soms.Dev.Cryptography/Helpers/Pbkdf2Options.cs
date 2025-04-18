namespace Soms.Dev.Cryptography;

public class Pbkdf2Options
{
    public int Iterations { get; set; } = 10000; // Default iterations
    public int SaltSize { get; set; } = 16; // Default salt size
    public int HashSize { get; set; } = 32; // Default hash size
    public string HashAlgorithm { get; set; } = "SHA256"; // Default hash algorithm
}
