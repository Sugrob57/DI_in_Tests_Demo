namespace MyService.Common.Services
{
    public interface IRestHttpClient
    {
        T? Get<T>(string address);

        T? Post<T>(string address, string data = null);

        void Delete(string address);

        void AddAuthHeader(string bearerToken);
    }
}