using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Abstraction.Cryptography;
using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Abstraction.Providers;
using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Cryptography;

namespace TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Factories;

public class SignatoryFactory
{
	private readonly ICurrentDateProvider _currentDateProvider;

	public SignatoryFactory(ICurrentDateProvider currentDateProvider)
	{
		_currentDateProvider = currentDateProvider;
	}

	public ISignatory Create(RSA privateKey, X509Certificate x509Certificate)
	{
		return new JwsSignatory(x509Certificate, privateKey, _currentDateProvider);
	}

	public ISignatory Create(string certificateAlias, string pkcs11LibraryPath, string pin)
	{
		return new JwsHardwareTokenSignatory(certificateAlias, pkcs11LibraryPath, pin);
	}
}
