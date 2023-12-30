using System.Threading.Tasks;
using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Models;

namespace TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Abstraction.Clients;

public interface ITaxPublicApi
{
	ServerInformationModel GetServerInformation();

	Task<ServerInformationModel> GetServerInformationAsync();
}
