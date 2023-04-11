using Microsoft.Extensions.Configuration;

namespace MyService.Common.Services
{
	public class ApplicationConfiguration  : IApplicationConfiguration
	{
		public ApplicationConfiguration()
		{
			_config = new ConfigurationBuilder()
				.AddJsonFile("commonAppSettings.json", true, true)
				.Build();
		}
		

		public string GetAppSettingValue(string key)
		{
			return _config.GetSection($"AppSettings:{key}").Value;
		}

		private readonly IConfiguration _config;
	}
}