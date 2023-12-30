namespace TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Models;

public class InvoiceResponseModel
{
	public string Data { get; }

	public string Uid { get; }

	public string ReferenceNumber { get; }

	public string TaxId { get; }

	public InvoiceResponseModel(string data, string uid, string referenceNumber, string taxId)
	{
		Data = data;
		Uid = uid;
		ReferenceNumber = referenceNumber;
		TaxId = taxId;
	}
}
