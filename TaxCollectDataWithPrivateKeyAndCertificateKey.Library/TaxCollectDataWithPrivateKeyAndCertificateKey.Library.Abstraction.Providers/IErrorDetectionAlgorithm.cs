namespace TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Abstraction.Providers;

public interface IErrorDetectionAlgorithm
{
	string GenerateCheckDigit(string num);

	bool ValidateCheckDigit(string num);
}
