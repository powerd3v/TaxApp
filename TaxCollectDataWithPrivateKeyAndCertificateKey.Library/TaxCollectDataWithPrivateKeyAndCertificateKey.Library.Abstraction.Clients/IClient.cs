using System.Net.Http;
using System.Threading.Tasks;

namespace TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Abstraction.Clients;

public interface IClient
{
	Task<T> SendRequestAsync<T>(HttpRequestMessage request, HttpRequestMessage nonceRequest);
}
