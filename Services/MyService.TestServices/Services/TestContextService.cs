using MyService.AdminApi;
using MyService.Common.Models;
using MyService.Common.Services;

namespace MyService.TestServices.Services
{
	public class TestContextService : IDisposable
	{
		public TestContextService(IAdminApi adminApi)
		{
			_adminApi= adminApi;
		}

		public void SetProperty(string key, object value)
		{
			MyTestContext.SetProperty(key, value);
		}

		public T GetProperty<T>(string key)
		{
			return MyTestContext.GetProperty<T>(key);
		}

		void IDisposable.Dispose()
		{
			var user = GetProperty<UserInfo>("User");

			if (user != null)
			{
				_adminApi.DeleteUser(user.Login);
			}
		}

		private readonly IAdminApi _adminApi;
	}
}
