using MyService.Common.Models;

namespace MyService.PaymentsApi
{
	public interface IPaymentsApi
	{
		PayResult PayOrder(int orderNumber, string authToken, PayRequest request);
	}
}