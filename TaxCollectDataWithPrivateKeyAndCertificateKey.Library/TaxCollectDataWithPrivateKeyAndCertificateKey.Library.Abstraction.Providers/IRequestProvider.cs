using System.Collections.Generic;
using System.Net.Http;
using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Dto;

namespace TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Abstraction.Providers;

public interface IRequestProvider
{
	HttpRequestMessage GetNonceRequest();

	HttpRequestMessage GetServerInformation();

	HttpRequestMessage GetInquiryByTimeRequest(InquiryByTimeRangeDto dto);

	HttpRequestMessage GetInquiryByUidRequest(InquiryByUidDto dto);

	HttpRequestMessage GetInquiryByReferenceIdRequest(InquiryByReferenceNumberDto dto);

	HttpRequestMessage GetInvoicesRequest(List<PacketDto> data);

	HttpRequestMessage GetTaxpayerRequest(string economicCode);

	HttpRequestMessage GetFiscalInformationRequest(string memoryId);
}
