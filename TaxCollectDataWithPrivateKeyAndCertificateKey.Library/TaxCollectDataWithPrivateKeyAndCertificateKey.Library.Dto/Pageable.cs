namespace TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Dto;

public class Pageable
{
	public int PageNumber { get; }

	public int PageSize { get; }

	public Pageable(int pageNumber = 0, int pageSize = 10)
	{
		PageNumber = pageNumber;
		PageSize = pageSize;
	}
}
