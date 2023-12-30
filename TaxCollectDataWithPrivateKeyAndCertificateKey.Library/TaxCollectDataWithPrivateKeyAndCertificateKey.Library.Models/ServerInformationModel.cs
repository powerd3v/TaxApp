using System.Collections.Generic;

namespace TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Models;

public class ServerInformationModel
{
	public long ServerTime { get; set; }

	public List<KeyModel> PublicKeys { get; set; }
}
