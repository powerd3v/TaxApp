using System.Collections.Generic;

namespace TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Abstraction.Properties;

public interface IHttpHeadersProperties
{
	string AuthorizationHeaderName { get; }

	IDictionary<string, string> CustomHeaders { get; }
}
