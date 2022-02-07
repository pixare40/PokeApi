using Microsoft.Extensions.Options;
using PokeApi.Infrastructure;
using PokeApi.Models;
using PokeApi.Models.ConfigModels;
using PokeApi.PokemonManagement;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace PokemonManagement
{
    public class PokemonService : IPokemonService
    {
        public readonly string pokemonApi;
        private readonly IHttpClientProvider httpClient;

        public PokemonService(IOptions<PokemonApiConfigModel> pokemonApiConfig, IHttpClientProvider httpClient)
        {
            pokemonApi = pokemonApiConfig.Value.PokeApiEndpoint;
            this.httpClient = httpClient;
        }

        public virtual async Task<Pokemon> GetPokemonAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }

            return await httpClient.GetAsync<Pokemon>(string.Format("{0}{1}", pokemonApi, name));
        }
    }
}
