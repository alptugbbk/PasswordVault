using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordVaultAPI.Application.Utilities.Cryptography
{
	public interface IAESEncryption
	{
		string EncryptPassword(string plainText);

		string DecryptPassword(string cipherText);
	}
}
