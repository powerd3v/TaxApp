using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using JWT;
using JWT.Algorithms;
using JWT.Builder;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Abstraction.Cryptography;
using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Abstraction.Providers;
using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Configs;

namespace TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Cryptography;

public class JwsSignatory : ISignatory
{
	private class CustomSerializer : IJsonSerializer
	{
		public string Serialize(object obj)
		{
			return Encoding.UTF8.GetString(JsonSerializer.SerializeToUtf8Bytes(obj, JsonSerializerConfig.JsonSerializerOptions));
		}

		public object? Deserialize(Type type, string json)
		{
			return JsonSerializer.Deserialize(json, type);
		}
	}

	private const string Jose = "jose";

	private const string SigT = "sigT";

	private const string Typ = "typ";

	private const string Crit = "crit";

	private const string Cty = "cty";

	private const string ContentTypeHeaderValue = "text/plain";

	private readonly X509Certificate _certificate;

	private readonly RSA _privateKey;

	private readonly ICurrentDateProvider _currentDateProvider;

	private readonly ILogger? _logger;

	public JwsSignatory(X509Certificate certificate, RSA privateKey, ICurrentDateProvider currentDateProvider, ILogger? logger = null)
	{
		_certificate = certificate;
		_privateKey = privateKey;
		_currentDateProvider = currentDateProvider;
		_logger = logger;
	}

	public string Sign(string text)
	{
		 
		//IL_0022: Expected O, but got Unknown
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Expected O, but got Unknown
		Stopwatch stopwatch = Stopwatch.StartNew();
		try
		{
			RSA rSA = DotNetUtilities.ToRSA((RsaKeyParameters)DotNetUtilities.FromX509Certificate(_certificate).GetPublicKey());
			return JwtBuilder.Create().WithJsonSerializer((IJsonSerializer)(object)new CustomSerializer()).WithAlgorithm((IJwtAlgorithm)new RS256Algorithm(rSA, _privateKey))
				.AddHeader((HeaderName)5, (object)new string[1] { Convert.ToBase64String(_certificate.GetRawCertData()) })
				.AddHeader("sigT", (object)_currentDateProvider.ToDateFormat())
				.AddHeader("typ", (object)"jose")
				.AddHeader("crit", (object)new string[1] { "sigT" })
				.AddHeader("cty", (object)"text/plain")
				.Encode((object)JsonSerializer.Deserialize<JsonNode>(text));
		}
		finally
		{
			_logger?.LogDebug("sign in {} ms", stopwatch.ElapsedMilliseconds);
		}
	}

	public string Sign(object data)
	{
		 
		//IL_0022: Expected O, but got Unknown
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Expected O, but got Unknown
		Stopwatch stopwatch = Stopwatch.StartNew();
		try
		{
			RSA rSA = DotNetUtilities.ToRSA((RsaKeyParameters)DotNetUtilities.FromX509Certificate(_certificate).GetPublicKey());
			return JwtBuilder.Create().WithJsonSerializer((IJsonSerializer)(object)new CustomSerializer()).WithAlgorithm((IJwtAlgorithm)new RS256Algorithm(rSA, _privateKey))
				.AddHeader((HeaderName)5, (object)new string[1] { Convert.ToBase64String(_certificate.GetRawCertData()) })
				.AddHeader("sigT", (object)_currentDateProvider.ToDateFormat())
				.AddHeader("typ", (object)"jose")
				.AddHeader("crit", (object)new string[1] { "sigT" })
				.AddHeader("cty", (object)"text/plain")
				.Encode(data);
		}
		finally
		{
			_logger?.LogDebug("sign in {} ms", stopwatch.ElapsedMilliseconds);
		}
	}
}
