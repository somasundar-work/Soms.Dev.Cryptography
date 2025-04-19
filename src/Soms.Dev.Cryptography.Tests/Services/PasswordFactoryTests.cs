using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Soms.Dev.Cryptography.Tests.Services
{
    public class PasswordFactoryTests
    {
        [Test]
        public void CreatePasswordHasher_ValidHashType_ReturnsPbkdf2Hasher()
        {
            PasswordHashingOptions _options = new();
            var _factory = new PasswordFactory(Microsoft.Extensions.Options.Options.Create(_options));
            var hasher = _factory.CreatePasswordHasher();
            Assert.That(hasher, Is.InstanceOf<Pbkdf2Hasher>());
        }

        [Test]
        public void CreatePasswordHasher_InvalidHashType_ThrowsArgumentException()
        {
            PasswordHashingOptions _options = new() { HashType = (HashType)999 };
            var _factory = new PasswordFactory(Microsoft.Extensions.Options.Options.Create(_options));
            Assert.Throws<NotSupportedException>(() => _factory.CreatePasswordHasher());
        }

        [Test]
        public void CreatePasswordHasher_InvalidHashAlgorithm_ReturnsSHA256()
        {
            PasswordHashingOptions _options = new() { HashType = HashType.PBKDF2 };
            _options.PBKDF2.HashAlgorithm = "InvalidAlgorithm";
            var _factory = new PasswordFactory(Microsoft.Extensions.Options.Options.Create(_options));

            var hasher = _factory.CreatePasswordHasher();
            Assert.That(hasher, Is.InstanceOf<Pbkdf2Hasher>());
        }

        [Test]
        public void CreatePasswordHasher_ValidHashType_ReturnsArgon2Hasher()
        {
            PasswordHashingOptions _options = new() { HashType = HashType.Argon2 };
            var _factory = new PasswordFactory(Microsoft.Extensions.Options.Options.Create(_options));
            var hasher = _factory.CreatePasswordHasher();
            Assert.That(hasher, Is.InstanceOf<Argon2Hasher>());
        }

        [Test]
        public void HashPassword_ValidPassword_ReturnsHashAndSalt()
        {
            PasswordHashingOptions _options = new()
            {
                HashType = HashType.Argon2,
                Argon2 = new Argon2Options
                {
                    Iterations = 4,
                    MemorySize = 65536,
                    DegreeOfParallelism = 2,
                    HashSize = 32,
                },
            };
            var _factory = new PasswordFactory(Microsoft.Extensions.Options.Options.Create(_options));
            var hasher = _factory.CreatePasswordHasher();

            string password = "TestPassword";
            var (hash, salt) = hasher.HashPassword(password);

            Assert.That(hash, Is.Not.Null.Or.Empty);
            Assert.That(salt, Is.Not.Null.Or.Empty);
        }

        [Test]
        public void HashPassword_EmptyPassword_ReturnsArgumentNullException()
        {
            PasswordHashingOptions _options = new()
            {
                HashType = HashType.Argon2,
                Argon2 = new Argon2Options
                {
                    Iterations = 4,
                    MemorySize = 65536,
                    DegreeOfParallelism = 2,
                    HashSize = 32,
                },
            };
            var _factory = new PasswordFactory(Microsoft.Extensions.Options.Options.Create(_options));
            var hasher = _factory.CreatePasswordHasher();
            string password = "";

            Assert.Throws<ArgumentNullException>(() => hasher.HashPassword(password));
        }

        [Test]
        public void VerifyPasswordPassword_EmptyPassword_ReturnsArgumentNullException()
        {
            PasswordHashingOptions _options = new()
            {
                HashType = HashType.Argon2,
                Argon2 = new Argon2Options
                {
                    Iterations = 4,
                    MemorySize = 65536,
                    DegreeOfParallelism = 2,
                    HashSize = 32,
                },
            };
            var _factory = new PasswordFactory(Microsoft.Extensions.Options.Options.Create(_options));
            var hasher = _factory.CreatePasswordHasher();
            string password = "";

            Assert.Throws<ArgumentNullException>(() => hasher.VerifyPassword(password, "hash", "salt"));
        }

        [Test]
        public void VerifyPasswordPassword_Emptyhash_ReturnsArgumentNullException()
        {
            PasswordHashingOptions _options = new()
            {
                HashType = HashType.Argon2,
                Argon2 = new Argon2Options
                {
                    Iterations = 4,
                    MemorySize = 65536,
                    DegreeOfParallelism = 2,
                    HashSize = 32,
                },
            };
            var _factory = new PasswordFactory(Microsoft.Extensions.Options.Options.Create(_options));
            var hasher = _factory.CreatePasswordHasher();
            string password = "12312312";

            Assert.Throws<ArgumentNullException>(() => hasher.VerifyPassword(password, "", "salt"));
        }

        [Test]
        public void VerifyPasswordPassword_EmptySalt_ReturnsArgumentNullException()
        {
            PasswordHashingOptions _options = new()
            {
                HashType = HashType.Argon2,
                Argon2 = new Argon2Options
                {
                    Iterations = 4,
                    MemorySize = 65536,
                    DegreeOfParallelism = 2,
                    HashSize = 32,
                },
            };
            var _factory = new PasswordFactory(Microsoft.Extensions.Options.Options.Create(_options));
            var hasher = _factory.CreatePasswordHasher();
            string password = "21341234";

            Assert.Throws<ArgumentNullException>(() => hasher.VerifyPassword(password, "hash", ""));
        }

        [Test]
        public void VerifyPassword_ValidPassword_ReturnsTrue()
        {
            PasswordHashingOptions _options = new()
            {
                HashType = HashType.Argon2,
                Argon2 = new Argon2Options
                {
                    Iterations = 4,
                    MemorySize = 65536,
                    DegreeOfParallelism = 2,
                    HashSize = 32,
                },
            };
            var _factory = new PasswordFactory(Microsoft.Extensions.Options.Options.Create(_options));
            var hasher = _factory.CreatePasswordHasher();
            string password = "12341234";
            var (hash, salt) = hasher.HashPassword(password);
            var result = hasher.VerifyPassword(password, hash, salt);
            Assert.That(result, Is.True);
        }

        [Test]
        public void VerifyPassword_InValidPassword_ReturnsFalse()
        {
            PasswordHashingOptions _options = new()
            {
                HashType = HashType.Argon2,
                Argon2 = new Argon2Options
                {
                    Iterations = 4,
                    MemorySize = 65536,
                    DegreeOfParallelism = 2,
                    HashSize = 32,
                },
            };
            var _factory = new PasswordFactory(Microsoft.Extensions.Options.Options.Create(_options));
            var hasher = _factory.CreatePasswordHasher();
            string password = "12341234";
            var (hash, salt) = hasher.HashPassword(password);
            var result = hasher.VerifyPassword("43214321", hash, salt);
            Assert.That(result, Is.False);
        }
    }
}
