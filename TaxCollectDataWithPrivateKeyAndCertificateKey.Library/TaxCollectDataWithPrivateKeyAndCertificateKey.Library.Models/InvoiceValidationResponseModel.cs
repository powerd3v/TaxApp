using System.Collections.Generic;

namespace TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Models;

public class InvoiceValidationResponseModel
{
	public List<InvoiceErrorModel> Error { get; }

	public List<InvoiceErrorModel> Warning { get; }

	public bool Success { get; }

	public InvoiceValidationResponseModel(List<InvoiceErrorModel> error, List<InvoiceErrorModel> warning, bool success)
	{
		Error = error;
		Warning = warning;
		Success = success;
	}
}
