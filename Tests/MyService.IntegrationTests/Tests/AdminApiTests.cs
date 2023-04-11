using FluentAssertions;

namespace MyService.IntegrationTests.Tests
{
    [TestFixture]
	[Parallelizable(ParallelScope.All)]
	public class AdminApiTests : TestBase
	{
		[Test]
		public void AdminApi_CreateNewUser_UserLoginIsEmail()
		{
			// act
			User = AdminApi.CreateUser();

			// accept
			User.Login.Should().Contain("@", "user login should be email");
		}

		[Test]
		public void AdminApi_CreateNewUser_UserPasswordNotEmpty()
		{
			// act
			User = AdminApi.CreateUser();

			// accept
			User.Password.Should().NotBeNullOrEmpty("user should has password");
		}

		[Test]
		public void AdminApi_DeleteUser_UserDeleted()
		{
			// act
			User = AdminApi.CreateUser();
			Action act = () => AdminApi.DeleteUser(User.Login);

			// accept
			act.Should().NotThrow("user should be deleted");
		}
	}
}