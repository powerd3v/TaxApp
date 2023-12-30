using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Text.Json.Nodes;
using JWT.Algorithms;
using JWT.Builder;
using Microsoft.Extensions.Logging;
using Net.Pkcs11Interop.X509Store;
using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Abstraction.Cryptography;
using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Providers;

namespace TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Cryptography;

public class JwsHardwareTokenSignatory : ISignatory
{
	private class ETokenPinProvider : IPinProvider
	{
		private readonly string _pin;

		public ETokenPinProvider(string pin)
		{
			_pin = pin;
		}

		private GetPinResult GetPin()
		{
			//IL_0031: Unknown result type (might be due to invalid IL or missing references)
			//IL_0037: Expected O, but got Unknown
			return new GetPinResult(false, _pin.Select((char ch) => (byte)ch).ToArray());
		}

		public GetPinResult GetKeyPin(Pkcs11X509StoreInfo storeInfo, Pkcs11SlotInfo slotInfo, Pkcs11TokenInfo tokenInfo, Pkcs11X509CertificateInfo certificateInfo)
		{
			return GetPin();
		}

		public GetPinResult GetTokenPin(Pkcs11X509StoreInfo storeInfo, Pkcs11SlotInfo slotInfo, Pkcs11TokenInfo tokenInfo)
		{
			return GetPin();
		}
	}

	private readonly X509Certificate2 _parsedCertificate;

	private readonly RS512Algorithm _algorithm;

	private readonly ILogger? _logger;

	private const string Jose = "jose";

	private const string SigT = "sigT";

	private const string Typ = "typ";

	private const string Crit = "crit";

	private const string Cty = "cty";

	private const string ContentTypeHeaderValue = "text/plain";

	public JwsHardwareTokenSignatory(string certificateAlias, string pkcs11LibraryPath, string pin, ILogger? logger = null)
	{
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Expected O, but got Unknown
		Pkcs11X509Certificate certificateFromStore = GetCertificateFromStore(certificateAlias, pkcs11LibraryPath, pin);
		_parsedCertificate = certificateFromStore.Info.ParsedCertificate;
		_algorithm = new RS512Algorithm(certificateFromStore.GetRSAPublicKey(), certificateFromStore.GetRSAPrivateKey());
		_logger = logger;
	}

	public JwsHardwareTokenSignatory(RS512Algorithm algorithm, X509Certificate2 certificate, ILogger? logger = null)
	{
		_parsedCertificate = certificate;
		_algorithm = algorithm;
		_logger = logger;
	}

	private Pkcs11X509Certificate GetCertificateFromStore(string certificateAlias, string pkcs11LibraryPath, string pin)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Expected O, but got Unknown
		string certificateAlias2 = certificateAlias;
		Pkcs11X509Store val = new Pkcs11X509Store(pkcs11LibraryPath, (IPinProvider)(object)new ETokenPinProvider(pin));
		return val.Slots[0].Token.Certificates.First((Pkcs11X509Certificate c) => c.Info.Label.Equals(certificateAlias2, StringComparison.InvariantCultureIgnoreCase));
	}

	public string Sign(string text)
	{
		Stopwatch stopwatch = Stopwatch.StartNew();
		string result = JwtBuilder.Create().DoNotVerifySignature().WithAlgorithm((IJwtAlgorithm)(object)_algorithm)
			.AddHeader((HeaderName)5, (object)new string[1] { Convert.ToBase64String(_parsedCertificate.GetRawCertData()) })
			.AddHeader("sigT", (object)new CurrentDateProvider().ToDateFormat())
			.AddHeader("typ", (object)"jose")
			.AddHeader("crit", (object)new string[1] { "sigT" })
			.AddHeader("cty", (object)"text/plain")
			.Encode((object)JsonSerializer.Deserialize<JsonNode>(text));
		_logger?.LogDebug("sign in {} ms", stopwatch.ElapsedMilliseconds);
		return result;
	}

	public string Sign(object data)
	{
		Stopwatch stopwatch = Stopwatch.StartNew();
		string result = JwtBuilder.Create().DoNotVerifySignature().WithAlgorithm((IJwtAlgorithm)(object)_algorithm)
			.AddHeader((HeaderName)5, (object)new string[1] { Convert.ToBase64String(_parsedCertificate.GetRawCertData()) })
			.AddHeader("sigT", (object)new CurrentDateProvider().ToDateFormat())
			.AddHeader("typ", (object)"jose")
			.AddHeader("crit", (object)new string[1] { "sigT" })
			.AddHeader("cty", (object)"text/plain")
			.Encode(data);
		_logger?.LogDebug("sign in {} ms", stopwatch.ElapsedMilliseconds);
		return result;
	}
}
