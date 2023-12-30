using System;
using System.Collections.Generic;

namespace TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Dto;

public class InquiryByReferenceNumberDto
{
	public List<string> ReferenceNumbers { get; }

	public DateTime? Start { get; }

	public DateTime? End { get; }

	public InquiryByReferenceNumberDto(List<string> referenceNumbers, DateTime? start = null, DateTime? end = null)
	{
		ReferenceNumbers = referenceNumbers;
		Start = start;
		End = end;
	}
}
