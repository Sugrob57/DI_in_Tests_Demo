using FluentAssertions;
using MyService.AdminApi;
using MyService.Common;
using MyService.Common.HttpClient;
using MyService.Common.Models;
using MyService.Common.Services;
using MyService.PaymentsApi;
using MyService.UserApi;

namespace MyService.IntegrationTests.Tests
{
    [TestFixture]
	[Parallelizable(ParallelScope.All)]
	public class PaymentTests : TestBase
    {
        [SetUp]
        public void Setup()
        {
			IRestHttpClient httpClient = new RestHttpClient();
			IAdminApi adminApi = new MyServiceAdminApi(httpClient, MyServiceUrl);
			User = adminApi.CreateUser();

			IRestHttpClient httpClient2 = new RestHttpClient();
			IUserApi userApi = new MyServiceUserApi(httpClient2, MyServiceUrl);
			Session = userApi.Login(User);
		}

		[Test]
		[TestCase("RUR")]
		[TestCase("EUR")]
		[TestCase("USD")]
		public void PayApi_PayForExistentOrder_PaymentSucced(string currency)
		{
			// arrange
			var sum = 100.500;

			// act
			var result = LoginAndCreateOrderAndPay(sum, currency);

			// accept
			result.IsSuccess.Should().BeTrue("Existent order can be paid");
		}

		[Test]
		[TestCase(100)]
		[TestCase(1.5)]
		public void PayApi_PayInEuro_PaymentSucced(double sum)
		{
			// act
			var result = LoginAndCreateOrderAndPay(sum);

			// accept
			result.IsSuccess.Should().BeTrue("Order can be paid with euro");
		}

		[Test]
        public void PayApi_SendTwoRequestsBySession_OnlyOneRequestAccepted()
        {
			// arrange
			IRestHttpClient httpClient = new RestHttpClient();
			IPaymentsApi payApi = new MyServicePaymentsApi(httpClient, MyServiceUrl);

			// act
			Action act1 = () => payApi.PayOrder(111, Session.BearerToken, new PayRequest());
			Action act2 = () => payApi.PayOrder(222, Session.BearerToken, new PayRequest());


			// accept
			act1.Should().NotThrow("First request should be accepted");
			act2.Should().Throw<RestHttpClientException>("Only one request by session can be accepted");
		}

		[Test]
		public void PayApi_SendRequestsByDifferentSession_AllRequestsAccepted()
		{
			// arrange
			IRestHttpClient httpClient1 = new RestHttpClient();
			IPaymentsApi payApi1 = new MyServicePaymentsApi(httpClient1, MyServiceUrl);

			IRestHttpClient httpClient2 = new RestHttpClient();
			IPaymentsApi payApi2 = new MyServicePaymentsApi(httpClient2, MyServiceUrl);

			// act
			Action act1 = () => payApi1.PayOrder(111, Session.BearerToken, new PayRequest());
			Action act2 = () => payApi2.PayOrder(222, Session.BearerToken, new PayRequest());


			// accept
			act1.Should().NotThrow("First request should be accepted");
			act2.Should().NotThrow("First request should be accepted"); ;
		}

		[TearDown]
		public void CleanUp()
		{
			IRestHttpClient httpClient = new RestHttpClient();
            IAdminApi adminApi = new MyServiceAdminApi(httpClient, MyServiceUrl);

            adminApi.DeleteUser(User.Login);
		}

		protected UserSession Session
		{
			get => MyTestContext.GetProperty<UserSession>(nameof(Session));
			set => MyTestContext.SetProperty(nameof(Session), value);
		}
	}
}