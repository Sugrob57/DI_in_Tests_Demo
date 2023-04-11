namespace MyService.Common.Services
{
	public interface IApplicationConfiguration
	{
		string GetAppSettingValue(string key);
	}
}
