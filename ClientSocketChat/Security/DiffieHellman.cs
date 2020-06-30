using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ClientSocketChat.Security
{
	public class DiffieHellman
	{
		private Aes aes = null;
		private ECDiffieHellmanCng diffieHellman = null;

		private readonly byte[] publicKey;
		public byte[] PublicKey
		{
			get
			{
				return this.publicKey;
			}
		}

		public byte[] IV
		{
			get
			{
				return this.aes.IV;
			}
		}

		public DiffieHellman()
		{
			this.aes = new AesCryptoServiceProvider();

			this.diffieHellman = new ECDiffieHellmanCng
			{
				KeyDerivationFunction = ECDiffieHellmanKeyDerivationFunction.Hash,
				HashAlgorithm = CngAlgorithm.Sha256
			};

			// This is the public key we will send to the other party
			this.publicKey = this.diffieHellman.PublicKey.ToByteArray();
		}

		//public byte Encrypt(byte[] publicKey, string message)
		//{
		//	byte[] encryptedMessage;
		//	var key = CngKey.Import(publicKey, CngKeyBlobFormat.EccPrivateBlob);
		//	var derivedKey = this.diffieHellman.DeriveKeyMaterial(key);
		//	this.aes.Key = derivedKey;

		//	using (var ms = new MemoryStream())
		//	{
		//		using (var encryptor = aes.CreateEncryptor())
		//		{
		//			using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
		//			{
		//				byte[] data = Encoding.UTF8.GetBytes(message);
		//				cs.Write(data, 0, data.Length);						
		//			}
		//		}
		//		encryptedMessage = ms.ToArray();
		//	}

		//}

	}
}
