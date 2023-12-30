namespace TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Models;

public class InvoiceErrorModel
{
	public string Code { get; }

	public string Message { get; }

	public InvoiceErrorModel(string code, string message)
	{
		Code = code;
		Message = message;
	}
}
