using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordVaultAPI.Application.Utilities.Cryptography
{
	public interface ISHAEncryption
	{

		public string Encrypt(string password, string userName);

	}
}
