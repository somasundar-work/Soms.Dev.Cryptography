namespace Soms.Dev.Cryptography;

public class PasswordHashingOptions
{
    public HashType HashType { get; set; } = HashType.PBKDF2; // Default to PBKDF2 if not specified
    public Pbkdf2Options PBKDF2 { get; set; } = new Pbkdf2Options(); // Defaults for PBKDF2 options
}
