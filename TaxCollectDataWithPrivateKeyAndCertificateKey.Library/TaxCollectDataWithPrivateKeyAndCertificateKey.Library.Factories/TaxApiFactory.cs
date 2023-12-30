using System;
using System.Net.Http;
using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Abstraction.Clients;
using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Abstraction.Cryptography;
using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Abstraction.Properties;
using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Abstraction.Providers;
using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Clients;
using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Properties;
using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Providers;

namespace TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Factories;

public class TaxApiFactory
{
	private readonly PacketProviderFactory _packetProviderFactory;

	private readonly IRequestProvider _requestProvider;

	private readonly TaxProperties _taxProperties;

	private readonly IHttpHeadersProperties _httpHeadersProperties;

	public TaxApiFactory(TaxProperties taxProperties, IRequestProvider requestProvider, IHttpHeadersProperties httpHeadersProperties)
	{
		_taxProperties = taxProperties;
		_requestProvider = requestProvider;
		_httpHeadersProperties = httpHeadersProperties;
		_packetProviderFactory = new PacketProviderFactory(taxProperties);
	}

	public TaxApiFactory(TaxProperties taxProperties, IUrlProperties urlProperties, IHttpHeadersProperties httpHeadersProperties)
		: this(taxProperties, new RequestProvider(urlProperties, new Serializer()), httpHeadersProperties)
	{
	}

	public TaxApiFactory(string baseUrl, TaxProperties taxProperties)
		: this(taxProperties, new RequestProvider(new DefaultUrlProperties(baseUrl), new Serializer()), new DefaultHttpHeadersProperties())
	{
	}

	public ITaxPublicApi CreatePublicApi(ISignatory signatory)
	{
		LowLevelTaxApi sender = CreateLowLevelApi(signatory);
		return new TaxPublicApi(sender);
	}

	public ITaxApi CreateApi(ISignatory signatory, IEncryptor encryptor)
	{
		LowLevelTaxApi sender = CreateLowLevelApi(signatory);
		IPacketProvider packetFactory = _packetProviderFactory.CreatePacketProvider(signatory, encryptor);
		return new TaxApi(sender, packetFactory);
	}

	public LowLevelTaxApi CreateLowLevelApi(ISignatory signatory)
	{
		return new LowLevelTaxApi(CreateRestHttpClient(signatory), _requestProvider);
	}

	private IClient CreateRestHttpClient(ISignatory signatory)
	{
		return new RestSharpHttpClient(GetHttpClient(), _taxProperties, signatory, _httpHeadersProperties, new Serializer());
	}

	private HttpClient GetHttpClient()
	{
		return new HttpClient
		{
			Timeout = TimeSpan.FromMinutes(10.0)
		};
	}
}
