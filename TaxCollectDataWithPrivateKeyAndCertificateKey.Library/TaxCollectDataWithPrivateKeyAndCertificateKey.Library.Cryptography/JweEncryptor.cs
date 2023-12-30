using System.Collections.Generic;
using System.Diagnostics;
using Jose;
using Microsoft.Extensions.Logging;
using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Abstraction.Cryptography;
using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Abstraction.Repositories;

namespace TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Cryptography;

public class JweEncryptor : IEncryptor
{
	private readonly IEncryptionKeyRepository _repository;

	private readonly ILogger? _logger;

	public JweEncryptor(IEncryptionKeyRepository repository, ILogger? logger = null)
	{
		_repository = repository;
		_logger = logger;
	}

	public string Encrypt(string text)
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Expected O, but got Unknown
		Stopwatch stopwatch = Stopwatch.StartNew();
		try
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object> { 
			{
				"kid",
				_repository.GetKeyId()
			} };
			JweRecipient val = new JweRecipient((JweAlgorithm)2, (object)_repository.GetKey(), (IDictionary<string, object>)dictionary);
			return JWE.Encrypt(text, (IEnumerable<JweRecipient>)(object)new JweRecipient[1] { val }, (JweEncryption)5, (byte[])null, (SerializationMode)0, (JweCompression?)null, (IDictionary<string, object>)null, (IDictionary<string, object>)null, (JwtSettings)null);
		}
		finally
		{
			_logger?.LogDebug("encrypt in {} ms", stopwatch.ElapsedMilliseconds);
		}
	}
}
