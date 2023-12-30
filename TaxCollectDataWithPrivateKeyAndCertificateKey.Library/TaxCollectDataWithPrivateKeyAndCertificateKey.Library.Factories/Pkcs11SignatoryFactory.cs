using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Abstraction.Cryptography;
using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Providers;

namespace TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Factories;

public class Pkcs11SignatoryFactory
{
	private readonly SignatoryFactory _signatoryFactory;

	public Pkcs11SignatoryFactory()
	{
		CurrentDateProvider currentDateProvider = new CurrentDateProvider();
		_signatoryFactory = new SignatoryFactory(currentDateProvider);
	}

	public ISignatory Create(string certificateAlias, string pkcs11LibraryPath, string pin)
	{
		return _signatoryFactory.Create(certificateAlias, pkcs11LibraryPath, pin);
	}
}
