namespace TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Abstraction.Cryptography;

public interface IEncryptor
{
	string Encrypt(string text);
}
