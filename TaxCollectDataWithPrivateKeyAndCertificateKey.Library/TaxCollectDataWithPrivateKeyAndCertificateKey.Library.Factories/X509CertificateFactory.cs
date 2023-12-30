using System;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Operators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;
using PemReader = Org.BouncyCastle.OpenSsl.PemReader;
using X509Certificate = System.Security.Cryptography.X509Certificates.X509Certificate;

namespace TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Factories;

public class X509CertificateFactory
{
    public X509Certificate ReadCertificateFromFile(string certificatePath, string privateKey, string publicKey)
    {
        PemReader certificateReader = new PemReader(new StreamReader(certificatePath));
        return DotNetUtilities.ToX509Certificate(FromPkcs10ToX509Cert(certificateReader, privateKey, publicKey));
    }

    public X509Certificate ReadCertificate(string certificate, string privateKey, string publicKey)
    {
        string s = "-----BEGIN CERTIFICATE-----\n" + certificate + "\n-----END CERTIFICATE-----";
        PemReader certificateReader = new PemReader(new StringReader(s));
        return DotNetUtilities.ToX509Certificate(FromPkcs10ToX509Cert(certificateReader, privateKey, publicKey));
    }
    private Org.BouncyCastle.X509.X509Certificate FromPkcs10ToX509Cert(PemReader pemReader, string privateKeyPem, string publicKeyPem)
    {
        Pkcs10CertificationRequest certificationRequest = (Pkcs10CertificationRequest)pemReader.ReadObject();

        RsaKeyParameters publicKeyParams = GetPublicKeyParameters(publicKeyPem);
        RsaPrivateCrtKeyParameters privateKeyParams = GetPrivateKeyParameters(privateKeyPem);

        AsymmetricCipherKeyPair keyPair = new AsymmetricCipherKeyPair(publicKeyParams,privateKeyParams);

        X509V3CertificateGenerator generator = new X509V3CertificateGenerator();
        generator.SetSerialNumber(BigInteger.ValueOf(DateTime.Now.Ticks));
        generator.SetIssuerDN(certificationRequest.GetCertificationRequestInfo().Subject);
        generator.SetSubjectDN(certificationRequest.GetCertificationRequestInfo().Subject);
        generator.SetNotBefore(DateTime.Now.AddHours(-1));
        generator.SetNotAfter(DateTime.Now.AddYears(1));
        generator.SetPublicKey(keyPair.Public);

        ISignatureFactory signatureFactory = new Asn1SignatureFactory("SHA256WITHRSA", keyPair.Private, null);
        return generator.Generate(signatureFactory);
    }


    private RsaKeyParameters GetPublicKeyParameters(string publicKeyPem)
    {
        StringReader stringReader = new StringReader(publicKeyPem);
        PemReader pemReader = new PemReader(stringReader);
        RsaKeyParameters publicKeyParams = ((RsaKeyParameters)pemReader.ReadObject());
        return publicKeyParams;
    }

    private RsaPrivateCrtKeyParameters GetPrivateKeyParameters(string privateKeyPem)
    {
        StringReader stringReader = new StringReader(privateKeyPem);
        PemReader pemReader = new PemReader(stringReader);
        RsaPrivateCrtKeyParameters privateKeyParams = ((RsaPrivateCrtKeyParameters)pemReader.ReadObject());
        return privateKeyParams;
    }
}