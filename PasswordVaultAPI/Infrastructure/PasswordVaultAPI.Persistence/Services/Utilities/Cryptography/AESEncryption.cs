﻿using PasswordVaultAPI.Application.Utilities.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PasswordVaultAPI.Persistence.Services.Utilities.Cryptography
{
    internal class AESEncryption : IAESEncryption
	{


        private const string _key = "alptugbebekauthh";

        public AESEncryption()
        {
            if (_key.Length != 16)
            {
                throw new ArgumentException("key must be longer than 16 characters");
            }

        }



        public string EncryptPassword(string plainText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(_key);
                aes.GenerateIV();

                using (var memoryStream = new MemoryStream())
                {
                    memoryStream.Write(aes.IV, 0, aes.IV.Length);

                    using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                    using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    using (var writer = new StreamWriter(cryptoStream))
                    {
                        writer.Write(plainText);
                    }
                    return Convert.ToBase64String(memoryStream.ToArray());
                }

            }
        }



		public string DecryptPassword(string cipherText)
		{
			byte[] fullCipher = Convert.FromBase64String(cipherText);
			using (Aes aes = Aes.Create())
			{
				aes.Key = Encoding.UTF8.GetBytes(_key);

				byte[] iv = new byte[16];
				byte[] cipher = new byte[fullCipher.Length - iv.Length];

				Array.Copy(fullCipher, 0, iv, 0, iv.Length);
				Array.Copy(fullCipher, iv.Length, cipher, 0, cipher.Length);

				using (var decryptor = aes.CreateDecryptor(aes.Key, iv))
				using (var memoryStream = new MemoryStream(cipher))
				using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
				using (var reader = new StreamReader(cryptoStream))
				{
					return reader.ReadToEnd();
				}
			}
		}


	}
}