using System.Collections.Generic;

namespace TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Models;

public class BatchResponseModel
{
	public long Timestamp { get; set; }

	public List<ResponsePacketModel> Result { get; set; }
}
