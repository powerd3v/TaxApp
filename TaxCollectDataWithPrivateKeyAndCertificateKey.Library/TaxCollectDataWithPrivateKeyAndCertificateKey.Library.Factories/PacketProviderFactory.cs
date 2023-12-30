using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Abstraction.Cryptography;
using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Abstraction.Providers;
using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Properties;
using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Providers;

namespace TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Factories;

public class PacketProviderFactory
{
	private readonly TaxProperties _packetProperties;

	public PacketProviderFactory(TaxProperties packetProperties)
	{
		_packetProperties = packetProperties;
	}

	public IPacketProvider CreatePacketProvider(ISignatory signatory, IEncryptor encryptor)
	{
		return new PacketProvider(_packetProperties, signatory, encryptor, new Serializer());
	}
}
