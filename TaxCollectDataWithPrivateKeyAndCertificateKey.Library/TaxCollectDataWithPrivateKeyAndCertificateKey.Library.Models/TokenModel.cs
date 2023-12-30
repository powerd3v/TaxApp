namespace TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Models;

public class TokenModel
{
	public string Nonce { get; }

	public string ClientId { get; }

	public TokenModel(string nonce, string clientId)
	{
		Nonce = nonce;
		ClientId = clientId;
	}
}
