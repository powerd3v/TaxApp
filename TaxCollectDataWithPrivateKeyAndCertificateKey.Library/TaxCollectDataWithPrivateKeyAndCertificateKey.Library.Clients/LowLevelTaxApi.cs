using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Threading;
using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Abstraction.Clients;
using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Abstraction.Providers;
using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Dto;
using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Models;

namespace TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Clients;

public class LowLevelTaxApi : ILowLevelTaxApi
{
	private readonly IClient _client;

	private readonly IRequestProvider _requestProvider;

	public LowLevelTaxApi(IClient client, IRequestProvider requestProvider)
	{
		_client = client;
		_requestProvider = requestProvider;
	}

	public ServerInformationModel GetServerInformation()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Expected O, but got Unknown
		JoinableTaskContext val = new JoinableTaskContext();
		try
		{
			JoinableTaskFactory val2 = new JoinableTaskFactory(val);
			return val2.Run<ServerInformationModel>((Func<Task<ServerInformationModel>>)(async () => await GetServerInformationAsync().ConfigureAwait(continueOnCapturedContext: true)));
		}
		finally
		{
			((IDisposable)val)?.Dispose();
		}
	}

	public async Task<ServerInformationModel> GetServerInformationAsync()
	{
		HttpRequestMessage request = _requestProvider.GetServerInformation();
		return await SendRequestAsync<ServerInformationModel>(request).ConfigureAwait(continueOnCapturedContext: false);
	}

	public FiscalFullInformationModel GetFiscalInformation(string memoryId)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Expected O, but got Unknown
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		string memoryId2 = memoryId;
		JoinableTaskContext val = new JoinableTaskContext();
		try
		{
			JoinableTaskFactory val2 = new JoinableTaskFactory(val);
			return val2.Run<FiscalFullInformationModel>((Func<Task<FiscalFullInformationModel>>)(async () => await GetFiscalInformationAsync(memoryId2).ConfigureAwait(continueOnCapturedContext: true)));
		}
		finally
		{
			((IDisposable)val)?.Dispose();
		}
	}

	public async Task<FiscalFullInformationModel> GetFiscalInformationAsync(string memoryId)
	{
		HttpRequestMessage request = _requestProvider.GetFiscalInformationRequest(memoryId);
		return await SendRequestAsync<FiscalFullInformationModel>(request).ConfigureAwait(continueOnCapturedContext: false);
	}

	public TaxpayerModel GetTaxpayer(string economicCode)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Expected O, but got Unknown
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		string economicCode2 = economicCode;
		JoinableTaskContext val = new JoinableTaskContext();
		try
		{
			JoinableTaskFactory val2 = new JoinableTaskFactory(val);
			return val2.Run<TaxpayerModel>((Func<Task<TaxpayerModel>>)(async () => await GetTaxpayerAsync(economicCode2).ConfigureAwait(continueOnCapturedContext: true)));
		}
		finally
		{
			((IDisposable)val)?.Dispose();
		}
	}

	public async Task<TaxpayerModel> GetTaxpayerAsync(string economicCode)
	{
		HttpRequestMessage request = _requestProvider.GetTaxpayerRequest(economicCode);
		return await SendRequestAsync<TaxpayerModel>(request).ConfigureAwait(continueOnCapturedContext: false);
	}

	public BatchResponseModel SendInvoices(List<PacketDto> packets)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Expected O, but got Unknown
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		List<PacketDto> packets2 = packets;
		JoinableTaskContext val = new JoinableTaskContext();
		try
		{
			JoinableTaskFactory val2 = new JoinableTaskFactory(val);
			return val2.Run<BatchResponseModel>((Func<Task<BatchResponseModel>>)(async () => await SendInvoicesAsync(packets2).ConfigureAwait(continueOnCapturedContext: true)));
		}
		finally
		{
			((IDisposable)val)?.Dispose();
		}
	}

	public async Task<BatchResponseModel> SendInvoicesAsync(List<PacketDto> packets)
	{
		HttpRequestMessage request = _requestProvider.GetInvoicesRequest(packets);
		return await SendRequestAsync<BatchResponseModel>(request).ConfigureAwait(continueOnCapturedContext: false);
	}

	public List<InquiryResultModel> InquiryByTime(InquiryByTimeRangeDto dto)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Expected O, but got Unknown
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		InquiryByTimeRangeDto dto2 = dto;
		JoinableTaskContext val = new JoinableTaskContext();
		try
		{
			JoinableTaskFactory val2 = new JoinableTaskFactory(val);
			return val2.Run<List<InquiryResultModel>>((Func<Task<List<InquiryResultModel>>>)(async () => await InquiryByTimeAsync(dto2).ConfigureAwait(continueOnCapturedContext: true)));
		}
		finally
		{
			((IDisposable)val)?.Dispose();
		}
	}

	public async Task<List<InquiryResultModel>> InquiryByTimeAsync(InquiryByTimeRangeDto dto)
	{
		HttpRequestMessage request = _requestProvider.GetInquiryByTimeRequest(dto);
		return await SendRequestAsync<List<InquiryResultModel>>(request).ConfigureAwait(continueOnCapturedContext: false);
	}

	public List<InquiryResultModel> InquiryByUid(InquiryByUidDto dto)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Expected O, but got Unknown
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		InquiryByUidDto dto2 = dto;
		JoinableTaskContext val = new JoinableTaskContext();
		try
		{
			JoinableTaskFactory val2 = new JoinableTaskFactory(val);
			return val2.Run<List<InquiryResultModel>>((Func<Task<List<InquiryResultModel>>>)(async () => await InquiryByUidAsync(dto2).ConfigureAwait(continueOnCapturedContext: true)));
		}
		finally
		{
			((IDisposable)val)?.Dispose();
		}
	}

	public async Task<List<InquiryResultModel>> InquiryByUidAsync(InquiryByUidDto dto)
	{
		HttpRequestMessage request = _requestProvider.GetInquiryByUidRequest(dto);
		return await SendRequestAsync<List<InquiryResultModel>>(request).ConfigureAwait(continueOnCapturedContext: false);
	}

	public List<InquiryResultModel> InquiryByReferenceId(InquiryByReferenceNumberDto dto)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Expected O, but got Unknown
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		InquiryByReferenceNumberDto dto2 = dto;
		JoinableTaskContext val = new JoinableTaskContext();
		try
		{
			JoinableTaskFactory val2 = new JoinableTaskFactory(val);
			return val2.Run<List<InquiryResultModel>>((Func<Task<List<InquiryResultModel>>>)(async () => await InquiryByReferenceIdAsync(dto2).ConfigureAwait(continueOnCapturedContext: true)));
		}
		finally
		{
			((IDisposable)val)?.Dispose();
		}
	}

	public async Task<List<InquiryResultModel>> InquiryByReferenceIdAsync(InquiryByReferenceNumberDto dto)
	{
		HttpRequestMessage request = _requestProvider.GetInquiryByReferenceIdRequest(dto);
		return await SendRequestAsync<List<InquiryResultModel>>(request).ConfigureAwait(continueOnCapturedContext: false);
	}

	private async Task<T> SendRequestAsync<T>(HttpRequestMessage request)
	{
		HttpRequestMessage nonceRequest = _requestProvider.GetNonceRequest();
		return await _client.SendRequestAsync<T>(request, nonceRequest).ConfigureAwait(continueOnCapturedContext: false);
	}
}
