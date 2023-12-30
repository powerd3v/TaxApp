using System;
using System.Collections.Generic;

namespace TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Dto;

public class InquiryByUidDto
{
	public List<string> UidList { get; }

	public string FiscalId { get; }

	public DateTime? Start { get; }

	public DateTime? End { get; }

	public InquiryByUidDto(List<string> uidList, string fiscalId, DateTime? start = null, DateTime? end = null)
	{
		UidList = uidList;
		FiscalId = fiscalId;
		Start = start;
		End = end;
	}
}
