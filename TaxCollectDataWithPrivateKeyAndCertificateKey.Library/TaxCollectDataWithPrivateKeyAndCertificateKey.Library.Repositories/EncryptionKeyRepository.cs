using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.Encoders;
using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Abstraction.Repositories;
using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Models;

namespace TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Repositories;

public class EncryptionKeyRepository : IEncryptionKeyRepository
{
	private readonly Random _random = new Random();

	private readonly Func<List<KeyModel>> _builder;

	private RSA _key;

	private string _keyId;

	private DateTime _expiredTime;

	public EncryptionKeyRepository(Func<List<KeyModel>> builder)
	{
		_builder = builder;
	}

	public RSA GetKey()
	{
		if (NeedRefresh())
		{
			Refresh();
		}
		return _key;
	}

	public string GetKeyId()
	{
		if (NeedRefresh())
		{
			Refresh();
		}
		return _keyId;
	}

	private void Refresh()
	{
		lock (this)
		{
			if (NeedRefresh())
			{
				List<KeyModel> list = _builder();
				KeyModel keyModel = list[_random.Next(list.Count)];
				_key = GetPublicKeyFromBase64(keyModel.Key);
				_keyId = keyModel.Id;
				_expiredTime = DateTime.Now.AddHours(1.0).ToLocalTime();
			}
		}
	}

	private bool NeedRefresh()
	{
		_ = _expiredTime;
		return DateTime.Now.ToLocalTime() > _expiredTime;
	}

	private RSA GetPublicKeyFromBase64(string key)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Expected O, but got Unknown
		byte[] array = Base64.Decode(key);
		AsymmetricKeyParameter val = PublicKeyFactory.CreateKey(array);
		RSAParameters parameters = DotNetUtilities.ToRSAParameters((RsaKeyParameters)val);
		RSA rSA = RSA.Create();
		rSA.ImportParameters(parameters);
		return rSA;
	}
}
