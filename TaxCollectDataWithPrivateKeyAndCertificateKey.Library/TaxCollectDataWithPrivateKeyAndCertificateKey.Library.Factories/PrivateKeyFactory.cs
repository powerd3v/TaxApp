using System.IO;
using System.Security.Cryptography;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;

namespace TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Factories;

public class PrivateKeyFactory
{
	public RSA ReadPrivateKeyFromFile(string privateKeyPath)
	{
		PemReader val = new PemReader((TextReader)File.OpenText(privateKeyPath));
		return DotNetUtilities.ToRSA((RsaPrivateCrtKeyParameters)val.ReadObject());
	}

	public RSA ReadPrivateKey(string privateKey)
	{
		string s = "-----BEGIN PRIVATE KEY-----\n" + privateKey + "\n-----END PRIVATE KEY-----";
		PemReader val = new PemReader((TextReader)new StringReader(s));
		return DotNetUtilities.ToRSA((RsaPrivateCrtKeyParameters)val.ReadObject());
	}
}
