using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace PokeApi.Infrastructure
{
    public class HttpClientProvider : IHttpClientProvider, IDisposable
    {
        private readonly HttpClient client;

        public HttpClientProvider()
        {
            client = new HttpClient();
        }

        public async Task<T> GetAsync<T>(string requestUri)
        {
            HttpResponseMessage response = await client.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();

            var contentString = await response.Content.ReadAsStringAsync();

            T result = JsonSerializer.Deserialize<T>(contentString,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return result;
        }

        public void Dispose()
        {
            client.Dispose();
        }
    }
}
