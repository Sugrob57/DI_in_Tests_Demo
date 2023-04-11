using FluentAssertions;
using MyService.Common.HttpClient;

namespace MyService.IntegrationTests.Tests
{
    [TestFixture]
	[Parallelizable(ParallelScope.All)]
	public class LoginTests : TestBase
    {
        [SetUp]
        public void Setup()
        {
			User = AdminApi.CreateUser();
		}

        [Test]
        public void UserApi_NotLogin_CreateNewOrder_AuthFailed()
        {
			// act
			Action act = () => UserApi.CreateOrder();

			// assert
			act.Should().Throw<RestHttpClientException>("Not authorized user can not create orders");
		}

		[Test]
		public void UserApi_NotLogin_GetOrder_AuthFailed()
		{
			// act
			Action act = () => UserApi.GetOrder(123);

			// assert
			act.Should().Throw<RestHttpClientException>("Not authorized user can not get orders");
		}

		[Test]
		public void UserApi_NotLogin_DeleteOrder_AuthFailed()
		{
			// act
			Action act = () => UserApi.DeleteOrder(123);

			// assert
			act.Should().Throw<RestHttpClientException>("Not authorized user can not delete orders");
		}
	}
}