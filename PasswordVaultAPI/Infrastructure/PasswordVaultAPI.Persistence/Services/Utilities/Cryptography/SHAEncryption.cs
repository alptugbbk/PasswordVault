using PasswordVaultAPI.Application.Repositories;
using PasswordVaultAPI.Application.Utilities.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PasswordVaultAPI.Persistence.Services.Utilities.Cryptography
{
	public class SHAEncryption : ISHAEncryption
	{

		private readonly IUserRepository _userRepository;

		public SHAEncryption(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}


		public string Encrypt(string password, string userName)
		{

			var combined = password + userName;
			
			using(SHA512 sha = SHA512.Create())
			{
				byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(combined));

				return Convert.ToBase64String(bytes);

			}


		}

	}
}
