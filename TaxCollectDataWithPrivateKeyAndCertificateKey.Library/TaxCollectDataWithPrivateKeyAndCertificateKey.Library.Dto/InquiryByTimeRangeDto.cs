using System;
using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Enums;

namespace TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Dto;

public class InquiryByTimeRangeDto
{
	public DateTime Start { get; }

	public DateTime? End { get; }

	public Pageable? Pageable { get; }

	public RequestStatus? Status { get; }

	public InquiryByTimeRangeDto(DateTime start, DateTime? end = null, Pageable? pageable = null, RequestStatus? status = null)
	{
		Start = start;
		End = end;
		Pageable = pageable;
		Status = status;
	}
}
