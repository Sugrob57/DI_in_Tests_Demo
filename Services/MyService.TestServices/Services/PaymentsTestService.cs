using MyService.Common.Models;
using MyService.Common.Services;
using MyService.PaymentsApi;
using MyService.UserApi;

namespace MyService.TestServices.Services
{
	public class PaymentsTestService
	{
		public PaymentsTestService(
			IUserApi userApi,
			IPaymentsApi paymentsApi,
			TestContextService testContext,
			UsersTestService userTS)
		{
			_userApi = userApi;
			_paymentsApi = paymentsApi;
			_testContext = testContext;
			_userTS= userTS;
		}

		public PayResult LoginAndCreateOrderAndPay(double sum, string currency)
		{
			_userTS.CreateUserAndLogin();
			var token = _testContext.GetProperty<UserSession>("Session").BearerToken;
			var order = _userApi.CreateOrder();

			var payRequest = new PayRequest
			{
				Sum = sum,
				Сurrency = currency,
			};

			return _paymentsApi.PayOrder(order.Id, token, payRequest);
		}

		private readonly IUserApi _userApi;
		private readonly IPaymentsApi _paymentsApi;
		private readonly TestContextService _testContext;
		private readonly UsersTestService _userTS;
	}
}
