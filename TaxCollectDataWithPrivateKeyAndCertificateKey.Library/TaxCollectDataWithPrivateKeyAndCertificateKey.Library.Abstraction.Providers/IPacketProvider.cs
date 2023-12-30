using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Dto;

namespace TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Abstraction.Providers;

public interface IPacketProvider
{
	PacketDto CreateInvoicePacket(InvoiceDto invoice);
}
