using System.IO;
using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Abstraction.Properties;

namespace TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Properties;

public class GsbUrlProperties : IUrlProperties
{
	private const string Version = "v2";

	private const string Api = "api";

	private readonly string _baseUrl;

	public GsbUrlProperties(string baseUrl)
	{
		_baseUrl = baseUrl;
	}

	public string GetUrl(string request)
	{
		return Path.Combine(_baseUrl, "api", "v2", request);
	}
}
