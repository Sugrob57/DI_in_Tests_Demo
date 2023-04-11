using FluentAssertions;
using MyService.AdminApi;
using MyService.Common;
using MyService.Common.Services;
using MyService.UserApi;

namespace MyService.IntegrationTests.Tests
{
    [TestFixture]
	[Parallelizable(ParallelScope.All)]
	public class OrderTests : TestBase
    {
        [SetUp]
        public void Setup()
        {
			IRestHttpClient httpClient = new RestHttpClient();
			IAdminApi adminApi = new MyServiceAdminApi(httpClient, MyServiceUrl);
			User = adminApi.CreateUser();
		}

        [Test]
        public void UserApi_CreateNewOrder_OrderCreated()
        {
			// arrange
			IRestHttpClient httpClient = new RestHttpClient();
			IUserApi userApi = new MyServiceUserApi(httpClient, MyServiceUrl);
			userApi.Login(User);

			// act
			var order = userApi.CreateOrder();

			// accept
			order.Id.Should().BeGreaterThan(0, "Order number should has positive value");
			order.Name.Should().NotBeNullOrEmpty("Order name should be not empty text");
		}

		[Test]
		public void UserApi_GetOrder_OrderHasProducts()
		{
			// arrange
			IRestHttpClient httpClient = new RestHttpClient();
			IUserApi userApi = new MyServiceUserApi(httpClient, MyServiceUrl);
			userApi.Login(User);

			// act
			var order = userApi.CreateOrder();
			order = userApi.GetOrder(order.Id);

			// accept
			order.Products.Should().HaveCount(2, "2 products was mocked for get method");
		}

		[Test]
		public void UserApi_DeleteOrder_OrderDeleted()
		{
			// arrange
			IRestHttpClient httpClient = new RestHttpClient();
			IUserApi userApi = new MyServiceUserApi(httpClient, MyServiceUrl);
			userApi.Login(User);

			// act
			var order = userApi.CreateOrder();
			Action act = () => userApi.DeleteOrder(order.Id);

			// accept
			act.Should().NotThrow("existent order can be deleted");
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