using System;
using System.Globalization;
using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Abstraction.Providers;

namespace TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Providers;

public class CurrentDateProvider : ICurrentDateProvider
{
	private const string Format = "yyyy-MM-dd'T'HH:mm:ss'Z'";

	public long ToEpochMilli()
	{
		return new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds();
	}

	public string ToDateFormat()
	{
		return DateTime.UtcNow.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'", CultureInfo.InvariantCulture);
	}
}
