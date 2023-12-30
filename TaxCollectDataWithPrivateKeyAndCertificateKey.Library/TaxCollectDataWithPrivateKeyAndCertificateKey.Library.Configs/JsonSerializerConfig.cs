using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Configs;

public static class JsonSerializerConfig
{
	public static readonly JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions
	{
		PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
		Converters = { (JsonConverter)new JsonStringEnumConverter() },
		Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
	};
}
