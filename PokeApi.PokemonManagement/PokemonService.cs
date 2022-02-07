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
            pokemonApi = pokemonApiConfig.Value.Host;
        }

        public void Dispose()
        {
            client.Dispose();
        }

        public virtual async Task<Pokemon> GetPokemonAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }

            return await GetHttpResponse(pokemonApi + name);
        }

        public virtual async Task<Pokemon> GetTranslatedPokemon(string name)
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

            var contentString = await response.Content.ReadAsStringAsync();

            Pokemon result = JsonSerializer.Deserialize<Pokemon>(contentString,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return result;
        }
    }
}
