using MyService.AdminApi;
using MyService.Common.Models;
using MyService.UserApi;

namespace MyService.TestServices.Services
{
	public class UsersTestService
	{
		public UsersTestService(
			IUserApi userApi,
			IAdminApi adminApi,
			TestContextService testContext)
		{
			_userApi = userApi;
			_adminApi = adminApi;
			_testContext = testContext;
		}

		public UserInfo CreateUser()
		{
			var user = _adminApi.CreateUser();

			_testContext.SetProperty("User", user);
			return user;
		}

		public IUserApi CreateUserAndLogin()
		{
			var user = CreateUser();

			var session = _userApi.Login(user);
			_testContext.SetProperty("Session", session);
			return _userApi;
		}

		private readonly IUserApi _userApi;
		private readonly IAdminApi _adminApi;
		private readonly TestContextService _testContext;
	}
}
