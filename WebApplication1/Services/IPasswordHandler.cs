using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertApi.Services
{
	public interface IPasswordHandler
	{
		public bool ConfirmPassword(string encryptedPass, string unEncryptedPass, string salt);
		public string EncryptPassword(string unsaltedPass, string salt);
		public string GenerateRandomSalt();
	}
}
