using Microsoft.Extensions.Options;
using PokeApi.Models;
using PokeApi.Models.ConfigModels;
using PokeApi.PokemonManagement;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace PokemonManagement
{
    public class PokemonService : IPokemonService, IDisposable
    {
        public readonly HttpClient client;
        public readonly string pokemonApi;
        public PokemonService(IOptions<PokemonApiConfigModel> pokemonApiConfig)
        {
            client = new HttpClient();
            pokemonApi = pokemonApiConfig.Value.PokemonApi;
        }

        public void Dispose()
        {
            client.Dispose();
        }

        public async Task<Pokemon> GetPokemonAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }

            return await GetHttpResponse(pokemonApi + name);
        }

        public async Task<Pokemon> GetTranslatedPokemon(string name)
        {
            if (name == null)
            {
                return null;
            }

            return await GetHttpResponse(pokemonApi + "translated/" + name);
        }

        private async Task<Pokemon> GetHttpResponse(string url)
        {
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var contentStream = await response.Content.ReadAsStreamAsync();

            return await JsonSerializer.DeserializeAsync<Pokemon>(contentStream,
                new JsonSerializerOptions { IgnoreNullValues = true, PropertyNameCaseInsensitive = true });
        }
    }
}
