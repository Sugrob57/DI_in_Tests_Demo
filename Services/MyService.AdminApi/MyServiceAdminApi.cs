﻿using MyService.Common.Models;
using MyService.Common.Services;

namespace MyService.AdminApi
{
    public class MyServiceAdminApi :IAdminApi
	{
		public MyServiceAdminApi(IRestHttpClient httpCleint, IApplicationConfiguration configuration)
		{
			_client = httpCleint;
			_baseUrl = configuration.GetAppSettingValue(ConfigSettingNames.MyServiceUrl);
		}

		public UserInfo CreateUser()
		{
			return _client.Post<UserInfo>($"{BaseUrl}/users");
		}

		public void DeleteUser(string login)
		{
			_client.Delete($"{BaseUrl}/users");
		}

		public string BaseUrl => $"{_baseUrl}/admin/api";

		private readonly string _baseUrl;

		private readonly IRestHttpClient _client;
	}
}