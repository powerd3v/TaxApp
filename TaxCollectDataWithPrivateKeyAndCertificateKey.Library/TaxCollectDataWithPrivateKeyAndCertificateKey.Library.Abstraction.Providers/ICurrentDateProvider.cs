namespace TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Abstraction.Providers;

public interface ICurrentDateProvider
{
	long ToEpochMilli();

	string ToDateFormat();
}
