namespace MyService.Common.Models
{
	public class Order
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public List<OrderProduct> Products { get; set; }
	}
}