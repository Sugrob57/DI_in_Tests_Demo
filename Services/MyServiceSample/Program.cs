using MyService.Sample;
using WireMock.Settings;

namespace WireMock.Server
{
	public class Program
	{
		static void Main(string[] args)
		{
			var wireMockUri = "http://localhost:9091/";

			RunMockServer(wireMockUri);
		}

		public static void RunMockServer(string wireMockUri)
		{
			var wiremockSettings = new WireMockServerSettings
			{
				Urls = new[] {wireMockUri},
				StartAdminInterface = true,
			};

			var mock = WireMockServer.Start(wiremockSettings);
			var mocks = new ApiRequestMocks(mock);
			mocks.MockAllAdminApiRequests();
			mocks.MockAllUserApiRequests();
			mocks.MockAllPaymentApiRequests();

			Console.WriteLine("WireMock server started");
			Console.WriteLine("Press any key for stop server...");
			Console.ReadKey();
			mock.Stop();
		}
	}
}