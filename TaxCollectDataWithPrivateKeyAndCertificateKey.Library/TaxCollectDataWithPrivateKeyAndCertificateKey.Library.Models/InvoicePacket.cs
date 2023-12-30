using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Dto;

namespace TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Models;

public class InvoicePacket
{
	public string TaxId { get; }

	public PacketDto PacketDto { get; }

	public InvoicePacket(string taxId, PacketDto packetDto)
	{
		TaxId = taxId;
		PacketDto = packetDto;
	}
}
