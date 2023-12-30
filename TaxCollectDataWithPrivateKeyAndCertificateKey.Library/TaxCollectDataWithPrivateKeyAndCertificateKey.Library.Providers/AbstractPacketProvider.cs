using System;
using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Dto;
using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Properties;

namespace TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Providers;

public abstract class AbstractPacketProvider
{
	protected readonly TaxProperties PacketProperties;

	protected AbstractPacketProvider(TaxProperties packetProperties)
	{
		PacketProperties = packetProperties;
	}

	protected PacketHeaderDto GetHeader()
	{
		string requestTraceId = Guid.NewGuid().ToString();
		string memoryId = PacketProperties.MemoryId;
		return new PacketHeaderDto
		{
			RequestTraceId = requestTraceId,
			FiscalId = memoryId
		};
	}
}
