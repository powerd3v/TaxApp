using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http;
using System.Text;
using System.Web;
using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Abstraction.Properties;
using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Abstraction.Providers;
using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Dto;

namespace TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Providers;

public class RequestProvider : IRequestProvider
{
	private const string MemoryId = "memoryId";

	private const string EconomicCode = "economicCode";

	private const string ReferenceIds = "referenceIds";

	private const string UidList = "uidList";

	private const string FiscalId = "fiscalId";

	private const string PageSize = "pageSize";

	private const string PageNumber = "pageNumber";

	private const string End = "end";

	private const string Start = "start";

	private const string Status = "status";

	private const string MediaType = "application/json";

	private const string DateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fffffff00K";

	private readonly IUrlProperties _urlProperties;

	private readonly ISerializer _serializer;

	public RequestProvider(IUrlProperties urlProperties, ISerializer serializer)
	{
		_urlProperties = urlProperties;
		_serializer = serializer;
	}

	public HttpRequestMessage GetNonceRequest()
	{
		return new HttpRequestMessage(HttpMethod.Get, _urlProperties.GetUrl("nonce"));
	}

	public HttpRequestMessage GetServerInformation()
	{
		return new HttpRequestMessage(HttpMethod.Get, _urlProperties.GetUrl("server-information"));
	}

	public HttpRequestMessage GetInquiryByTimeRequest(InquiryByTimeRangeDto dto)
	{
		NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(string.Empty);
		nameValueCollection["start"] = dto.Start.ToString("yyyy-MM-ddTHH:mm:ss.fffffff00K");
		if (dto.End.HasValue)
		{
			nameValueCollection["end"] = dto.End?.ToString("yyyy-MM-ddTHH:mm:ss.fffffff00K");
		}
		if (dto.Status.HasValue)
		{
			nameValueCollection["status"] = dto.Status.ToString();
		}
		if (dto.Pageable != null)
		{
			nameValueCollection["pageNumber"] = dto.Pageable.PageNumber.ToString();
			nameValueCollection["pageSize"] = dto.Pageable.PageSize.ToString();
		}
		return GetRequestFromQuery("inquiry", nameValueCollection.ToString());
	}

	public HttpRequestMessage GetInquiryByUidRequest(InquiryByUidDto dto)
	{
		NameValueCollection query = HttpUtility.ParseQueryString(string.Empty);
		query["fiscalId"] = dto.FiscalId;
		dto.UidList.ForEach(delegate(string u)
		{
			query.Add("uidList", u);
		});
		if (dto.Start.HasValue)
		{
			query["start"] = dto.Start?.ToString("yyyy-MM-ddTHH:mm:ss.fffffff00K");
		}
		if (dto.End.HasValue)
		{
			query["end"] = dto.End?.ToString("yyyy-MM-ddTHH:mm:ss.fffffff00K");
		}
		return GetRequestFromQuery("inquiry-by-uid", query.ToString());
	}

	public HttpRequestMessage GetInquiryByReferenceIdRequest(InquiryByReferenceNumberDto dto)
	{
		NameValueCollection query = HttpUtility.ParseQueryString(string.Empty);
		dto.ReferenceNumbers.ForEach(delegate(string u)
		{
			query.Add("referenceIds", u);
		});
		if (dto.Start.HasValue)
		{
			query["start"] = dto.Start?.ToString("yyyy-MM-ddTHH:mm:ss.fffffff00K");
		}
		if (dto.End.HasValue)
		{
			query["end"] = dto.End?.ToString("yyyy-MM-ddTHH:mm:ss.fffffff00K");
		}
		return GetRequestFromQuery("inquiry-by-reference-id", query.ToString());
	}

	public HttpRequestMessage GetInvoicesRequest(List<PacketDto> data)
	{
		HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, _urlProperties.GetUrl("invoice"));
		httpRequestMessage.Content = new StringContent(_serializer.Serialize(data), Encoding.UTF8, "application/json");
		return httpRequestMessage;
	}

	public HttpRequestMessage GetTaxpayerRequest(string economicCode)
	{
		NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(string.Empty);
		nameValueCollection["economicCode"] = economicCode;
		return GetRequestFromQuery("taxpayer", nameValueCollection.ToString());
	}

	public HttpRequestMessage GetFiscalInformationRequest(string memoryId)
	{
		NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(string.Empty);
		nameValueCollection["memoryId"] = memoryId;
		return GetRequestFromQuery("fiscal-information", nameValueCollection.ToString());
	}

	private HttpRequestMessage GetRequestFromQuery(string requestUrl, string query)
	{
		UriBuilder uriBuilder = new UriBuilder(_urlProperties.GetUrl(requestUrl))
		{
			Query = query
		};
		return new HttpRequestMessage(HttpMethod.Get, uriBuilder.ToString());
	}
}
