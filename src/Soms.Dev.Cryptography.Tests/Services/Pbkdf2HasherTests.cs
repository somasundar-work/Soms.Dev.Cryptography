using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Soms.Dev.Cryptography.Tests
{
    public class Pbkdf2HasherTests
    {
        private IPasswordHasher _hasher;
        private const string _password = "password123";
        private const string _wrongpassword = "wrongpassword123";
        private const string _hash = "hash";
        private const string _salt = "salt";

        [SetUp]
        public void Setup()
        {
            _hasher = new Pbkdf2Hasher();
        }

        [Test]
        public void HashPassword_ValidInput_ReturnsHash()
        {
            var result = _hasher.HashPassword(_password);

            Assert.That(result, Is.TypeOf<ValueTuple<string, string>>());
            Assert.Multiple(() =>
            {
                Assert.That(result.Hash, Is.Not.Null);
                Assert.That(result.Salt, Is.Not.Null);
                Assert.That(result.Hash, Is.Not.Empty);
                Assert.That(result.Salt, Is.Not.Empty);
            });
            Assert.Multiple(() =>
            {
                Assert.That(result.Hash, Is.Not.EqualTo(_password));
                Assert.That(result.Salt, Is.Not.EqualTo(_password));
            });
        }

        [Test]
        public void HashPassword_EmptyPassword_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _hasher.HashPassword(string.Empty));
        }

        [Test]
        public void VerifyPassword_ValidInput_ReturnsTrue()
        {
            var response = _hasher.HashPassword(_password);
            var hash = response.Hash;
            var salt = response.Salt;

            var result = _hasher.VerifyPassword(_password, hash, salt);

            Assert.That(result, Is.True);
        }

        [Test]
        public void VerifyPassword_InvalidPassword_ReturnsFalse()
        {
            var response = _hasher.HashPassword(_password);
            var hash = response.Hash;
            var salt = response.Salt;
            var result = _hasher.VerifyPassword(_wrongpassword, hash, salt);
            Assert.That(result, Is.False);
        }

        [Test]
        public void VerifyPassword_EmptyPassword_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _hasher.VerifyPassword(string.Empty, _hash, _salt));
        }

        [Test]
        public void VerifyPassword_EmptyHash_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _hasher.VerifyPassword(_password, string.Empty, _salt));
        }

        [Test]
        public void VerifyPassword_EmptySalt_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _hasher.VerifyPassword(_password, _hash, string.Empty));
        }

        [Test]
        public void VerifyPassword_invalidhash_returnFalse()
        {
            var response = _hasher.HashPassword(_password);
            var salt = response.Salt;

            var result = _hasher.VerifyPassword(_wrongpassword, _hash, salt);

            Assert.That(result, Is.False);
        }

        [Test]
        public void VerifyPassword_invalidsalt_returnFalse()
        {
            var response = _hasher.HashPassword(_password);
            var hash = response.Hash;

            var result = _hasher.VerifyPassword(_wrongpassword, hash, _salt);

            Assert.That(result, Is.False);
        }
    }
}
