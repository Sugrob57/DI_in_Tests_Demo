using MyService.Common.Extensions;
using MyService.Common.Models;
using MyService.Common.Services;

namespace MyService.UserApi
{
    public class MyServiceUserApi : IUserApi
	{
		public MyServiceUserApi(IRestHttpClient httpCleint, string baseUrl)
		{
			_client = httpCleint;
			_baseUrl = baseUrl;
		}

		public UserSession Login(UserInfo user)
		{
			var session = _client.Post<UserSession>($"{BaseUrl}/login", user.ToJson());
			_client.AddAuthHeader(session.BearerToken);
			return session;
		}

		public Order CreateOrder()
		{
			return _client.Post<Order>($"{BaseUrl}/orders");
		}

		public Order GetOrder(int orderNumber)
		{
			return _client.Get<Order>($"{BaseUrl}/orders?order={orderNumber}");
		}

		public void DeleteOrder(int orderNumber)
		{
			_client.Delete($"{BaseUrl}/orders?order={orderNumber}");
		}

		public void Logout()
		{
			_client.Delete($"{BaseUrl}/logout");
		}

		public string BaseUrl => $"{_baseUrl}/user";

		private readonly string _baseUrl;

		private readonly IRestHttpClient _client;
	}
}