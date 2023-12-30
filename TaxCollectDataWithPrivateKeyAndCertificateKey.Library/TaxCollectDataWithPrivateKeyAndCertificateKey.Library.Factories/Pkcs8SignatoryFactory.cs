using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Abstraction.Cryptography;
using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Providers;

namespace TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Factories;

public class Pkcs8SignatoryFactory
{
    private readonly SignatoryFactory _signatoryFactory;

    private readonly X509CertificateFactory _x509CertificateFactory;

    private readonly PrivateKeyFactory _privateKeyFactory;

    public Pkcs8SignatoryFactory()
    {
        CurrentDateProvider currentDateProvider = new CurrentDateProvider();
        _signatoryFactory = new SignatoryFactory(currentDateProvider);
        _x509CertificateFactory = new X509CertificateFactory();
        _privateKeyFactory = new PrivateKeyFactory();
    }

    public ISignatory Create(string privateKey, string certificate, string publicKey)
    {
        RSA privateKey2 = _privateKeyFactory.ReadPrivateKeyFromFile(privateKey);
        string pri = File.ReadAllText(privateKey), pub = File.ReadAllText(publicKey);
        X509Certificate x509Certificate = _x509CertificateFactory.ReadCertificateFromFile(certificate, pri, pub);
        return _signatoryFactory.Create(privateKey2, x509Certificate);
    }
}
