using MyService.Common.Extensions;
using MyService.Common.Models;
using MyService.Common.Services;

namespace MyService.PaymentsApi
{
    public class MyServicePaymentsApi : IPaymentsApi
	{
		public MyServicePaymentsApi(IRestHttpClient httpCleint, string baseUrl)
		{
			_client = httpCleint;
			_baseUrl = baseUrl;
		}

		public PayResult PayOrder(int orderNumber, string authToken, PayRequest request)
		{
			_client.AddAuthHeader(authToken);
			return _client.Post<PayResult>($"{BaseUrl}?order={orderNumber}", request.ToJson());
		}

		public string BaseUrl => $"{_baseUrl}/pay/order";

		private readonly string _baseUrl;

		private readonly IRestHttpClient _client;
	}
}