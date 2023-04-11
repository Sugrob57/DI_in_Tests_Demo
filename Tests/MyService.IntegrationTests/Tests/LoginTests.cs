using FluentAssertions;
using MyService.AdminApi;
using MyService.Common;
using MyService.Common.HttpClient;
using MyService.Common.Services;
using MyService.UserApi;

namespace MyService.IntegrationTests.Tests
{
    [TestFixture]
	[Parallelizable(ParallelScope.All)]
	public class LoginTests : TestBase
    {
        [SetUp]
        public void Setup()
        {
			IRestHttpClient httpClient = new RestHttpClient();
			IAdminApi adminApi = new MyServiceAdminApi(httpClient, MyServiceUrl);
			User = adminApi.CreateUser();
		}

        [Test]
        public void UserApi_NotLogin_CreateNewOrder_AuthFailed()
        {
			// arrange
			IRestHttpClient httpClient = new RestHttpClient();
			IUserApi userApi = new MyServiceUserApi(httpClient, MyServiceUrl);

			// act
			Action act = () => userApi.CreateOrder();

			// accept
			act.Should().Throw<RestHttpClientException>("Not authorized user can not create orders");
		}

		[Test]
		public void UserApi_NotLogin_GetOrder_AuthFailed()
		{
			// arrange
			IRestHttpClient httpClient = new RestHttpClient();
			IUserApi userApi = new MyServiceUserApi(httpClient, MyServiceUrl);

			// act
			Action act = () => userApi.GetOrder(123);

			// accept
			act.Should().Throw<RestHttpClientException>("Not authorized user can not get orders");
		}

		[Test]
		public void UserApi_NotLogin_DeleteOrder_AuthFailed()
		{
			// arrange
			IRestHttpClient httpClient = new RestHttpClient();
			IUserApi userApi = new MyServiceUserApi(httpClient, MyServiceUrl);

			// act
			Action act = () => userApi.DeleteOrder(123);

			// accept
			act.Should().Throw<RestHttpClientException>("Not authorized user can not delete orders");
		}

		[TearDown]
		public void CleanUp()
		{
			IRestHttpClient httpClient = new RestHttpClient();
            IAdminApi adminApi = new MyServiceAdminApi(httpClient, MyServiceUrl);

            adminApi.DeleteUser(User.Login);
		}
	}
}