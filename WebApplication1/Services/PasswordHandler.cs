using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AdvertApi.Services
{
	public class PasswordHandler : IPasswordHandler
	{
        public bool ConfirmPassword(string encryptedPass, string unEncryptedPass, string salt)
        {
            return EncryptPassword(unEncryptedPass, salt) == encryptedPass;
        }

        public string EncryptPassword(string unsaltedPass, string salt)
        {
            var saltedPass = KeyDerivation.Pbkdf2(
               password: unsaltedPass,
               salt: Encoding.UTF8.GetBytes(salt),
               prf: KeyDerivationPrf.HMACSHA512,
               iterationCount: 20000,
               numBytesRequested: 256 / 8);

            return Convert.ToBase64String(saltedPass);
        }

        public string GenerateRandomSalt()
        {
            var salt = "";
            byte[] randomBytes = new byte[128 / 8];
            using (var generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(randomBytes);
                salt = Convert.ToBase64String(randomBytes);
            }
            return salt;
        }



    }

}
