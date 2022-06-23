using System;
using System.Security.Cryptography;
using VaultPdmTest.Contracts;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace VaultPdmTest.Engine
{
    public class CryptoServiceProvider : ICryptoServiceProvider
    {
        public string GetHash(string value) => GenerateHash(value);

        private string GenerateHash(string value)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: value,
                salt: new byte[16],
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));
        }
    }
}