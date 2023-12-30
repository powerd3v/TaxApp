using System;
using System.Collections.Generic;
using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Abstraction.Clients;
using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Abstraction.Cryptography;
using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Cryptography;
using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Models;
using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Repositories;

namespace TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Factories;

public class EncryptorFactory
{
	public IEncryptor Create(ITaxPublicApi publicApi)
	{
		ITaxPublicApi publicApi2 = publicApi;
		return Create(() => publicApi2.GetServerInformation().PublicKeys);
	}

	public IEncryptor Create(Func<List<KeyModel>> getPublicKeys)
	{
		EncryptionKeyRepository repository = new EncryptionKeyRepository(getPublicKeys);
		return new JweEncryptor(repository);
	}
}
