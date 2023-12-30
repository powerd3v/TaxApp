using System.Security.Cryptography;

namespace TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Abstraction.Repositories;

public interface IEncryptionKeyRepository
{
	RSA GetKey();

	string GetKeyId();
}
