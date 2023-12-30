namespace TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Abstraction.Providers;

public interface ISerializer
{
	string Serialize(object dto);
}
