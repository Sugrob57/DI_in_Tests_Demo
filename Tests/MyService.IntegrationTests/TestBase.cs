using MyService.AdminApi;
using MyService.Common;
using MyService.Common.Models;
using MyService.Common.Services;
using MyService.PaymentsApi;
using MyService.UserApi;

namespace MyService.IntegrationTests
{
    public abstract class TestBase
	{
		protected IUserApi CreateUserAndLogin()
		{
			IRestHttpClient httpClient = new RestHttpClient();
			IAdminApi adminApi = new MyServiceAdminApi(httpClient, MyServiceUrl);
			User = adminApi.CreateUser();

			IRestHttpClient httpClient2 = new RestHttpClient();
			return new MyServiceUserApi(httpClient2, MyServiceUrl);
		}

		protected PayResult LoginAndCreateOrderAndPay(double sum, string currency)
		{
			IRestHttpClient httpClient = new RestHttpClient();
			IUserApi userApi = new MyServiceUserApi(httpClient, MyServiceUrl);
			var token = userApi.Login(User).BearerToken;

			IRestHttpClient httpClient2 = new RestHttpClient();
			IPaymentsApi payApi = new MyServicePaymentsApi(httpClient2, MyServiceUrl);

			var order = userApi.CreateOrder();

			var payRequest = new PayRequest
			{
				Sum = sum,
				Сurrency = currency,
			};

			var payResult = payApi.PayOrder(order.Id, token, payRequest);

			return payResult;
		}

		protected PayResult LoginAndCreateOrderAndPay(double sumInEuro)
		{
			IRestHttpClient httpClient = new RestHttpClient();
			IUserApi userApi = new MyServiceUserApi(httpClient, MyServiceUrl);
			var token = userApi.Login(User).BearerToken;

			IRestHttpClient httpClient2 = new RestHttpClient();
			IPaymentsApi payApi = new MyServicePaymentsApi(httpClient2, MyServiceUrl);

			var order = userApi.CreateOrder();

			var payRequest = new PayRequest
			{
				Sum = sumInEuro,
				Сurrency = "Euro",
			};

			var payResult = payApi.PayOrder(order.Id, token, payRequest);

			return payResult;
		}

		protected UserInfo User
		{
			get => MyTestContext.GetProperty<UserInfo>(nameof(User));
			set => MyTestContext.SetProperty(nameof(User), value);
		}

		protected string MyServiceUrl = "http://localhost:9091";
	}
}