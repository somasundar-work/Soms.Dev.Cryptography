using System;

namespace Soms.Dev.Cryptography;

public interface IPasswordFactory
{
    IPasswordHasher CreatePasswordHasher();
    IPasswordHasher CreatePasswordHasher(HashType hashType);
}
