using MyService.Common.Extensions;
using MyService.Common.Models;
using MyService.Common.Services;

namespace MyService.UserApi
{
    public class MyServiceUserApi : IUserApi, IDisposable
	{
		public MyServiceUserApi(IRestHttpClient httpCleint, IApplicationConfiguration configuration)
		{
			_client = httpCleint;
			_baseUrl = configuration.GetAppSettingValue(ConfigSettingNames.MyServiceUrl);
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

		void IDisposable.Dispose()
		{
			Logout();
		}

		public string BaseUrl => $"{_baseUrl}/user";

		private readonly string _baseUrl;

		private readonly IRestHttpClient _client;
	}
}