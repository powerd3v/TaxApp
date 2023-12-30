using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Abstraction.Clients;
using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Abstraction.Cryptography;
using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Abstraction.Properties;
using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Abstraction.Providers;
using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Configs;
using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Models;
using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Properties;

namespace TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Clients;

public class RestSharpHttpClient : IClient
{
	private const string Bearer = "Bearer ";

	private const string MediaType = "application/json";

	private const string Charset = "utf-8";

	private readonly HttpClient _httpClient;

	private readonly ISignatory _signatory;

	private readonly TaxProperties _taxProperties;

	private readonly IHttpHeadersProperties _httpHeadersProperties;

	private readonly ISerializer _serializer;

	private readonly ILogger? _logger;

	public RestSharpHttpClient(HttpClient httpClient, TaxProperties taxProperties, ISignatory signatory, IHttpHeadersProperties httpHeadersProperties, ISerializer serializer, ILogger? logger = null)
	{
		_httpClient = httpClient;
		_taxProperties = taxProperties;
		_signatory = signatory;
		_httpHeadersProperties = httpHeadersProperties;
		_serializer = serializer;
		_logger = logger;
	}

	public async Task<T> SendRequestAsync<T>(HttpRequestMessage request, HttpRequestMessage nonceRequest)
	{
		return await SendRequestAsync<T>(await GetAuthenticatedRequestAsync(request, nonceRequest).ConfigureAwait(continueOnCapturedContext: false)).ConfigureAwait(continueOnCapturedContext: false);
	}

	private async Task<T> SendRequestAsync<T>(HttpRequestMessage request)
	{
		Stopwatch stopWatch = Stopwatch.StartNew();
		foreach (KeyValuePair<string, string> customHeader in _httpHeadersProperties.CustomHeaders)
		{
			request.Headers.Add(customHeader.Key, customHeader.Value);
		}
		using HttpResponseMessage response = await _httpClient.SendAsync(request).ConfigureAwait(continueOnCapturedContext: false);
		try
		{
			_logger?.LogDebug("call {}", request.RequestUri.ToString());
			return await response.Content.ReadFromJsonAsync<T>(JsonSerializerConfig.JsonSerializerOptions).ConfigureAwait(continueOnCapturedContext: false);
		}
		finally
		{
			stopWatch.Stop();
			_logger?.LogDebug("send and receive in {} ms", stopWatch.ElapsedMilliseconds);
		}
	}

	private async Task<HttpRequestMessage> GetAuthenticatedRequestAsync(HttpRequestMessage request, HttpRequestMessage nonceRequest)
	{
		string signedNonce = await GetSignedNonceAsync(nonceRequest).ConfigureAwait(continueOnCapturedContext: false);
		request.Headers.Add(_httpHeadersProperties.AuthorizationHeaderName, signedNonce);
		request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		request.Headers.AcceptCharset.Add(new StringWithQualityHeaderValue("utf-8"));
		return request;
	}

	private async Task<string> GetSignedNonceAsync(HttpRequestMessage request)
	{
		TokenModel tokenModel = new TokenModel((await SendRequestAsync<NonceEntity>(request).ConfigureAwait(continueOnCapturedContext: false)).Nonce, _taxProperties.MemoryId);
		return "Bearer  " + _signatory.Sign(_serializer.Serialize(tokenModel));
	}
}
