using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Abstraction.Cryptography;

namespace TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Middlewares;

public class EncryptionMiddleware : Middleware
{
	private readonly IEncryptor _encryptor;

	public EncryptionMiddleware(IEncryptor encryptor)
	{
		_encryptor = encryptor;
	}

	public override string Handle(string text)
	{
		return HandleNext(_encryptor.Encrypt(text));
	}
}
