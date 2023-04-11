using MyService.Common.Extensions;
using MyService.Common.HttpClient;
using RestSharp;

namespace MyService.Common.Services
{
    public class RestHttpClient : IRestHttpClient
    {
        public RestHttpClient()
        {
            var options = new RestClientOptions()
            {
                ThrowOnAnyError = false,
                Timeout = 10000,
                FailOnDeserializationError = true
            };

            _client = new RestClient(options);
        }

        public T? Get<T>(string address)
        {
            var request = new RestRequest(address);
            request.Method = Method.Get;

            var response = _client.Execute(request);

            if (!response.IsSuccessStatusCode)
            {
                throw new RestHttpClientException($"status code is {response.StatusCode}");
            }

            return response.Content.FromJson<T>();
        }

        public T? Post<T>(string address, string data = null)
        {
            var request = new RestRequest(address);
            request.Method = Method.Post;

            if (!string.IsNullOrEmpty(data))
            {
                request.AddJsonBody(data);
            }

            var response = _client.Execute(request);
            if (!response.IsSuccessStatusCode)
            {
                throw new RestHttpClientException($"status code is {response.StatusCode}");
            }

            return response.Content.FromJson<T>();
        }

        public void Delete(string address)
        {
            var request = new RestRequest(address);
            request.Method = Method.Delete;

            var response = _client.Execute(request);
            if (!response.IsSuccessStatusCode)
            {
                throw new RestHttpClientException($"status code is {response.StatusCode}");
            }
        }

        public void AddAuthHeader(string bearerToken)
        {
            if (!IsDefaultHeaderAdded)
            {
                _client.AddDefaultHeader("Authorization", $"Bearer {bearerToken}");
                IsDefaultHeaderAdded = true;
            }
        }

        private bool IsDefaultHeaderAdded = false;

        private readonly RestClient _client;
    }
}