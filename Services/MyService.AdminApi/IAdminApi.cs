using MyService.Common.Models;

namespace MyService.AdminApi
{
	public interface IAdminApi
	{
		UserInfo CreateUser();

		void DeleteUser(string login);
	}
}