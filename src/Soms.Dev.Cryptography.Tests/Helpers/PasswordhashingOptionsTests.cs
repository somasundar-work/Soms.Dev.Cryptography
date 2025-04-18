using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Soms.Dev.Cryptography.Tests
{
    public class PasswordhashingOptionsTests
    {
        [Test]
        public void PasswordHashingOptions()
        {
            var options = new PasswordHashingOptions();
            Assert.That(options, Is.Not.Null);
            Assert.That(options, Is.InstanceOf<PasswordHashingOptions>());
            Assert.Multiple(() =>
            {
                Assert.That(options.HashType, Is.EqualTo(HashType.PBKDF2));
                Assert.That(options.PBKDF2, Is.InstanceOf<Pbkdf2Options>());
            });
            Assert.Multiple(() =>
            {
                Assert.That(options.PBKDF2.Iterations, Is.EqualTo(10000));
                Assert.That(options.PBKDF2.SaltSize, Is.EqualTo(16));
                Assert.That(options.PBKDF2.HashSize, Is.EqualTo(32));
                Assert.That(options.PBKDF2.HashAlgorithm, Is.Not.Null);
                Assert.That(options.PBKDF2.HashAlgorithm, Is.Not.Empty);
                Assert.That(options.PBKDF2.HashAlgorithm, Is.EqualTo("SHA256"));
            });
        }

        [Test]
        public void PasswordHashingOptions_Pbkdf2Options()
        {
            var options = new PasswordHashingOptions
            {
                PBKDF2 = new Pbkdf2Options
                {
                    Iterations = 20000,
                    SaltSize = 32,
                    HashSize = 64,
                    HashAlgorithm = "SHA512",
                },
            };
            Assert.That(options, Is.Not.Null);
            Assert.That(options, Is.InstanceOf<PasswordHashingOptions>());
            Assert.Multiple(() =>
            {
                Assert.That(options.HashType, Is.EqualTo(HashType.PBKDF2));
                Assert.That(options.PBKDF2, Is.InstanceOf<Pbkdf2Options>());
            });
            Assert.Multiple(() =>
            {
                Assert.That(options.PBKDF2.Iterations, Is.EqualTo(20000));
                Assert.That(options.PBKDF2.SaltSize, Is.EqualTo(32));
                Assert.That(options.PBKDF2.HashSize, Is.EqualTo(64));
                Assert.That(options.PBKDF2.HashAlgorithm, Is.Not.Null);
                Assert.That(options.PBKDF2.HashAlgorithm, Is.Not.Empty);
                Assert.That(options.PBKDF2.HashAlgorithm, Is.EqualTo("SHA512"));
                Assert.That(options.PBKDF2.HashAlgorithm, Is.Not.EqualTo("SHA256"));
            });
        }
    }
}
