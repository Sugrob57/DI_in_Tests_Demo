namespace MyService.Common.HttpClient
{
	public class RestHttpClientException : Exception
	{
		public RestHttpClientException(string errorMessage) : base(errorMessage) { }
	}
}