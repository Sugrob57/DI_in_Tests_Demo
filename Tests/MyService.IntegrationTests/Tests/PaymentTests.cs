using FluentAssertions;
using MyService.Common.HttpClient;
using MyService.Common.Models;

namespace MyService.IntegrationTests.Tests
{
    [TestFixture]
	[Parallelizable(ParallelScope.All)]
	public class PaymentTests : TestBase
    {
        [SetUp]
        public void Setup()
        {
			UsersTestService.CreateUserAndLogin();
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
			var result = PaymentsTestService.LoginAndCreateOrderAndPay(sum, currency);

			// assert
			result.IsSuccess.Should().BeTrue("Existent order can be paid");
		}

		[Test]
		[TestCase(100)]
		[TestCase(1.5)]
		public void PayApi_PayInEuro_PaymentSucced(double sum)
		{
			// act
			var result = PaymentsTestService.LoginAndCreateOrderAndPay(sum, "EUR");

			// assert
			result.IsSuccess.Should().BeTrue("Order can be paid with euro");
		}

		[Test]
        public void PayApi_SendTwoRequestsBySession_OnlyOneRequestAccepted()
        {
			// arrange
			var payApi = PaymentsApi;

			// act
			Action act1 = () => payApi.PayOrder(111, Session.BearerToken, new PayRequest());
			Action act2 = () => payApi.PayOrder(222, Session.BearerToken, new PayRequest());


			// assert
			act1.Should().NotThrow("First request should be accepted");
			act2.Should().Throw<RestHttpClientException>("Only one request by session can be accepted");
		}

		[Test]
		public void PayApi_SendRequestsByDifferentSession_AllRequestsAccepted()
		{
			// arrange
			var payApi1 = PaymentsApi;
			var payApi2 = PaymentsApi;

			// act
			Action act1 = () => payApi1.PayOrder(111, Session.BearerToken, new PayRequest());
			Action act2 = () => payApi2.PayOrder(222, Session.BearerToken, new PayRequest());

			// assert
			act1.Should().NotThrow("First request should be accepted");
			act2.Should().NotThrow("First request should be accepted"); ;
		}

		protected UserSession Session
		{
			get => TestContext.GetProperty<UserSession>(nameof(Session));
			set => TestContext.SetProperty(nameof(Session), value);
		}
	}
}