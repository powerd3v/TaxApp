using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Abstraction.Clients;
using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Abstraction.Providers;
using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Dto;
using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Models;

namespace TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Clients;

public class TaxApi : ITaxApi
{
	private readonly ILowLevelTaxApi _sender;

	private readonly IPacketProvider _packetFactory;

	public TaxApi(ILowLevelTaxApi sender, IPacketProvider packetFactory)
	{
		_sender = sender;
		_packetFactory = packetFactory;
	}

	public List<InquiryResultModel> InquiryByTime(InquiryByTimeRangeDto dto)
	{
		return _sender.InquiryByTime(dto);
	}

	public async Task<List<InquiryResultModel>> InquiryByTimeAsync(InquiryByTimeRangeDto dto)
	{
		return await _sender.InquiryByTimeAsync(dto).ConfigureAwait(continueOnCapturedContext: false);
	}

	public List<InquiryResultModel> InquiryByUid(InquiryByUidDto dto)
	{
		return _sender.InquiryByUid(dto);
	}

	public async Task<List<InquiryResultModel>> InquiryByUidAsync(InquiryByUidDto dto)
	{
		return await _sender.InquiryByUidAsync(dto).ConfigureAwait(continueOnCapturedContext: false);
	}

	public List<InquiryResultModel> InquiryByReferenceId(InquiryByReferenceNumberDto dto)
	{
		return _sender.InquiryByReferenceId(dto);
	}

	public async Task<List<InquiryResultModel>> InquiryByReferenceIdAsync(InquiryByReferenceNumberDto dto)
	{
		return await _sender.InquiryByReferenceIdAsync(dto).ConfigureAwait(continueOnCapturedContext: false);
	}

	public FiscalFullInformationModel GetFiscalInformation(string memoryId)
	{
		return _sender.GetFiscalInformation(memoryId);
	}

	public async Task<FiscalFullInformationModel> GetFiscalInformationAsync(string memoryId)
	{
		return await _sender.GetFiscalInformationAsync(memoryId).ConfigureAwait(continueOnCapturedContext: false);
	}

	public TaxpayerModel GetTaxpayer(string economicCode)
	{
		return _sender.GetTaxpayer(economicCode);
	}

	public async Task<TaxpayerModel> GetTaxpayerAsync(string economicCode)
	{
		return await _sender.GetTaxpayerAsync(economicCode).ConfigureAwait(continueOnCapturedContext: false);
	}

	public List<InvoiceResponseModel> SendInvoices(List<InvoiceDto> invoices)
	{
		List<InvoicePacket> source = invoices.Select(GetInvoicePacket).ToList();
		List<PacketDto> packets = source.Select((InvoicePacket i) => i.PacketDto).ToList();
		List<ResponsePacketModel> result = _sender.SendInvoices(packets).Result;
		Dictionary<string, string> map = source.ToDictionary((InvoicePacket i) => i.PacketDto.Header.RequestTraceId, (InvoicePacket i) => i.TaxId);
		return result.Select((ResponsePacketModel r) => GetInvoiceResponseModel(map, r)).ToList();
	}

	public async Task<List<InvoiceResponseModel>> SendInvoicesAsync(List<InvoiceDto> invoices)
	{
		List<InvoicePacket> invoicePackets = invoices.Select(GetInvoicePacket).ToList();
		List<PacketDto> packets = invoicePackets.Select((InvoicePacket i) => i.PacketDto).ToList();
		BatchResponseModel response = await _sender.SendInvoicesAsync(packets).ConfigureAwait(continueOnCapturedContext: false);
		Dictionary<string, string> map = invoicePackets.ToDictionary((InvoicePacket i) => i.PacketDto.Header.RequestTraceId, (InvoicePacket i) => i.TaxId);
		return response.Result.Select((ResponsePacketModel r) => GetInvoiceResponseModel(map, r)).ToList();
	}

	private static InvoiceResponseModel GetInvoiceResponseModel(Dictionary<string, string> map, ResponsePacketModel x)
	{
		return new InvoiceResponseModel(x.Data, x.Uid, x.ReferenceNumber, map[x.Uid]);
	}

	private InvoicePacket GetInvoicePacket(InvoiceDto x)
	{
		return new InvoicePacket(x.Header.taxid, _packetFactory.CreateInvoicePacket(x));
	}
}
