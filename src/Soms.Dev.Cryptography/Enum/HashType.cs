namespace Soms.Dev.Cryptography;

public enum HashType
{
    PBKDF2, // Password-Based Key Derivation Function 2 (default)
    Argon2, // Memory-hard password hashing function
    // // Future Implementation Not Supported
    // BCrypt, // Blowfish-based password hashing function
}
