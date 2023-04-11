using FluentAssertions;
using MyService.AdminApi;
using MyService.Common.Services;

namespace MyService.IntegrationTests.Tests
{
    [TestFixture]
	[Parallelizable(ParallelScope.All)]
	public class AdminApiTests : TestBase
	{
		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public void AdminApi_CreateNewUser_UserLoginIsEmail()
		{
			// arrange
			IRestHttpClient httpClient = new RestHttpClient();
			IAdminApi adminApi = new MyServiceAdminApi(httpClient, MyServiceUrl);

			// act
			User = adminApi.CreateUser();

			// accept
			User.Login.Should().Contain("@", "user login should be email");
		}

		[Test]
		public void AdminApi_CreateNewUser_UserPasswordNotEmpty()
		{
			// arrange
			IRestHttpClient httpClient = new RestHttpClient();
			IAdminApi adminApi = new MyServiceAdminApi(httpClient, MyServiceUrl);

			// act
			User = adminApi.CreateUser();

			// accept
			User.Password.Should().NotBeNullOrEmpty("user should has password");
		}

		[Test]
		public void AdminApi_DeleteUser_UserDeleted()
		{
			// arrange
			IRestHttpClient httpClient = new RestHttpClient();
			IAdminApi adminApi = new MyServiceAdminApi(httpClient, MyServiceUrl);

			// act
			User = adminApi.CreateUser();
			Action act = () => adminApi.DeleteUser(User.Login);

			// accept
			act.Should().NotThrow("user should be deleted");
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