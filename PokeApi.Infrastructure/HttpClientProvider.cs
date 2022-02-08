using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace PokeApi.Infrastructure
{
    public class HttpClientProvider : IHttpClientProvider, IDisposable
    {
        private readonly HttpClient client;

        public HttpClient Client
        {
            get { return client; }
        }

        public HttpClientProvider(IHttpClientFactory httpClientFactory)
        {
            client = httpClientFactory.CreateClient();
        }

        public async Task<T> GetAsync<T>(string requestUri)
        {
            if (string.IsNullOrEmpty(requestUri))
            {
                throw new ArgumentNullException();
            }

            HttpResponseMessage response = await Client.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();

            var contentString = await response.Content.ReadAsStringAsync();

            T result = DeserializeContentString<T>(contentString);
            return result;
        }

        public static T DeserializeContentString<T>(string contentString)
        {
            return JsonSerializer.Deserialize<T>(contentString,
                            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public void Dispose()
        {
            Client.Dispose();
        }
    }
}
