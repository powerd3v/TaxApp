using System;
using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Abstraction.Providers;

namespace TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Algorithms;

public class VerhoeffAlgorithm : IErrorDetectionAlgorithm
{
	private readonly int[,] _multiplicationTable = new int[10, 10]
	{
		{ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 },
		{ 1, 2, 3, 4, 0, 6, 7, 8, 9, 5 },
		{ 2, 3, 4, 0, 1, 7, 8, 9, 5, 6 },
		{ 3, 4, 0, 1, 2, 8, 9, 5, 6, 7 },
		{ 4, 0, 1, 2, 3, 9, 5, 6, 7, 8 },
		{ 5, 9, 8, 7, 6, 0, 4, 3, 2, 1 },
		{ 6, 5, 9, 8, 7, 1, 0, 4, 3, 2 },
		{ 7, 6, 5, 9, 8, 2, 1, 0, 4, 3 },
		{ 8, 7, 6, 5, 9, 3, 2, 1, 0, 4 },
		{ 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 }
	};

	private readonly int[,] _permutationTable = new int[8, 10]
	{
		{ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 },
		{ 1, 5, 7, 6, 2, 8, 3, 0, 9, 4 },
		{ 5, 8, 0, 3, 7, 9, 6, 1, 4, 2 },
		{ 8, 9, 1, 6, 0, 4, 3, 5, 2, 7 },
		{ 9, 4, 5, 3, 1, 2, 6, 8, 7, 0 },
		{ 4, 2, 8, 6, 5, 7, 3, 9, 0, 1 },
		{ 2, 7, 9, 3, 8, 0, 6, 4, 1, 5 },
		{ 7, 0, 4, 6, 9, 1, 3, 2, 5, 8 }
	};

	private readonly int[] _inverseTable = new int[10] { 0, 4, 3, 2, 1, 5, 6, 7, 8, 9 };

	public string GenerateCheckDigit(string num)
	{
		int num2 = 0;
		int[] array = StringToReversedIntArray(num);
		for (int i = 0; i < array.Length; i++)
		{
			num2 = _multiplicationTable[num2, _permutationTable[(i + 1) % 8, array[i]]];
		}
		return _inverseTable[num2].ToString();
	}

	public bool ValidateCheckDigit(string num)
	{
		int num2 = 0;
		int[] array = StringToReversedIntArray(num);
		for (int i = 0; i < array.Length; i++)
		{
			num2 = _multiplicationTable[num2, _permutationTable[i % 8, array[i]]];
		}
		return num2 == 0;
	}

	private static int[] StringToReversedIntArray(string num)
	{
		int[] array = new int[num.Length];
		for (int i = 0; i < num.Length; i++)
		{
			array[i] = int.Parse(num.Substring(i, 1));
		}
		Array.Reverse((Array)array);
		return array;
	}
}
