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
            Assert.Throws<ArgumentException>(() => _factory.CreatePasswordHasher());
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
    }
}
