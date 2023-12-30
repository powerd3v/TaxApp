using System.Collections.Generic;
using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Abstraction.Properties;

namespace TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Properties;

public class GsbHttpHeadersProperties : IHttpHeadersProperties
{
	private const string Token = "Token";

	public string AuthorizationHeaderName => "Token";

	public IDictionary<string, string> CustomHeaders { get; }

	public GsbHttpHeadersProperties(IDictionary<string, string> customHeaders)
	{
		CustomHeaders = customHeaders;
	}
}
