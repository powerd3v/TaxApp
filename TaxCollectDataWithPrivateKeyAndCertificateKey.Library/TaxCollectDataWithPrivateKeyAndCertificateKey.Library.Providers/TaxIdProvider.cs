using System;
using System.Text;
using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Abstraction.Providers;

namespace TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Providers;

public class TaxIdProvider : ITaxIdProvider
{
	private readonly IErrorDetectionAlgorithm _errorDetectionAlgorithm;

	public TaxIdProvider(IErrorDetectionAlgorithm errorDetectionAlgorithm)
	{
		_errorDetectionAlgorithm = errorDetectionAlgorithm;
	}

	public string GenerateTaxId(string memoryId, long serial, DateTime createDate)
	{
		int num = (int)(new DateTimeOffset(createDate).ToUnixTimeSeconds() / 86400);
		string text = Convert.ToString(num, 16);
		string text2 = Convert.ToString(serial, 16);
		string text3 = memoryId + text.PadLeft(5, '0') + text2.PadLeft(10, '0');
		string num2 = ToDecimal(memoryId) + num.ToString().PadLeft(6, '0') + serial.ToString().PadLeft(12, '0');
		string text4 = text3 + _errorDetectionAlgorithm.GenerateCheckDigit(num2);
		return text4.ToUpperInvariant();
	}

	private static string ToDecimal(string memoryId)
	{
		StringBuilder stringBuilder = new StringBuilder();
		foreach (char c in memoryId)
		{
			if (char.IsDigit(c))
			{
				stringBuilder.Append(c);
			}
			else
			{
				stringBuilder.Append((int)c);
			}
		}
		return stringBuilder.ToString();
	}
}
