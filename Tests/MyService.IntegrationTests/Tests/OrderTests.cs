using FluentAssertions;

namespace MyService.IntegrationTests.Tests
{
    [TestFixture]
	[Parallelizable(ParallelScope.All)]
	public class OrderTests : TestBase
    {
        [SetUp]
        public void Setup()
        {
			User = AdminApi.CreateUser();
		}

        [Test]
        public void UserApi_CreateNewOrder_OrderCreated()
        {
			// arrange
			UserApi.Login(User);

			// act
			var order = UserApi.CreateOrder();

			// assert
			order.Id.Should().BeGreaterThan(0, "Order number should has positive value");
			order.Name.Should().NotBeNullOrEmpty("Order name should be not empty text");
		}

		[Test]
		public void UserApi_GetOrder_OrderHasProducts()
		{
			// arrange
			UserApi.Login(User);

			// act
			var order = UserApi.CreateOrder();
			order = UserApi.GetOrder(order.Id);

			// assert
			order.Products.Should().HaveCount(2, "2 products was mocked for get method");
		}

		[Test]
		public void UserApi_DeleteOrder_OrderDeleted()
		{
			// arrange
			UserApi.Login(User);

			// act
			var order = UserApi.CreateOrder();
			Action act = () => UserApi.DeleteOrder(order.Id);

			// assert
			act.Should().NotThrow("existent order can be deleted");
		}
	}
}