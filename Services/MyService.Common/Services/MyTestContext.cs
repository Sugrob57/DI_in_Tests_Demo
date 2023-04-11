using NUnit.Framework.Internal;

namespace MyService.Common.Services
{
	public class MyTestContext
	{
		public static void SetProperty(string key, object value)
		{
			TestExecutionContext.CurrentContext.CurrentTest.Properties.Set(key, value);
		}

		public static T GetProperty<T>(string key)
		{
			if (TestExecutionContext.CurrentContext.CurrentTest.Properties.ContainsKey(key))
			{
				return (T)TestExecutionContext.CurrentContext.CurrentTest.Properties.Get(key);
			}

			return default(T);
		}
	}
}