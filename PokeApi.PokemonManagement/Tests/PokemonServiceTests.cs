using Microsoft.Extensions.Options;
using NUnit.Framework;
using PokeApi.Infrastructure;
using PokeApi.Models.ConfigModels;
using PokemonManagement;
using System.Threading.Tasks;

namespace PokeApi.PokemonManagement.Tests
{
    [TestFixture]
    public class PokemonServiceTests
    {
        [Test]
        public void Send_Null_As_Pokemon_Name()
        {
            var options = Options.Create<PokemonApiConfigModel>(
                new PokemonApiConfigModel { PokeApiEndpoint = "" });

            var httpClientMock = new HttpClientProviderMock();

            var pokemonService = new PokemonService(options, httpClientMock);
            Assert.IsNull(pokemonService.GetPokemonAsync(""));
        }
    }

    public class HttpClientProviderMock : IHttpClientProvider
    {
        public Task<T> GetAsync<T>(string requestUri)
        {
            throw new System.NotImplementedException();
        }
    }
}
