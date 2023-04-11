using MyService.Common.Models;

namespace MyService.UserApi
{
	public interface IUserApi
	{
		UserSession Login(UserInfo user);

		Order CreateOrder();

		Order GetOrder(int orderNumber);

		void DeleteOrder(int orderNumber);

		void Logout();
	}
}