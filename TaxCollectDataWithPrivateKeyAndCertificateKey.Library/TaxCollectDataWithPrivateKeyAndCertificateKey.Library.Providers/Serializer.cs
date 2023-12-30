using System.Text;
using System.Text.Json;
using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Abstraction.Providers;
using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Configs;

namespace TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Providers;

public class Serializer : ISerializer
{
	public string Serialize(object dto)
	{
		return Encoding.UTF8.GetString(JsonSerializer.SerializeToUtf8Bytes(dto, JsonSerializerConfig.JsonSerializerOptions));
	}
}
