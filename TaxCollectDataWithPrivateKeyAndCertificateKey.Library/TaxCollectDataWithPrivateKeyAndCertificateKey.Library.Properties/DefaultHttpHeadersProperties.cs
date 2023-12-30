using System.Collections.Generic;
using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Abstraction.Properties;

namespace TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Properties;

public class DefaultHttpHeadersProperties : IHttpHeadersProperties
{
	private const string Authorization = "Authorization";

	public string AuthorizationHeaderName => "Authorization";

	public IDictionary<string, string> CustomHeaders => new Dictionary<string, string>();
}
