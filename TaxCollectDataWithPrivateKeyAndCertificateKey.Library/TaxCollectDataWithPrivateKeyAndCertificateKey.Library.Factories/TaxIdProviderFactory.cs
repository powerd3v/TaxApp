using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Abstraction.Providers;
using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Algorithms;
using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Providers;

namespace TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Factories;

public class TaxIdProviderFactory
{
	public ITaxIdProvider Create()
	{
		return new TaxIdProvider(GetErrorDetectionAlgorithm());
	}

	private IErrorDetectionAlgorithm GetErrorDetectionAlgorithm()
	{
		return new VerhoeffAlgorithm();
	}
}
